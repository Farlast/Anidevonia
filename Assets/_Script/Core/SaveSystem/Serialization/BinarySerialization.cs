using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace Script.Core.SaveSystem
{
    public class BinarySerialization
    {
        public static bool Save(string savename, SaveData data)
        {
            BinaryFormatter formatter = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            string path = Application.persistentDataPath + "/saves/" + savename + ".save";
            FileStream fileStream = File.Create(path);

            formatter.Serialize(fileStream, data);
            fileStream.Close();
            return true;

        }
        public static SaveData Load(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream fileStream = File.Open(path, FileMode.Open);

            try
            {
                SaveData data = (SaveData)formatter.Deserialize(fileStream);
                fileStream.Close();
                return data;
            }
            catch
            {
                Debug.LogErrorFormat("Fail to load file at {0}", path);
                fileStream.Close();
                return null;
            }
        }
        public static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter;
        }
    }
}
