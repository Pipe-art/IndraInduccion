using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discografica.DAL.Properties
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class PropertyNameAttribute : Attribute
    {
        private readonly string _mappingName;

        public string MappingName
        {
            get
            {
                return _mappingName;
            }
        }

        public PropertyNameAttribute(string pNombre)
        {
            _mappingName = pNombre;
        }

    }
}
