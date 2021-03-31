using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Discografica.DAL.Properties;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DTO.DTOResponse
{
    public class CancionesDiscoDTOResponse
    {
        public CancionesDiscoDTOResponse()
        {

        }
        public CancionesDiscoDTOResponse(SqlDataReader reader)
        {
            try
            {
                if (reader != null)
                {
                    this.CodigoDisco = Convert.IsDBNull(reader["dis_codigo"]) ? string.Empty : Convert.ToString(reader["dis_codigo"]);
                    this.DiscoNombre = Convert.IsDBNull(reader["dis_nombre"]) ? string.Empty : Convert.ToString(reader["dis_nombre"]);
                    this.CancionNombre = Convert.IsDBNull(reader["can_nombre"]) ? string.Empty : Convert.ToString(reader["can_nombre"]);
                    this.Alias = Convert.IsDBNull(reader["ART_AKA"]) ? string.Empty : Convert.ToString(reader["ART_AKA"]);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Codigo Disco
        /// </summary>
        [PropertyName("dis_codigo")]
        public string CodigoDisco { get; set; }
        /// <summary>
        /// Disco Nombre
        /// </summary>
        [PropertyName("dis_nombre")]
        public string DiscoNombre { get; set; }
        /// <summary>
        /// Nombre Cancion Disco
        /// </summary>
        [PropertyName("can_nombre")]
        public string CancionNombre { get; set; }
        /// <summary>
        /// Alias Artista
        /// </summary>
        [PropertyName("ART_AKA")]
        public string Alias { get; set; }


    }
}
