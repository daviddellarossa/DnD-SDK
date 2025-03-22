using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts
{
    [CreateAssetMenu(fileName = "NewDie", menuName = "Game Entities/Die")]
    public class Die : ScriptableObject, ILocalizable
    {
        //public string Name;
        public int NumOfFaces;
        
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
