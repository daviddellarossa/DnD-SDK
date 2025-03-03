using UnityEngine;

namespace Assets.Scripts.Game.Characters.Languages
{
    [CreateAssetMenu(fileName = "NewStandardLanguage", menuName = "Game Entities/Character/Languages/Standard Language")]
    public class StandardLanguage : ScriptableObject, IStandardLanguage
    {
        public string Name;
        public LanguageOrigin Origin;
    }
}
