using UnityEngine;

namespace Assets.Scripts.Game.Characters.Languages
{
    [CreateAssetMenu(fileName = "NewRareLanguage", menuName = "Game Entities/Character/Languages/Rare Language")]
    public class RareLanguage : ScriptableObject, IRareLanguage
    {
        public string Name;
        public LanguageOrigin Origin;
    }
}
