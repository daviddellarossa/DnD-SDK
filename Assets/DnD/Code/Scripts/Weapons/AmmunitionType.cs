using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "NewAmmunitionType", menuName = "Game Entities/Weapons/AmmunitionType")]
    public class AmmunitionType : ScriptableObject, ILocalizable
    {
        public int Amount;
        public Storage.Storage Storage;
        public float Weight;
        public float Cost;
        
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string DisplayDescription
        {
            get => displayDescription;
            set => displayDescription = value;
        }
    }
}
