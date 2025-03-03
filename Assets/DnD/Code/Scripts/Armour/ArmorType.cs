using UnityEngine;

namespace DnD.Code.Scripts.Armour
{
    [CreateAssetMenu(fileName = "NewArmourType", menuName = "Game Entities/Armours/ArmourType")]
    public class ArmourType : ScriptableObject, IBaseArmourType
    {
        public string Name;
        public float TimeInMinutesToDon;
        public float TimeInMinutesToDoff;
    }
}
