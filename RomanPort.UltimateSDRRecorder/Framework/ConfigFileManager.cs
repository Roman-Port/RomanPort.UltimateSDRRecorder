using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RomanPort.UltimateSDRRecorder.Framework
{
    public static class ConfigFileManager
    {
        public const string SAVEKEY_DVR = "DVR";
        public const string SAVEKEY_APP = "PLUGIN_MAIN";
        
        public static T LoadConfigFile<T>(string key)
        {
            string path = GetFilenameFromKey(key);
            if(File.Exists(path))
            {
                //Deserialize
                XmlSerializer ser = new XmlSerializer(typeof(T));
                T config;
                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    config = (T)ser.Deserialize(reader);
                }
                return config;
            } else
            {
                return default(T);
            }
        }

        public static void SaveConfigFile<T>(string key, T config)
        {
            //Serialize (thanks https://stackoverflow.com/questions/4123590/serialize-an-object-to-xml)
            XmlSerializer ser = new XmlSerializer(typeof(T));
            string xml;
            using (var sww = new FileStream(GetFilenameFromKey(key), FileMode.Create))
            {
                using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings
                {
                    Encoding = Encoding.ASCII
                }))
                {
                    ser.Serialize(writer, config);
                    xml = sww.ToString();
                }
            }
        }

        private static string GetFilenameFromKey(string key)
        {
            return "RomanPort.UltimateSDRRecorder.Config." + key + ".xml";
        }
    }
}
