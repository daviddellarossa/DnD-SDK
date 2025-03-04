using UnityEngine;

namespace DnD.Code.Scripts.Characters.Languages
{
    [CreateAssetMenu(fileName = "NewRareLanguage", menuName = "Game Entities/Character/Languages/Rare Language")]
    public class RareLanguage : ScriptableObject, IRareLanguage
    {
        public string Name;
        public LanguageOrigin Origin;
    }
}
