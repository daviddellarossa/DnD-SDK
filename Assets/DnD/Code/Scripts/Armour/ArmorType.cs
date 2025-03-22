using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Armour
{
    [CreateAssetMenu(fileName = "NewArmourType", menuName = "Game Entities/Armours/ArmourType")]
    public class ArmourType : ScriptableObject, IBaseArmourType
    {
        public string Name;
        public float TimeInMinutesToDon;
        public float TimeInMinutesToDoff;
        
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
