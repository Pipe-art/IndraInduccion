using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DTO.Base
{
    public class BasePropertieResponseDTO
    {
        public class BasePropertiesRequestDTO
        {
            /// <summary>
            /// Número de la página en la cual me posiciono.
            /// </summary>
            /// <value>Valor por defecto (1)</value>
            public int PageNumber { get; set; } = 1;

            /// <summary>
            /// Cantidad de registros de la página.
            /// </summary>
            /// <value>Valor por defecto (10)</value>
            public int PageSize { get; set; } = 10000;

            /// <summary>
            /// Ordenamiento de la lista, separado por comas (,)
            /// </summary>
            /// <value>Valor por defecto (Id)</value>
            public string Sort { get; set; } = "Codigo";
        }
    }
}
