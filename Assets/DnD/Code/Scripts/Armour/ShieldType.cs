using UnityEngine;

namespace DnD.Code.Scripts.Armour
{
    [CreateAssetMenu(fileName = "NewShieldType", menuName = "Game Entities/Armours/ShieldType")]
    public class ShieldType : ScriptableObject, IBaseArmourType
    {
        public string Name;
    }
}
