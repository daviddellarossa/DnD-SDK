using UnityEngine;

namespace DnD.Code.Scripts.Characters.Classes
{
    [CreateAssetMenu(fileName = "NewSubClass", menuName = "Game Entities/Character/Classes/SubClass")]
    public class SubClass : ScriptableObject
    {
        public string Name;
        public string Description;
        public Level Level_3;
        public Level Level_6;
        public Level Level_10;
        public Level Level_14;
    }
}
