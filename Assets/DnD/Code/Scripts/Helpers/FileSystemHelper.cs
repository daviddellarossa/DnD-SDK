using System.IO;
using UnityEditor;

namespace DnD.Code.Scripts.Helpers
{
    public static class FileSystemHelper
    {
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
    }
}