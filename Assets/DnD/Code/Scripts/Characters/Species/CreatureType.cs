using UnityEngine;

namespace DnD.Code.Scripts.Characters.Species
{
    [CreateAssetMenu(fileName = "NewCreatureType", menuName = "Game Entities/Character/Species/CreatureType")]
    public class CreatureType : ScriptableObject
    {
        public string Name;
    }
}