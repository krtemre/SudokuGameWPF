using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SudokuGameWPF.Helpers
{
    public static class XmlHelper
    {
        public static bool SaveToXMLFile(string filePath, object obj)
        {
            bool result = false;
            try
            {
                filePath += ".xml";

                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }

                XmlSerializer mySerializer = new XmlSerializer(obj.GetType());
                using (StreamWriter myWriter = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    mySerializer.Serialize(myWriter, obj);
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static T ReadXMLFromSerializedFile<T>(string filePath)
        {
            filePath += ".xml";

            T deserializedObject;
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += (sender, args) =>
            {
                if (args.Severity != XmlSeverityType.Warning)
                    throw new XmlException();

            };
            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                deserializedObject = (T)serializer.Deserialize(reader);
            }
            return deserializedObject;
        }

        public static T LoadFromJSONFile<T>(string path)
        {
            path += ".json";

            string json = File.ReadAllText(path);

            T deserializedObject = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return deserializedObject;
        }

        public static bool SaveToJSONFile(string path, object obj)
        {
            bool result = false;
            try
            {
                path += ".json";

                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                string data = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                });

                File.WriteAllText(path, data, Encoding.UTF8);

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
