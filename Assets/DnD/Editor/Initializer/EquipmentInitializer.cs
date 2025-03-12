using Codice.Client.BaseCommands.Acl;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment.Coins;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class EquipmentInitializer
    {
        public static readonly string EquipmentPath = $"{Common.FolderPath}/Equipment";
        public static readonly string EquipmentCoinsPath = $"{EquipmentPath}/Coins";
        public static readonly string EquipmentToolsPath = $"{EquipmentPath}/Tools";


        public static CoinValue[] GetAllCoinValues()
        {
            return Common.GetAllScriptableObjects<CoinValue>(EquipmentCoinsPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initialize Equipment Data")]
        public static void InitializeEquipment()
        {
            Common.EnsureFolderExists(EquipmentPath);
            InitializeCoins();
            InitializeTools();
        }

        private static void InitializeCoins()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(EquipmentCoinsPath);

                {
                    var copperPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.CopperPiece, EquipmentCoinsPath);
                    copperPiece.Name = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.CopperPiece}";
                    copperPiece.Abbreviation = "CP";
                    copperPiece.Value = 1;
                }

                {
                    var silverpiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.SilverPiece, EquipmentCoinsPath);
                    silverpiece.Name = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.SilverPiece}";
                    silverpiece.Abbreviation = "SP";
                    silverpiece.Value = 10;
                }

                {
                    var electrumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.ElectrumPiece, EquipmentCoinsPath);
                    electrumPiece.Name = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.ElectrumPiece}";
                    electrumPiece.Abbreviation = "EP";
                    electrumPiece.Value = 50;
                }

                {
                    var goldPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.GoldPiece, EquipmentCoinsPath);
                    goldPiece.Name = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.GoldPiece}";
                    goldPiece.Abbreviation = "GP";
                    goldPiece.Value = 100;
                }

                {
                    var platinumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.PlatinumPiece, EquipmentCoinsPath);
                    platinumPiece.Name = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.PlatinumPiece}";
                    platinumPiece.Abbreviation = "PP";
                    platinumPiece.Value = 1000;
                }
            
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void InitializeTools()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(EquipmentToolsPath);
                
                
                
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