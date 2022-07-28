using System;
using System.Text;
using System.IO;
using UnityEngine;

namespace Script.Core.SaveSystem
{
    public class JsonSerialization
    {
        public static SaveData Load(int saveIndex)
        {
            using (StreamReader streamReader = new($"Save-{saveIndex}.json"))
            {
                string rawData = streamReader.ReadToEnd();
                /*
                //decode
                var b64 = Convert.FromBase64String(rawData);
                var json = Encoding.UTF8.GetString(b64);
                */
                SaveData data = JsonUtility.FromJson<SaveData>(rawData);
                return data;
            }
        }

        public static void Save(SaveData data, int saveIndex)
        {
            var jsonString = JsonUtility.ToJson(data);
            /*
            //encode
            var texBytes = Encoding.UTF8.GetBytes(jsonString);
            var b64 = Convert.ToBase64String(texBytes);
            */
            using(StreamWriter streamWriter = new StreamWriter($"Save-{saveIndex}.json"))
            {
                streamWriter.Write(jsonString);
            }
        }
    }
}
