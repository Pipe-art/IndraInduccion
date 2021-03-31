using System;
using System.Collections.Generic;
using Discografica.Resource;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Discografica.Utilities
{
    public static class Utilities
    {

        /// <summary>
        /// Metodo utilizado para convertir un tipo de dato de C# a SqlDbType.
        /// </summary>
        /// <param name="tipoDato">Tipo de Dato C#</param>
        /// <returns>Tipo de Dato SqlDbType</returns>
        public static SqlDbType ConvertirTipoDeDatoASqlDbType(string tipoDato)
        {
            if (tipoDato != null)
            {
                switch (tipoDato.ToLower())
                {
                    case "int32":
                        {
                            return SqlDbType.Int;
                        }
                    case "string":
                        {
                            return SqlDbType.Text;
                        }
                    case "datetime":
                        {
                            return SqlDbType.DateTime;
                        }
                    case "char":
                        {
                            return SqlDbType.Char;
                        }
                    case "decimal":
                        {
                            return SqlDbType.Decimal;
                        }
                    case "double":
                        {
                            return SqlDbType.Float;
                        }
                    case "int16":
                        {
                            return SqlDbType.SmallInt;
                        }
                    case "byte[]":
                        {
                            return SqlDbType.Binary;
                        }
                    case "byte":
                        {
                            return SqlDbType.TinyInt;
                        }
                    default:
                        return SqlDbType.VarChar;
                }
            }
            else
            {
                return SqlDbType.VarChar;
            }
        }

        /// <summary>
        /// Metodo utilizado para convertir un tipo de dato SQL a c#
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static dynamic ConvertirObjetoAValorConTipoDeDato(PropertyInfo propertyInfo, object valor)
        {
            if (propertyInfo != null)
            {
                switch (propertyInfo.PropertyType.Name.ToLower())
                {
                    case "int32":
                        {
                            return Convert.ToInt32(valor);
                        }
                    case "string":
                        {
                            return Convert.ToString(valor);
                        }
                    case "datetime":
                        {
                            return Convert.ToDateTime(valor);
                        }
                    case "char":
                        {
                            return Convert.ToString(valor);
                        }
                    case "decimal":
                        {
                            return Convert.ToDecimal(valor);
                        }
                    case "double":
                        {
                            return Convert.ToDouble(valor);
                        }
                    case "int16":
                        {
                            return Convert.ToInt16(valor);
                        }
                    case "byte[]":
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bf.Serialize(ms, valor);
                                return ms.ToArray();
                            }
                        }
                    case "image":
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bf.Serialize(ms, valor);
                                return ms.ToArray();
                            }
                        }
                    case "byte":
                        {
                            return Convert.ToByte(valor);
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo utilizado para convertir un tipo de dato SQL a c#
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static dynamic ConvertirObjetoAValorConTipoDeDato(string tipo, object valor)
        {
            if (tipo != null)
            {
                switch (tipo.ToLower())
                {
                    case "int32":
                        {
                            return Convert.IsDBNull(valor) ? 0 : Convert.ToInt32(valor);
                        }
                    case "int":
                        {
                            return Convert.IsDBNull(valor) ? 0 : Convert.ToInt32(valor);
                        }
                    case "string":
                        {
                            return Convert.IsDBNull(valor) ? string.Empty : Convert.ToString(valor).Trim();
                        }
                    case "datetime":
                        {
                            return Convert.IsDBNull(valor) ? new DateTime(Resource.Resource.DEFAULT_DAY, Resource.Resource.DEFAULT_MONTH, Resource.Resource.DEFAULT_DAY) : Convert.ToDateTime(valor);
                        }
                    case "date":
                        {
                            return Convert.IsDBNull(valor) ? new DateTime(Resource.Resource.DEFAULT_YEAR, Resource.Resource.DEFAULT_MONTH, Resource.Resource.DEFAULT_DAY) : Convert.ToDateTime(valor);
                        }
                    case "char":
                        {
                            return Convert.IsDBNull(valor) ? string.Empty : Convert.ToString(valor).Trim();
                        }
                    case "decimal":
                        {
                            return Convert.IsDBNull(valor) ? default(decimal) : Convert.ToDecimal(valor);
                        }
                    case "double":
                        {
                            return Convert.IsDBNull(valor) ? default(double) : Convert.ToDouble(valor);
                        }
                    case "int16":
                        {
                            return Convert.IsDBNull(valor) ? default(Int16) : Convert.ToInt16(valor);
                        }
                    case "byte[]":
                        {
                            if (Convert.IsDBNull(valor))
                            {
                                return default(Byte[]);
                            }
                            BinaryFormatter bf = new BinaryFormatter();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bf.Serialize(ms, valor);
                                return ms.ToArray();
                            }
                        }
                    case "image":
                        {
                            if (Convert.IsDBNull(valor))
                            {
                                return default(Byte[]);
                            }
                            BinaryFormatter bf = new BinaryFormatter();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bf.Serialize(ms, valor);
                                return ms.ToArray();
                            }
                        }
                    case "byte":
                        {
                            return Convert.IsDBNull(valor) ? default(Byte) : Convert.ToByte(valor);
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deja el valor empty por defecto en un string null o con espacio en blanco
        /// </summary>
        /// <param name="valor">string null o con espacio</param>
        /// <returns>el mismo valor o string.empty</returns>
        public static string ValorDbEmpty(this string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? string.Empty : valor;
        }

    }
}
