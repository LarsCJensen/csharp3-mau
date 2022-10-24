using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Utilities
{
    public static class Serializer
    {
        public static void BinaryFileSerialize<T>(string path, T obj)
        {
            FileStream fileStream = null;
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    throw new SerializerException($"Directory {Path.GetDirectoryName(path)} was not found!");
                }
                else
                {
                    fileStream = new FileStream(path, FileMode.Create);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, obj);
                }
            }
            // Make sure filestream is closed, but let error go up the callstack
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
        public static T BinaryFileDeSerialize<T>(string path)
        {
            FileStream fileStream = null;
            // I used this form because it was used in examples, but I would probably prefer to let errors fall through
            Object obj = null;
            try
            {
                if (!File.Exists(path))
                {
                    throw new SerializerException($"File {path} was not found!");
                }
                else
                {
                    fileStream = new FileStream(path, FileMode.Open);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    obj = binaryFormatter.Deserialize(fileStream);
                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return (T)obj;
        }
        public static void XmlFileSerialize<T>(string path, T obj)
        {
            // I used this form because it was used in examples, but I would probably prefer to let errors fall through
            TextWriter writer = null;
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    throw new SerializerException($"Directory {Path.GetDirectoryName(path)} was not found!");
                }
                else
                {
                    writer = new StreamWriter(path);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(writer, obj);
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        public static T XmlFileDeSerialize<T>(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            // I used this form because it was used in examples, but I would probably prefer to let errors fall through
            Object obj = null;
            TextReader reader = null;
            try
            {
                if (!File.Exists(path))
                {
                    throw new SerializerException($"File {path} was not found!");
                }
                reader = new StreamReader(path);
                obj = (T)xmlSerializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return (T)obj;
        }
    }
}
