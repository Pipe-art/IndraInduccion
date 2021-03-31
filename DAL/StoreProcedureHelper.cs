using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Discografica.Resource;
using Discografica.Utilities;
using System.Reflection;
using System.Threading.Tasks;
using Discografica.DAL.Properties;

namespace Discografica.DAL
{
    public class StoreProcedureHelper<T> : IStoreProcedureHelper<T> where T : new()
    {
        private T entidadBase;
        private Assembly ensamblado;
        private readonly IConfiguration _configurationFile;



        #region Constructor

        /// <summary>
        /// Constructor de la Clase.
        /// </summary>
        /// <param name="_configurationFile"></param>
        public StoreProcedureHelper(IConfiguration configurationFile)
        {
            _configurationFile = configurationFile;
            LoadAssembly();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado y 
        /// obtener un listado de objetos en base a una lista de parámetros de ENTRADA (Input) definidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="listParameters">Lista de parametros (Entrada) opcional, para ejecutar el procedimiento almacenado</param>
        /// <returns>Lista adaptada a una entidad DTO</returns>
        public IList<T2> ExecuteReader<T2>(string procedureName, IList<SqlParameter> listParameters = null)
        {
            var tuplaPropiedades = LoadEntity<T2>();
            SqlConnection conn = new SqlConnection(CreateConnectionString());
            conn.Open();
            SqlDataReader reader = ListAccess(procedureName, listParameters, ref conn);
            int cantidadColumnas = reader.FieldCount;
            IList<string> nombreColumnasObjetoSalida = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            var columnasDeRetornoContraColumnasObjetoSalida = tuplaPropiedades.Item1.Intersect(nombreColumnasObjetoSalida);
            if (cantidadColumnas != columnasDeRetornoContraColumnasObjetoSalida.Count())
            {
                throw new Exception(string.Format("Existen campos del objeto de salida, que no estan considerados en las propiedades del DTO de retorno cantidadColumnas {0} " +
                                                  "columnasDeRetornoContraColumnasObjetoSalida {1} nombreColumnasObjetoSalida {2}  nombreColumnasObjetoSalida {3}",
                                                   cantidadColumnas, columnasDeRetornoContraColumnasObjetoSalida, String.Join(",", nombreColumnasObjetoSalida.ToList().ToArray()),
                                                   string.Join(",", tuplaPropiedades.Item1.ToList().ToArray())));
            }
            IList<T2> listadoRetorno = AdapterReaderToDTO<T2>(reader);
            conn.Close();
            return listadoRetorno;

        }

        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado,
        /// y obtener un listado de objetos en base a una entidad, desde la cual
        /// se utilizan los PropertyAtributeName definidos en esta, en conjunto con sus valores 
        /// como parámetros de ENTRADA (Input) para el procedimiento almacenado.        
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="entityToUse">Entidad con los PropertyAttributeName</param>
        /// <returns>Lista adaptada a una entidad DTO</returns>
        public IList<T2> ExecuteReaderWhitAutomaticParameters<T2>(string procedureName, dynamic entityToUse)
        {
            var tupläPropiedades = LoadEntity<T2>();
            SqlConnection conn = new SqlConnection(CreateConnectionString());
            conn.Open();
            List<SqlParameter> listaParametros = AdapterDTOToParameter(entityToUse, tupläPropiedades.Item2);
            SqlDataReader reader = ListAccess(procedureName, listaParametros, ref conn);
            int cantidadColumnas = reader.FieldCount;

            if (cantidadColumnas != tupläPropiedades.Item2.Count())
            {
                throw new Exception("Existen diferencias entre el objeto de salida y los campos de retorno del SP");
            }

            IList<T2> listadoRetorno = AdapterReaderToDTO<T2>(reader);
            conn.Close();
            return listadoRetorno;
        }

        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado que afecta filas dentro de la base de datos,
        /// en base a una lista de parámetros de ENTRADA (Input) definidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="listParameters">Lista de parámetros (Entrada/Salida) opcional, para ejecutar el procedimiento almacenado</param>
        /// <returns>Número de filas afectadas</returns>
        public T2 ExecuteNonQuery<T2>(string procedureName, IList<SqlParameter> listParameters = null)
        {
            var tuplaPropiedades = LoadEntity<T2>();
            return ExecuteAccess<T2>(procedureName, tuplaPropiedades.Item2, listParameters);
        }

        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado que afecta filas dentro de la 
        /// base de datos, base a una entidad desde la cual se utilizan los PropertyAtributeName 
        /// definidos en esta, en conjunto con sus valores como parámetros de ENTRADA (Input) para el procedimiento almacenado.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="entityToUse">Entidad con los PropertyAttributeName</param>
        /// <returns>Número de filas afectadas</returns>
        public T2 ExecuteNonQueryWithAutomaticParameters<T2>(string procedureName, dynamic entityToUse)
        {
            var tuplaPropiedades = LoadEntity<T2>();
            List<SqlParameter> listaParametros = AdapterDTOToParameter(entityToUse, tuplaPropiedades.Item2);
            return ExecuteAccess<T2>(procedureName, tuplaPropiedades.Item2, listaParametros);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Carga el ensamblado requerido para utilizar reflection,
        /// se debe cargar el ensamblado ya que forma parte 
        /// de otro assembly referenciado.
        /// </summary>
        internal void LoadAssembly()
        {
            entidadBase = new T();
            ensamblado = Assembly.Load(Assembly.GetAssembly(entidadBase.GetType()).FullName);
        }

        internal Tuple<IList<string>, IEnumerable<PropertyInfo>> LoadEntity<T2>()
        {
            IEnumerable<PropertyInfo> listaPropiedades;
            IList<string> listaNombresColumnas;
            var c = typeof(T2).GetConstructors();
            T2 entidadResponse = (T2)c.First().Invoke(new Object[0]);
            listaPropiedades = ensamblado.GetType(entidadResponse.GetType().FullName, true).GetProperties().ToList();
            listaNombresColumnas = new List<string>();
            listaPropiedades.ToList().ForEach(property =>
            {
                var attributes = property.GetCustomAttributes(false);
                PropertyNameAttribute columnMapping = (PropertyNameAttribute)attributes.FirstOrDefault(a => a.GetType() == typeof(PropertyNameAttribute));
                listaNombresColumnas.Add(columnMapping.MappingName);
            });
            return new Tuple<IList<string>, IEnumerable<PropertyInfo>>(listaNombresColumnas, listaPropiedades);
        }

        #endregion

        #region Private

        /// <summary>
        /// Método para adaptar una entidad DTO a una lista de parámetros SQL.
        /// </summary>
        /// <param name="reader">Reader a adaptar</param>
        /// <returns>Lista de retorno adaptada a entidad DTO</returns>
        private List<SqlParameter> AdapterDTOToParameter(dynamic entityToUse, IEnumerable<PropertyInfo> listaPropiedades)
        {
            List<SqlParameter> listaParametros = new List<SqlParameter>();
            listaPropiedades.ToList().ForEach(property =>
            {
                var attributes = property.GetCustomAttributes(false);
                var columnMapping = attributes.FirstOrDefault(a => a.GetType() == typeof(PropertyNameAttribute));

                if (!Resource.Resource.excludedPropertiesToMapping.Contains(property.Name))
                {
                    if (columnMapping != null)
                    {
                        var mapsto = columnMapping as PropertyNameAttribute;
                        listaParametros.Add(new SqlParameter
                        {
                            ParameterName = mapsto.MappingName,
                            SqlDbType = Utilities.Utilities.ConvertirTipoDeDatoASqlDbType(property.PropertyType.Name),
                            Value = property.GetValue(entityToUse, null)
                        });
                    }
                    else
                    {
                        throw new EvaluateException(string.Format("La propiedad: {0} no contiene atributo.", property.Name));
                    }
                }
            });

            return listaParametros;
        }

        /// <summary>
        /// Método para adaptar una entidad DTO a una lista de parámetros SQL.
        /// </summary>
        /// <param name="reader">Reader a adaptar</param>
        /// <returns>Lista de retorno adaptada a entidad DTO</returns>
        private T2 AdapterParametersToDTO<T2>(IList<SqlParameter> listaParametros, IEnumerable<PropertyInfo> listaPropiedades)
        {
            var c = typeof(T2).GetConstructors();
            T2 entidadResponse = (T2)c.First().Invoke(new Object[0]);
            Type tipo = ensamblado.GetType(entidadResponse.GetType().FullName, true);
            var listadoRetorno = new List<T2>();
            var item = (dynamic)null;

            item = Activator.CreateInstance(tipo);

            listaPropiedades.ToList().ForEach(property =>
            {
                var attributes = property.GetCustomAttributes(false);
                var columnMapping = attributes.FirstOrDefault(a => a.GetType() == typeof(PropertyNameAttribute));

                if (columnMapping != null)
                {
                    var mapsto = columnMapping as PropertyNameAttribute;

                    try
                    {
                        if (listaParametros.Any(x => x.ParameterName.Equals(mapsto.MappingName)))
                        {
                            var valor = listaParametros.First(x => x.ParameterName.Equals(mapsto.MappingName)).Value;

                            var valorConvertido =Utilities.Utilities.ConvertirObjetoAValorConTipoDeDato(property, valor);

                            item.GetType().GetProperty(property.Name).SetValue(item, valorConvertido);
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("El almacen de datos no contiene una definición para la columna {0}:", mapsto.MappingName));
                    }
                }
                else
                {
                    throw new EvaluateException(string.Format("La propiedad: {0} no contiene atributo.", property.Name));
                }
            });

            return item;
        }

        /// <summary>
        /// Método para adaptar las filas del reader a un DTO del sistema.
        /// </summary>
        /// <param name="reader">Reader a adaptar</param>
        /// <returns>Lista de retorno adaptada a entidad DTO</returns>
        private IList<T2> AdapterReaderToDTO<T2>(SqlDataReader reader)
        {
            var c = typeof(T2).GetConstructors();
            T2 entidadResponse = (T2)c.First().Invoke(new Object[0]);
            Type tipo = ensamblado.GetType(entidadResponse.GetType().FullName, true);
            var listadoRetorno = new List<T2>();
            var item = (dynamic)null;
            while (reader.Read())
            {
                item = Activator.CreateInstance(tipo, reader);
                listadoRetorno.Add(item);
                item = null;
            }

            return listadoRetorno;
        }

        /// <summary>
        /// Método utilizado para realizar el ExecuteReader, para obtener un Reader con registros,
        /// en base a una lista de parámetros definida.
        /// </summary>
        /// <param name="storeProcedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="pListaParametros">Lista de parámetros (Entrada/salida) del procedimiento</param>
        /// <returns>Reader con los registros obtenidos</returns>
        private SqlDataReader ListAccess(string procedureName, IList<SqlParameter> listParameters, ref SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = procedureName, CommandType = CommandType.StoredProcedure })
            {
                if (listParameters != null)
                {
                    cmd.Parameters.AddRange(listParameters.ToArray());
                }
                SqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }
        }

        ///// <summary>
        ///// Método utilizado para realizar el ExecuteNonQuery, para obtener un numero de filas afectadas,
        ///// en base a una lista de parámetros definida.
        ///// </summary>
        ///// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        ///// <param name="listParameters">Lista de parámetros (Entrada/salida) del procedimiento</param>
        ///// <returns>Número de filas afectadas</returns>
        private T2 ExecuteAccess<T2>(string procedureName, IEnumerable<PropertyInfo> listaPropiedades, IList<SqlParameter> listParameters = null)
        {
            IDictionary<string, int> retorno = new Dictionary<string, int>();
            IList<SqlParameter> lstParametrosRetorno = new List<SqlParameter>();

            using (SqlConnection conn = new SqlConnection(CreateConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = procedureName, CommandType = CommandType.StoredProcedure, CommandTimeout = 0 })
                {
                    if (listParameters != null)
                    {
                        cmd.Parameters.AddRange(listParameters.ToArray());
                    }

                    conn.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    lstParametrosRetorno.Add(new SqlParameter("filasAfectadas", filasAfectadas));

                    var parametrosDeSalida = listParameters.Where(x => x.Direction == ParameterDirection.Output).ToList();

                    if (parametrosDeSalida != null)
                    {
                        ((List<SqlParameter>)lstParametrosRetorno).AddRange(parametrosDeSalida.ToArray());
                    }

                    return AdapterParametersToDTO<T2>(lstParametrosRetorno, listaPropiedades);
                }
            }
        }

        /// <summary>
        /// Método utilizado para obtener el string de conexión desde el archivo de configuraciones.
        /// </summary>
        /// <returns>String de conexión</returns>
        private string CreateConnectionString()
        {
            return _configurationFile.GetConnectionString("DefaultConnection");
        }

        #endregion
    }
}
