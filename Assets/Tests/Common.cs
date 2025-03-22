using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tests
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
    }
}