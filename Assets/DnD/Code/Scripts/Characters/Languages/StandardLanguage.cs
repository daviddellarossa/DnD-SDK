using UnityEngine;

namespace DnD.Code.Scripts.Characters.Languages
{
    [CreateAssetMenu(fileName = "NewStandardLanguage", menuName = "Game Entities/Character/Languages/Standard Language")]
    public class StandardLanguage : ScriptableObject, IStandardLanguage
    {
        // public string Name;
        public LanguageOrigin Origin;
        
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
