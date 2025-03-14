using DnD.Code.Scripts.Characters.Storage;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Weapons;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class StorageInitializer
    {
        public static readonly string StoragePath = $"{Common.FolderPath}/Storage";

        public static Storage[] GetAllStorage()
        {
            return Common.GetAllScriptableObjects<Storage>(StoragePath);
        }

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Storage Data")]
        public static void InitializeStorage()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(StoragePath);

                var caseStorage = Common.CreateScriptableObject<Storage>(NameHelper.Storage.Case, StoragePath);
                caseStorage.Name = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Case}";
                var pouchStorage = Common.CreateScriptableObject<Storage>(NameHelper.Storage.Pouch, StoragePath);
                pouchStorage.Name = $"{nameof(NameHelper.Storage)}.{NameHelper.Storage.Pouch}";
                var quiverStorage = Common.CreateScriptableObject<Storage>(NameHelper.Storage.Quiver, StoragePath);
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
