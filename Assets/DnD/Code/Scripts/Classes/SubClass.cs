using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Classes
{
    [CreateAssetMenu(fileName = "NewSubClass", menuName = "Game Entities/Character/Classes/SubClass")]
    public class SubClass : ScriptableObject, ILocalizable
    {
        public Level Level03;
        public Level Level06;
        public Level Level10;
        public Level Level14;
        
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
