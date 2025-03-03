using UnityEngine;

namespace DnD.Code.Scripts.Combat
{
    [CreateAssetMenu(fileName = "NewDamageType", menuName = "Game Entities/DamageType")]
    public class DamageType : ScriptableObject
    {
        public string Name;
        public string Description;
    }
}
