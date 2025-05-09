﻿using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Storage;
using Infrastructure.Helpers;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class StorageInitializer
    {
        // public static readonly string PathHelper.StoragePath = $"{Common.FolderPath}/{NameHelper.Naming.Storage}";

        public static Storage[] GetAllStorage()
        {
            return ScriptableObjectHelper.GetAllScriptableObjects<Storage>(PathHelper.StoragePath);
        }

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Storage Data")]
        public static void InitializeStorage()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                FileSystemHelper.EnsureFolderExists(PathHelper.StoragePath);

                {
                    var caseStorage = ScriptableObjectHelper.CreateScriptableObject<Storage>(NameHelper.Storage.Case, PathHelper.StoragePath);
                    caseStorage.DisplayName = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Case}";
                    caseStorage.DisplayDescription = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Case}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(caseStorage);
                }
                {
                    var pouchStorage = ScriptableObjectHelper.CreateScriptableObject<Storage>(NameHelper.Storage.Pouch, PathHelper.StoragePath);
                    pouchStorage.DisplayName = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Pouch}";
                    pouchStorage.DisplayDescription = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Pouch}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(pouchStorage);
                }
                {
                    var quiverStorage = ScriptableObjectHelper.CreateScriptableObject<Storage>(NameHelper.Storage.Quiver, PathHelper.StoragePath);
                    quiverStorage.DisplayName = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Quiver}";
                    quiverStorage.DisplayDescription = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Quiver}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(quiverStorage);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}
