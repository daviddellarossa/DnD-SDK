using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Languages
{
    [CreateAssetMenu(fileName = "NewLanguageOrigin", menuName = "Game Entities/Character/Languages/Origin/Language Origin")]
    public class LanguageOrigin : ScriptableObject, ILocalizable
    {
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
