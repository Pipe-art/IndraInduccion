using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Discografica.Resource
{
    public static class Resource
    {
        public static readonly IImmutableList<string> excludedPropertiesToMapping = ImmutableList.Create("PageNumber", "PageSize", "Sort");
        /// <summary>
        /// Tamaño de pagina
        /// </summary>
        public static readonly int PAGE_SIZE = 10000;

        /// <summary>
        /// Año para fechas por defecto
        /// </summary>
        public static readonly int DEFAULT_YEAR = 3000;

        /// <summary>
        /// Mes para fechas por defecto
        /// </summary>
        public static readonly int DEFAULT_MONTH = 12;

        /// <summary>
        /// Dia para fechas por defecto
        /// </summary>
        public static readonly int DEFAULT_DAY = 31;

        /// <summary>
        /// Http Response code 201
        /// </summary>
        public static readonly int STATUS_CODE_201 = 201;

        /// <summary>
        /// Http Response code 400
        /// </summary>
        public static readonly int STATUS_CODE_400 = 400;

        /// <summary>
        /// Http Response code 403
        /// </summary>
        public static readonly int STATUS_CODE_403 = 403;

        /// <summary>
        /// Http Response code 500
        /// </summary>
        public static readonly int STATUS_CODE_500 = 500;


        public static readonly string ConsultaDatosArtista = "SP_DATOS_ARTISTA";
        public static readonly string ConsultaDatosRepresentante = "SP_DATOS_REPRESENTANTE";
        public static readonly string CancionesArtista = "SP_CANCIONES_ARTISTA_AKA";
        public static readonly string CancionesDisco = "SP_CANCIONES_ARTISTA_NDISCO";
        public static readonly string NumeroCanciones = "SP_NUMERO_CANCIONES";
        public static readonly string VigenciaContrato = "SP_VIGENCIA_CONTRATO";
        public static readonly string DatosDobles = "SP_DOBLE_DATOS";



    }
}
