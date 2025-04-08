using System.IO;
using UnityEngine;

namespace Infrastructure.SaveManager
{
    public static class SaveManager
    {
        private static string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");
        
        public static void Save(SaveGameData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
            Debug.Log("Game saved to: " + SavePath);
        }

        public static SaveGameData Load()
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("No save file found");
                return null;
            }

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SaveGameData>(json);
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