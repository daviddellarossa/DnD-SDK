using UnityEngine;

namespace DnD.Code.Scripts.Characters.Classes.ClassFeatures
{
    [CreateAssetMenu(fileName = "NewClassFeature", menuName = "Game Entities/Character/Classes/Class Feature")]
    public class ClassFeature : ScriptableObject
    {
        public string Name;
    }
}