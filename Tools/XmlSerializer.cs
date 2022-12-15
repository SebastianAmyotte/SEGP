using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Serialization;



namespace SEGP7.Tools
{
    internal static class ObjectSerializer<T>
    {
        // Author: https://www.c-sharpcorner.com/UploadFile/manishkdwivedi/save-a-observablecollection-to-application-storage-in-window/
        // Implemented by: Sebastian Amyotte
        // Not much in the code was changed
        // Description: Serializes an object to a string. but uses XML serialization instead
        // Traditional serialization does not support Dictionaries. Xml does.
        // Usage:
        // String serializedResult = ObjectSerializer<Dictionary<String, String>>.Serialize(object);
        // Dictionary<String, String> deserializedResult = ObjectSerializer<Dictionary<String, String>>.Deserialize(serializedResult);
        public static string ToXml(T value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true,
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                serializer.Serialize(xmlWriter, value);
            }
            return stringBuilder.ToString();
        }

        // Deserialize from xml  
        public static T FromXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T value;
            using (StringReader stringReader = new StringReader(xml))
            {
                object deserialized = serializer.Deserialize(stringReader);
                value = (T)deserialized;
            }

            return value;
        }
    }
}

