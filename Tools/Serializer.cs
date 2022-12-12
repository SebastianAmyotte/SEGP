using Newtonsoft.Json;
using System.Text;

namespace SEGP7.Tools
{
    
    public class Serializer
    {
        public String Serialize(Object toSerialize)
        {
            StringBuilder sb = new System.Text.StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonTextWriter jsw = new JsonTextWriter(sw);
            JsonSerializer js = new JsonSerializer();
            js.Serialize(jsw, toSerialize);
            return sb.ToString();
        }
    }
}
