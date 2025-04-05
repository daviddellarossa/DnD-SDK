
using DnD.Code.Scripts;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using static DnD.Code.Scripts.Helpers.NameHelper.NameHelper;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class DiceInitializer
    {
        public static Die[] GetAllDice()
        {
            return ScriptableObjectHelper.GetAllScriptableObjects<Die>(PathHelper.DicePath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Dice Data")]
        public static void InitializeDice()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                FileSystemHelper.EnsureFolderExists(PathHelper.DicePath);
            
                var d1 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D1, PathHelper.DicePath);
                d1.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D1}";
                d1.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D1}.{NameHelper.Naming.Description}";
                d1.NumOfFaces = 1;
                EditorUtility.SetDirty(d1);

                
                var d3 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D3, PathHelper.DicePath);
                d3.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D3}";
                d3.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D3}.{NameHelper.Naming.Description}";
                d3.NumOfFaces = 3;
                EditorUtility.SetDirty(d3);

                var d4 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D4, PathHelper.DicePath);
                d4.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D4}";
                d4.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D4}.{NameHelper.Naming.Description}";
                d4.NumOfFaces = 4;
                EditorUtility.SetDirty(d4);

                var d6 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D6, PathHelper.DicePath);
                d6.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D6}";
                d6.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D6}.{NameHelper.Naming.Description}";
                d6.NumOfFaces = 6;
                EditorUtility.SetDirty(d6);

                var d8 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D8, PathHelper.DicePath);
                d8.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D8}";
                d8.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D8}.{NameHelper.Naming.Description}";
                d8.NumOfFaces = 8;
                EditorUtility.SetDirty(d8);

                var d10 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D10, PathHelper.DicePath);
                d10.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D10}";
                d10.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D10}.{NameHelper.Naming.Description}";
                d10.NumOfFaces = 10;
                EditorUtility.SetDirty(d10);

                var d12 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D12, PathHelper.DicePath);
                d12.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D12}";
                d12.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D12}.{NameHelper.Naming.Description}";
                d12.NumOfFaces = 12;
                EditorUtility.SetDirty(d12);

                var d20 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D20, PathHelper.DicePath);
                d20.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D20}";
                d20.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D20}.{NameHelper.Naming.Description}";
                d20.NumOfFaces = 20;
                EditorUtility.SetDirty(d20);

                var d100 = ScriptableObjectHelper.CreateScriptableObject<Die>(Dice.D100, PathHelper.DicePath);
                d100.DisplayName = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D100}";
                d100.DisplayDescription = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D100}.{NameHelper.Naming.Description}";
                d100.NumOfFaces = 100;
                EditorUtility.SetDirty(d100);

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
