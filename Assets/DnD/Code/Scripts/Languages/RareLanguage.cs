using UnityEngine;

namespace DnD.Code.Scripts.Languages
{
    [CreateAssetMenu(fileName = "NewRareLanguage", menuName = "Game Entities/Character/Languages/Rare Language")]
    public class RareLanguage : ScriptableObject, IRareLanguage
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
