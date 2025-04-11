using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Languages
{
    public abstract class Language: ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        [SerializeField]
        private LanguageOrigin origin;

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

        public LanguageOrigin Origin
        {
            get => origin;
            set => origin = value;
        }
    }
}
