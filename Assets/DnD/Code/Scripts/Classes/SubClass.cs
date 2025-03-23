using UnityEngine;

namespace DnD.Code.Scripts.Classes
{
    [CreateAssetMenu(fileName = "NewSubClass", menuName = "Game Entities/Character/Classes/SubClass")]
    public class SubClass : ScriptableObject
    {
        public string Name;
        public string Description;
        public Level Level03;
        public Level Level06;
        public Level Level10;
        public Level Level14;
    }
}
