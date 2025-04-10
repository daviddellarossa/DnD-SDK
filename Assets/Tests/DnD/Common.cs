using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tests.DnD
{
    public static class Common
    {
        public static T[] GetAllScriptableObjects<T>(string folderPath) where T : ScriptableObject
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            return assets.ToArray();
        }
        
        public static string GetUnexpectedValueLogInfo(string instanceName, string fieldName, object expectedValue)
            => $"{instanceName}: {fieldName} not equal to {expectedValue}.";

        public static string GetNotFoundLogInfo(string instanceTypeName, string instanceName)
            => $"{instanceTypeName} {instanceName} not found.";

    }
}