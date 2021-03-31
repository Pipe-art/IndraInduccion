using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Discografica.DAL.Properties;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DTO.DTOResponse
{
    public class VigenciaContratoDTOResponse
    {
        public VigenciaContratoDTOResponse()
        {

        }
        public VigenciaContratoDTOResponse(SqlDataReader reader)
        {
            try
            {
                if (reader != null)
                {
                    this.NumerosContrato = Convert.IsDBNull(reader["Numero de Contrato"]) ? string.Empty : Convert.ToString(reader["Numero de Contrato"]);
                    this.Descripcion = Convert.IsDBNull(reader["Descripcion"]) ? string.Empty : Convert.ToString(reader["Descripcion"]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Codigo Disco
        /// </summary>
        [PropertyName("Numero de Contrato")]
        public string NumerosContrato { get; set; }
        /// <summary>
        /// Disco Nombre
        /// </summary>
        [PropertyName("Descripcion")]
        public string Descripcion { get; set; }


    }
}
