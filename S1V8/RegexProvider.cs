using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace S1V8
{
    internal class RegexProvider
    {
        public static Dictionary<string, string> GetEmpRgx()
        {
            Dictionary<string, string> DcRegexes = new Dictionary<string, string>();

            PropertyInfo[] properties = typeof(Employee).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                RegularExpressionAttribute regexAttribute = (RegularExpressionAttribute)Attribute.GetCustomAttribute(property, typeof(RegularExpressionAttribute));

                if (regexAttribute != null)
                {
                    string propertyName = property.Name;
                    string regex = regexAttribute.Pattern;

                    DcRegexes.Add(propertyName, regex);
                }
            }
            return DcRegexes;
        }
    }
}
