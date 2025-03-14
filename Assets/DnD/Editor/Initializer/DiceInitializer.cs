
using DnD.Code.Scripts;
using DnD.Code.Scripts.Common;
using UnityEditor;
using static DnD.Code.Scripts.Common.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class DiceInitializer
    {
        public static readonly string DicePath = $"{Common.FolderPath}/Dice";

        public static Die[] GetAllDice()
        {
            return Common.GetAllScriptableObjects<Die>(DicePath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Dice Data")]
        public static void InitializeDice()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(DicePath);
            
                var d1 = Common.CreateScriptableObject<Die>(Dice.D1, DicePath);
                d1.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D1}";
                d1.NumOfFaces = 1;

                var d3 = Common.CreateScriptableObject<Die>(Dice.D3, DicePath);
                d3.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D3}";
                d3.NumOfFaces = 3;

                var d4 = Common.CreateScriptableObject<Die>(Dice.D4, DicePath);
                d4.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D4}";
                d4.NumOfFaces = 4;

                var d6 = Common.CreateScriptableObject<Die>(Dice.D6, DicePath);
                d6.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D6}";
                d6.NumOfFaces = 6;

                var d8 = Common.CreateScriptableObject<Die>(Dice.D8, DicePath);
                d8.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D8}";
                d8.NumOfFaces = 8;

                var d10 = Common.CreateScriptableObject<Die>(Dice.D10, DicePath);
                d10.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D10}";
                d10.NumOfFaces = 10;

                var d12 = Common.CreateScriptableObject<Die>(Dice.D12, DicePath);
                d12.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D12}";
                d12.NumOfFaces = 12;

                var d20 = Common.CreateScriptableObject<Die>(Dice.D20, DicePath);
                d20.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D20}";
                d20.NumOfFaces = 20;

                var d100 = Common.CreateScriptableObject<Die>(Dice.D100, DicePath);
                d100.Name = $"{nameof(NameHelper.Dice)}.{NameHelper.Dice.D100}";
                d100.NumOfFaces = 100;


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
