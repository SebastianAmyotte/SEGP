using Newtonsoft.Json;
using System.Text;

namespace SEGP7.Tools
{
    // Author: Sebastian Amyotte
    // Description: Traditional serializer that helps abstract code away
    // Makes the code in the rest of the project look like..
    // Usage:
    // String serializedResult = Serializer.Serialize(object);
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
