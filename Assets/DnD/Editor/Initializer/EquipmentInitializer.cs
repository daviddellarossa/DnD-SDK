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
            
        }
    }
}