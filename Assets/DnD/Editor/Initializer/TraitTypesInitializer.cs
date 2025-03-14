using DnD.Code.Scripts.Common;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class TraitTypesInitializer
    {
        public static readonly string TraitTypesPath = $"{Common.FolderPath}/{NameHelper.Naming.TraitTypes}";

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Type Traits Data")]
        public static void InitializeTypeTraits()
        {
            Common.EnsureFolderExists(TraitTypesPath);
            
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(TraitTypesPath);
                

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