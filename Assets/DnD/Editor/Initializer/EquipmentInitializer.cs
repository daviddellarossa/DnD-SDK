using Codice.Client.BaseCommands.Acl;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Helpers.PathHelper;
using UnityEditor;
using static DnD.Code.Scripts.Helpers.NameHelper.NameHelper;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class EquipmentInitializer
    {
        // public static readonly string EquipmentPath = $"{Common.FolderPath}/{NameHelper.Naming.Equipments}";
        // public static readonly string PathHelper.Equipments.EquipmentCoinsPath = $"{EquipmentPath}/{NameHelper.Naming.Coins}";
        // public static readonly string EquipmentToolsPath = $"{EquipmentPath}/{NameHelper.Naming.Tools}";


        public static CoinValue[] GetAllCoinValues()
        {
            return Common.GetAllScriptableObjects<CoinValue>(PathHelper.Equipments.EquipmentCoinsPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Equipment Data")]
        public static void InitializeEquipment()
        {
            Common.EnsureFolderExists(PathHelper.Equipments.EquipmentPath);
            InitializeCoins();
            InitializeTools();
        }

        private static void InitializeCoins()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(PathHelper.Equipments.EquipmentCoinsPath);

                {
                    var copperPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.CopperPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    copperPiece.DisplayName = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.CopperPiece}";
                    copperPiece.DisplayDescription = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.CopperPiece}.{NameHelper.Naming.Description}";
                    copperPiece.Abbreviation = "CP";
                    copperPiece.Value = 1;
                }

                {
                    var silverpiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.SilverPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    silverpiece.DisplayName = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.SilverPiece}";
                    silverpiece.DisplayDescription = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.SilverPiece}.{NameHelper.Naming.Description}";
                    silverpiece.Abbreviation = "SP";
                    silverpiece.Value = 10;
                }

                {
                    var electrumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.ElectrumPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    electrumPiece.DisplayName = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.ElectrumPiece}";
                    electrumPiece.DisplayDescription = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.ElectrumPiece}.{NameHelper.Naming.Description}";
                    electrumPiece.Abbreviation = "EP";
                    electrumPiece.Value = 50;
                }

                {
                    var goldPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.GoldPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    goldPiece.DisplayName = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.GoldPiece}";
                    goldPiece.DisplayDescription = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.GoldPiece}.{NameHelper.Naming.Description}";
                    goldPiece.Abbreviation = "GP";
                    goldPiece.Value = 100;
                }

                {
                    var platinumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.PlatinumPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    platinumPiece.DisplayName = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.PlatinumPiece}";
                    platinumPiece.DisplayDescription = $"{nameof(NameHelper.CoinValues)}.{NameHelper.CoinValues.PlatinumPiece}.{NameHelper.Naming.Description}";
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
            
                Common.EnsureFolderExists(PathHelper.Equipments.EquipmentToolsPath);
                
                
                
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