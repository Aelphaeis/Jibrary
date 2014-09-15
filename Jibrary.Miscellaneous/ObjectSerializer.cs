using System;
using System.IO;
using System.Xml.Serialization;
namespace Jibrary.Miscellaneous
{
    public static class StringSerializer
    {
        public static String Serialize(Object obj)
        {
            using(StringWriter writer = new StringWriter())
            {
                new XmlSerializer(obj.GetType()).Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T Deserialize<T>(String xml)
        {
            using(TextReader reader = new StringReader(xml))
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        }
    }
}
