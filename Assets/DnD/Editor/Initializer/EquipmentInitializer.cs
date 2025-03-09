using Codice.Client.BaseCommands.Acl;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment.Coins;

namespace DnD.Editor.Initializer
{
    public static class EquipmentInitializer
    {
        public static readonly string EquipmentPath = $"{Common.FolderPath}/Equipment";
        public static readonly string EquipmentCoinsPath = $"{EquipmentPath}/Coins";


        public static CoinValue[] GetAllCoinValues()
        {
            return Common.GetAllScriptableObjects<CoinValue>(EquipmentCoinsPath);
        }
        
        public static void InitializeEquipment()
        {
            InitializeCoins();
        }

        public static void InitializeCoins()
        {
            var copperPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.CopperPiece, EquipmentCoinsPath);
            copperPiece.Name = NameHelper.CoinValues.CopperPiece;
            copperPiece.Abbreviation = "CP";
            copperPiece.Value = 1;

            var silverpiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.SilverPiece, EquipmentCoinsPath);
            silverpiece.Name = NameHelper.CoinValues.SilverPiece;
            silverpiece.Abbreviation = "SP";
            silverpiece.Value = 10;

            var electrumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.ElectrumPiece, EquipmentCoinsPath);
            electrumPiece.Name = NameHelper.CoinValues.ElectrumPiece;
            electrumPiece.Abbreviation = "EP";
            electrumPiece.Value = 50;

            var goldPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.GoldPiece, EquipmentCoinsPath);
            goldPiece.Name = NameHelper.CoinValues.GoldPiece;
            goldPiece.Abbreviation = "GP";
            goldPiece.Value = 100;

            var platinumPiece = Common.CreateScriptableObject<CoinValue>(NameHelper.CoinValues.PlatinumPiece, EquipmentCoinsPath);
            platinumPiece.Name = NameHelper.CoinValues.PlatinumPiece;
            platinumPiece.Abbreviation = "PP";
            platinumPiece.Value = 1000;
        }
    }
}