using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.Entity
{
    public class Disco
    {

        [Key]
        public string dis_codigo { get; set; }
        public string dis_nombre { get; set; }
        public string dis_descripcion { get; set; }
        public decimal dis_precio { get; set; }
        public string art_codigo { get; set; }

    }
}
