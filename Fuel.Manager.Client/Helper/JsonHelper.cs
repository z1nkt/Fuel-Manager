using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel.Manager.Client.Helper
{
    public static class JsonHelper
    {
        public static string DictionaryToJson(IDictionary<string, string> dict)
        {
            var x = dict.Select(d =>
                string.Format("\"{0}\": \"{1}\"", d.Key, string.Join(",", d.Value)));
            return "{" + string.Join(",", x) + "}";
        }
    }
}
