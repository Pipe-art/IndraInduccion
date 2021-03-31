using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Discografica.Class
{
    public class Parametros
    {

        /// <summary>
        /// Propiedad de retorno de la lista de parametros 
        /// inicializados
        /// </summary>
        public IList<SqlParameter> ListaSqlParam { get; }

        /// <summary>
        /// Constructor por defecto que inicializa nueva instacia de la lista de parametros de encontranse nula
        /// </summary>
        public Parametros()
        {
            ListaSqlParam = ListaSqlParam ?? new List<SqlParameter>();
        }

        /// <summary>
        /// Constructor sobrecargado, que inicia 
        /// </summary>
        /// <param name="pNombreParametro">Nombre del parametro</param>
        /// <param name="pTipoVariableSql">Tipo Variable del procedimiento</param>
        /// <param name="pValorDelParametro">Valor de asignación al parametro</param>
        public Parametros(string pNombreParametro, SqlDbType pTipoVariableSql, object pValorDelParametro = null, ParameterDirection direccionParametro = ParameterDirection.Input)
        {
            ListaSqlParam = ListaSqlParam ?? new List<SqlParameter>();
            ListaSqlParam.Add(new SqlParameter(pNombreParametro, pTipoVariableSql) { Value = pValorDelParametro ?? null, Direction = direccionParametro });
        }

        /// <summary>
        /// Método que agrega un nuevo parametro a la lista de parametros sql
        /// </summary>
        /// <param name="pNombreParametro">Nombre del parametro</param>
        /// <param name="pTipoVariableSql">Tipo Variable del procedimiento</param>
        /// <param name="pValorDelParametro">Valor de asignación al parametro</param>
        public void AddSqlParam(string pNombreParametro, SqlDbType pTipoVariableSql, object pValorDelParametro = null, ParameterDirection direccionParametro = ParameterDirection.Input)
        {
            ListaSqlParam.Add(new SqlParameter(pNombreParametro, pTipoVariableSql) { Value = pValorDelParametro ?? null, Direction = direccionParametro });
        }

    }
}
