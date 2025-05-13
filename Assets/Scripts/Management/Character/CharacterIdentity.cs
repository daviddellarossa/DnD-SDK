using DnD.Code.Scripts.Characters;
using UnityEngine;

namespace Management.Character
{
    public class CharacterIdentity : MonoBehaviour
    {
        [field: SerializeField]
        public CharacterStats CharacterStats { get; set; }
    }
}