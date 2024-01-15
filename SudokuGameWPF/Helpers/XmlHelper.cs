using System;
using System.IO;
using System.Xml.Serialization;

namespace SudokuGameWPF.Helpers
{
    public static class XmlHelper
    {
        public static bool SaveToXML(string filePath, object obj, Type objectType)
        {
            bool result = false;
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }

                XmlSerializer mySerializer = new XmlSerializer(objectType);
                StreamWriter myWriter = new StreamWriter(filePath);
                mySerializer.Serialize(myWriter, obj);
                myWriter.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static object LoadFromXML(Type type, string filePath)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                FileStream stream = new FileStream(filePath, FileMode.Open);
                return xmlSerializer.Deserialize(stream);
            }
            catch (Exception)
            {
            }
            return Activator.CreateInstance(type);
        }
    }
}
