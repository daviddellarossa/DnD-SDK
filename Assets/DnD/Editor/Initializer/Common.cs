using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Codice.Client.Common;
using DnD.Code.Scripts.Common;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DnD.Editor.Initializer
{
    public static class Common
    {
        // public static readonly string FolderPath = "Assets/DnD/Code/Instances";
        
        public static T CreateScriptableObject<T>(string fileName, string folderPath) where T : ScriptableObject
        {
            string assetPath = Path.Combine(folderPath, fileName + ".asset");

            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, assetPath);
                Debug.Log($"{fileName} created.");
            }
            else
            {
                Debug.LogWarning($"{fileName} already exists.");
            }

            return asset;
        }
        
        public static T CreateScriptableObjectAndAddToObject<T>(string scriptableObjectName, Object parent) where T : ScriptableObject
        {
            if (parent == null)
            {
                throw new Exception($@"Parent object is null");
            }

            T asset = null;
            var doesNotContain = !ContainsScriptableObjectByName(parent, scriptableObjectName);
            if (doesNotContain)
            {
                asset = ScriptableObject.CreateInstance<T>();
                asset.name = scriptableObjectName;
                AssetDatabase.AddObjectToAsset(asset, parent);
                
                Debug.Log($"{scriptableObjectName} created.");
            }
            else
            {
                Debug.LogWarning($"{scriptableObjectName} already exists.");
            }

            return asset;
        }
        
        public static bool ContainsScriptableObjectByName(Object parentAsset, string scriptableObjectName)
        {
            if (parentAsset == null) return false;

            string assetPath = AssetDatabase.GetAssetPath(parentAsset);
            if (string.IsNullOrEmpty(assetPath)) return false;

            Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            return subAssets.Any(subAsset => subAsset.name == scriptableObjectName);
        }
        public static bool ContainsScriptableObject<T>(Object parentAsset) where T : ScriptableObject
        {
            if (parentAsset == null) return false;

            string assetPath = AssetDatabase.GetAssetPath(parentAsset);
            if (string.IsNullOrEmpty(assetPath)) return false;

            Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            return subAssets.OfType<T>().Any();
        }
        
        public static void EnsureFolderExists(string folderPath, bool recursively = false)
        {
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                string parentFolder = Path.GetDirectoryName(folderPath);
                string newFolder = Path.GetFileName(folderPath);

                if (recursively && !string.IsNullOrEmpty(parentFolder) && !AssetDatabase.IsValidFolder(parentFolder))
                {
                    EnsureFolderExists(parentFolder, true); // Recursively create parent folders if needed
                }

                AssetDatabase.CreateFolder(parentFolder, newFolder);
                AssetDatabase.Refresh();
            }
        }
        
        public static T GetExistingScriptableObject<T>(string folderPath) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });

            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return null;
        }
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
        
        public static T[] GetAllScriptableObjectsImplementingInterface<T,I>(string folderPath) 
            where T : ScriptableObject
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null && typeof(I).IsAssignableFrom(asset.GetType()))
                {
                    assets.Add(asset);
                }
            }

            return assets.ToArray();
        }
    }
}