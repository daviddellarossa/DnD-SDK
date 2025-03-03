using UnityEngine;

namespace DnD.Code.Scripts
{
    [CreateAssetMenu(fileName = "NewDie", menuName = "Game Entities/Die")]
    public class Die : ScriptableObject
    {
        public string Name;
        public int NumOfFaces;
    }
}
