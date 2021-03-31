using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.Entity
{
    public class Artista
    {
        [Key]
        public string art_codigo { get; set; }
        public string art_cod_verf { get; set; }
        public string art_nombre { get; set; }
        public string art_aka { get; set; }
        public DateTime art_bithday { get; set; }
        public string art_state { get; set; }
        public string con_codigo { get; set; }
        public string rep_codigo { get; set; }

    }
}
