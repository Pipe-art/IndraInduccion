using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.Entity
{
    public class Cancion
    {

        [Key]
        public string can_codigo { get; set; }
        public string can_nombre { get; set; }
        public string can_descripcion { get; set; }
        public string dis_codigo { get; set; }
        
    }
}
