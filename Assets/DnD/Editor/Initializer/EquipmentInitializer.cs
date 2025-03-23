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
                    copperPiece.DisplayName = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.CopperPiece}";
                    copperPiece.DisplayDescription = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.CopperPiece}.{NameHelper.Naming.Description}";
                    copperPiece.Abbreviation = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.CopperPiece}.{NameHelper.Naming.Abbreviation}";
                    copperPiece.Value = 1;
                    EditorUtility.SetDirty(copperPiece);
                }

                {
                    var silverpiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.SilverPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    silverpiece.DisplayName = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.SilverPiece}";
                    silverpiece.DisplayDescription = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.SilverPiece}.{NameHelper.Naming.Description}";
                    silverpiece.Abbreviation = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.SilverPiece}.{NameHelper.Naming.Abbreviation}";;
                    silverpiece.Value = 10;
                    EditorUtility.SetDirty(silverpiece);
                }

                {
                    var electrumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.ElectrumPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    electrumPiece.DisplayName = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.ElectrumPiece}";
                    electrumPiece.DisplayDescription = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.ElectrumPiece}.{NameHelper.Naming.Description}";
                    electrumPiece.Abbreviation = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.ElectrumPiece}.{NameHelper.Naming.Abbreviation}";;
                    electrumPiece.Value = 50;
                    EditorUtility.SetDirty(electrumPiece);
                }

                {
                    var goldPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.GoldPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    goldPiece.DisplayName = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.GoldPiece}";
                    goldPiece.DisplayDescription = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.GoldPiece}.{NameHelper.Naming.Description}";
                    goldPiece.Abbreviation = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.GoldPiece}.{NameHelper.Naming.Abbreviation}";;
                    goldPiece.Value = 100;
                    EditorUtility.SetDirty(goldPiece);
                }

                {
                    var platinumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.PlatinumPiece, PathHelper.Equipments.EquipmentCoinsPath);
                    platinumPiece.DisplayName = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.PlatinumPiece}";
                    platinumPiece.DisplayDescription = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.PlatinumPiece}.{NameHelper.Naming.Description}";
                    platinumPiece.Abbreviation = $"{NameHelper.Naming.Coins}.{NameHelper.CoinValues.PlatinumPiece}.{NameHelper.Naming.Abbreviation}";;
                    platinumPiece.Value = 1000;
                    EditorUtility.SetDirty(platinumPiece);
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