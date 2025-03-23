using UnityEngine;

namespace DnD.Code.Scripts.Feats
{
    [CreateAssetMenu(fileName = "NewFeat", menuName = "Game Entities/Character/Feats/Feat")]
    public class Feat : ScriptableObject
    {
        public string DisplayName;
        public FeatCategory Category;
        public Repeatable Repeatable;
        public object Benefit; // TODO
        public object Prerequisite; // TODO

    }
}
