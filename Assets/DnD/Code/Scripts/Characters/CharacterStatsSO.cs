using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    [CreateAssetMenu(fileName = "NewCharacterStat", menuName = "Game Entities/Character/Character Stats")]
    public class CharacterStatsSo: ScriptableObject
    {
        [field: SerializeField]
        public CharacterStats CharacterStats { get; set; }
    }
}