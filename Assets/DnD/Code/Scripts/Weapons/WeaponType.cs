using UnityEngine;

namespace DnD.Code.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "NewWeaponType", menuName = "Game Entities/Weapons/WeaponType")]
    public class WeaponType : ScriptableObject
    {
        public string Name;
    }
}
