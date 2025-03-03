using DnD.Code.Scripts.Items;
using UnityEngine;

namespace DnD.Code.Scripts.Equipment.Coins
{
    [CreateAssetMenu(fileName = "NewCoinValue", menuName = "Game Entities/Equipment/CoinValues")]
    public class CoinValue : ScriptableObject, IItem, ICoin
    {
        public string Name;
        public string Abbreviation;
        public float Value;

        public string DisplayText => this.Name;
    }
}
