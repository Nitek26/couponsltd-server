using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CouponsLtd.Helpers
{
    public static class JsonHelper
    {
        public static T LoadFromJson<T>(string filePath) where T : class
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                var output = JsonConvert.DeserializeObject<T>(json);
                return output;
            }
        }
    }
}
