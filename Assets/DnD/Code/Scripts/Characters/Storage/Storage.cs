using UnityEngine;

namespace DnD.Code.Scripts.Characters.Storage
{
    [CreateAssetMenu(fileName = "NewStorage", menuName = "Game Entities/Storage")]
    public class Storage : ScriptableObject
    {
        public string Name;
    }
}
