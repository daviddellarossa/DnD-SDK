using UnityEngine;

namespace Assets.Scripts.Game.Character
{
    [CreateAssetMenu(fileName = "NewStorage", menuName = "Game Entities/Storage")]
    public class Storage : ScriptableObject
    {
        public string Name;
    }
}
