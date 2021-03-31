using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Discografica.DAL.Properties;
using System.Threading.Tasks;

namespace Discografica.DTO.DTOResponse
{
    public class DatosArtistaDTOResponse
    {
        public DatosArtistaDTOResponse()
        {

        }

        public DatosArtistaDTOResponse(SqlDataReader reader)
        {
            try
            {
                if(reader != null)
                {
                    this.NombreArtista = Convert.IsDBNull(reader["art_nombre"]) ? string.Empty : Convert.ToString(reader["art_nombre"]);
                    this.AliasArtista = Convert.IsDBNull(reader["art_aka"]) ? string.Empty : Convert.ToString(reader["art_aka"]);
                    this.NombreDisco = Convert.IsDBNull(reader["dis_nombre"]) ? string.Empty : Convert.ToString(reader["dis_nombre"]);
                    this.Precio = Convert.IsDBNull(reader["dis_precio"]) ? decimal.Zero : Convert.ToDecimal(reader["dis_precio"]);
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
        [PropertyName("art_nombre")]
        public string NombreArtista { get; set; }
        /// <summary>
        /// AKA Artista
        /// </summary>
        [PropertyName("art_aka")]
        public string AliasArtista { get; set; }
        /// <summary>
        /// Nombre Disco
        /// </summary>
        [PropertyName("dis_nombre")]
        public string NombreDisco { get; set; }
        /// <summary>
        /// Precio Disco C/U
        /// </summary>
        [PropertyName("dis_precio")]
        public decimal Precio { get; set; }

     



    }

    
}
