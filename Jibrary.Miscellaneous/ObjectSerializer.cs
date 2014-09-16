using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;
using System.Text;
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

        public static String DataContractSerialize(Object obj)
        {
            using(StringWriter sw = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true }))
            {
                new DataContractSerializer(obj.GetType()).WriteObject(writer, obj);
                writer.Flush();
                return sw.ToString();
            }
        }

        public static T DataContractDeserialize<T>(String xml)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
                return (T)new DataContractSerializer(typeof(T)).ReadObject(reader);
        }
    }

}
