using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.Entity
{
    public class Representante
    {

        [Key]
        public string rep_codigo { get; set; }
        public string rep_cod_verf { get; set; }
        public string rep_nombre { get; set; }
        public DateTime rep_bithday { get; set; }
        public string rep_state { get; set; }

    }
}
