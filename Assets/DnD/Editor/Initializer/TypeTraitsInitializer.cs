﻿using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class TypeTraitsInitializer
    {
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Type Traits Data")]
        public static void InitializeTypeTraits()
        {
            FileSystemHelper.EnsureFolderExists(PathHelper.TypeTraitsPath);
            
            try
            {
                AssetDatabase.StartAssetEditing();
            
                FileSystemHelper.EnsureFolderExists(PathHelper.TypeTraitsPath);
                

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