using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Discografica.DAL.Properties;
using System.Threading.Tasks;

namespace Discografica.DTO.DTOResponse
{
    public class DatosRepresentanteDTOResponse
    {
        public DatosRepresentanteDTOResponse()
        {

        }
        public DatosRepresentanteDTOResponse(SqlDataReader reader)
        {

            try
            {
                if (reader != null)
                {
                    this.RutRepresentante = Convert.IsDBNull(reader["Rut Representante"]) ? string.Empty : Convert.ToString(reader["Rut Representante"]);
                    this.NombreRepresentante = Convert.IsDBNull(reader["Nombre Representante"]) ? string.Empty : Convert.ToString(reader["Nombre Representante"]);
                    this.Ciudad = Convert.IsDBNull(reader["Ciudad"]) ? string.Empty : Convert.ToString(reader["Ciudad"]);
                    this.Bithday = Convert.IsDBNull(reader["Fecha Nacimiento"]) ? DateTime.MinValue : Convert.ToDateTime(reader["Fecha Nacimiento"]);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        /// <summary>
        /// Nombre Artista
        /// </summary>
        [PropertyName("Rut Representante")]
        public string RutRepresentante { get; set; }
        /// <summary>
        /// AKA Artista
        /// </summary>
        [PropertyName("Nombre Representante")]
        public string NombreRepresentante { get; set; }
        /// <summary>
        /// Nombre Disco
        /// </summary>
        [PropertyName("Ciudad")]
        public string Ciudad { get; set; }
        /// <summary>
        /// Precio Disco C/U
        /// </summary>
        [PropertyName("Fecha Nacimiento")]
        public DateTime Bithday { get; set; }


    }
}
