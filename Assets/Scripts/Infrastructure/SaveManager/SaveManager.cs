using System.IO;
using Infrastructure.SaveManager.Models;
using UnityEngine;
using ProtoBuf;

namespace Infrastructure.SaveManager
{
    public static class SaveManager
    {
        private static string SavePath => Path.Combine(Application.persistentDataPath, "savegame.dat");
        
        public static void Save(SaveGameData data)
        {
            using (var file = File.Create(SavePath))
            {
                Serializer.Serialize(file, data);
            }

            //DeeDeeR.
            Debug.Log("Game saved with Protobuf to: " + SavePath);
        }

        public static SaveGameData Load()
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("No save file found");
                return null;
            }

            using (var file = File.OpenRead(SavePath))
            {
                return Serializer.Deserialize<SaveGameData>(file);
            }
        }
        public static void DeleteSave()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                Debug.Log("Save file deleted.");
            }
        }
    }
}