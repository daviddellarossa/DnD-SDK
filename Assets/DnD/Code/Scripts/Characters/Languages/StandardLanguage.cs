using UnityEngine;

namespace DnD.Code.Scripts.Characters.Languages
{
    [CreateAssetMenu(fileName = "NewStandardLanguage", menuName = "Game Entities/Character/Languages/Standard Language")]
    public class StandardLanguage : ScriptableObject, IStandardLanguage
    {
        public string Name;
        public LanguageOrigin Origin;
    }
}
