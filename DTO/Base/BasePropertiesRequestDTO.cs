using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DTO.Base
{
    public class BasePropertiesRequestDTO
    {

        [PropertyName("resultado")]
        public int CodigoRetorno { get; set; }

        /// <summary>
        /// Cuerpo a retornar en caso de ser necesario
        /// </summary>
        [PropertyName("cuerpoRetorno")]
        public string CuerpoRetorno { get; set; }

        /// <summary>
        /// Filas afectadas durante la ejecuccion del procedimiento almacenado.
        /// </summary>
        [PropertyName("filasAfectadas")]
        public int FilasAfectadas { get; set; }

        /// <summary>
        /// Resultado obtenidos a ser devueltos
        /// </summary>
        [PropertyName("Resultado")]
        public dynamic Resultado { get; set; }

    }
}
