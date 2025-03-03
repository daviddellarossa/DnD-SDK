using System.Collections.Generic;
using DnD.Code.Scripts.Characters.Classes.ClassFeatures;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Classes
{
    public class SubLevel: ScriptableObject
    {
        public List<ClassFeature> ClassFeatures = new();
    }
}
