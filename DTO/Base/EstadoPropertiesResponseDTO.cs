using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DTO.Base
{
    public class EstadoPropertiesResponseDTO
    {

        /// <summary>
        /// Codigo de Retorno devuelto de la ejecucion del SP ejecutado.
        /// </summary>
        [PropertyName("resultado")]
        public int CodigoRetorno { get; set; }


        /// <summary>
        /// Filas afectadas durante la ejecuccion del procedimiento almacenado.
        /// </summary>
        [PropertyName("filasAfectadas")]
        public int FilasAfectadas { get; set; }



    }
}
