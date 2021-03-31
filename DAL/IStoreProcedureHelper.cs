using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DAL
{
    public interface IStoreProcedureHelper<T>
    {
        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado y 
        /// obtener un listado de objetos en base a una lista de parámetros definidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="listParameters">Lista de parametros (Entrada/Salida) opcional, para ejecutar el procedimiento almacenado</param>
        /// <returns>Lista adaptada a una entidad DTO</returns>
        IList<T2> ExecuteReader<T2>(string procedureName, IList<SqlParameter> listParameters = null);

        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado,
        /// y obtener un listado de objetos en base a una entidad, desde la cual
        /// se utilizan los PropertyAtributeName definidos en esta, en conjunto con sus valores 
        /// como parámetros de ENTRADA para el procedimiento almacenado.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="entityToUse">Entidad con los PropertyAttributeName</param>
        /// <returns>Lista adaptada a una entidad DTO</returns>
        IList<T2> ExecuteReaderWhitAutomaticParameters<T2>(string procedureName, dynamic entityToUse);

        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado que afecta filas dentro de la base de datos,
        /// en base a una lista de parámetros definidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="listParameters">Lista de parámetros (Entrada/Salida) opcional, para ejecutar el procedimiento almacenado</param>
        /// <returns>Número de filas afectadas</returns>
        T2 ExecuteNonQuery<T2>(string procedureName, IList<SqlParameter> listParameters = null);

        /// <summary>
        /// Método utilizado para ejecutar un procedimiento almacenado que afecta filas dentro de la 
        /// base de datos, base a una entidad desde la cual se utilizan los PropertyAtributeName 
        /// definidos en esta, en conjunto con sus valores como parámetros de ENTRADA para el procedimiento almacenado.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="entityToUse">Entidad con los PropertyAttributeName</param>
        /// <returns>Número de filas afectadas</returns>
        T2 ExecuteNonQueryWithAutomaticParameters<T2>(string procedureName, dynamic entityToUse);
    }
}
