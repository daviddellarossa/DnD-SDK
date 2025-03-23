﻿using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Storage;
using DnD.Code.Scripts.Weapons;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class StorageInitializer
    {
        // public static readonly string PathHelper.StoragePath = $"{Common.FolderPath}/{NameHelper.Naming.Storage}";

        public static Storage[] GetAllStorage()
        {
            return Common.GetAllScriptableObjects<Storage>(PathHelper.StoragePath);
        }

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Storage Data")]
        public static void InitializeStorage()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(PathHelper.StoragePath);

                var caseStorage = Common.CreateScriptableObject<Storage>(NameHelper.Storage.Case, PathHelper.StoragePath);
                caseStorage.Name = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Case}";
                var pouchStorage = Common.CreateScriptableObject<Storage>(NameHelper.Storage.Pouch, PathHelper.StoragePath);
                pouchStorage.Name = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Pouch}";
                var quiverStorage = Common.CreateScriptableObject<Storage>(NameHelper.Storage.Quiver, PathHelper.StoragePath);
                quiverStorage.Name = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Quiver}";

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
