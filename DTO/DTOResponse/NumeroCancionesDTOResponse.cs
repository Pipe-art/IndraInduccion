using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Discografica.DAL.Properties;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DTO.DTOResponse
{
    public class NumeroCancionesDTOResponse
    {

        public NumeroCancionesDTOResponse()
        {

        }
        public NumeroCancionesDTOResponse(SqlDataReader reader)
        {
            try
            {
                if (reader!=null)
                {
                    this.AliasArtista = Convert.IsDBNull(reader["art_aka"]) ? string.Empty : Convert.ToString(reader["art_aka"]);
                    this.DiscoNombre = Convert.IsDBNull(reader["Nombre Disco"]) ? string.Empty : Convert.ToString(reader["Nombre Disco"]);
                    this.NumeroCanciones = Convert.IsDBNull(reader["Numero de Canciones"]) ? string.Empty : Convert.ToString(reader["Numero de Canciones"]);
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
        [PropertyName("art_aka")]
        public string AliasArtista { get; set; }
        /// <summary>
        /// Disco Nombre
        /// </summary>
        [PropertyName("Nombre Disco")]
        public string DiscoNombre { get; set; }
        /// <summary>
        /// Numero de canciones
        /// </summary>
        [PropertyName("Numero de Canciones")]
        public string NumeroCanciones { get; set; }
    }
}
