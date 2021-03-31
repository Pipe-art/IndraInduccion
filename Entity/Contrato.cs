using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.Entity
{
    public class Contrato
    {

        [Key]
        public string con_codigo { get; set; }
        public string con_vigente { get; set; }
        public string con_descripcion { get; set; }
        public string art_codigo { get; set; }

    }
}
