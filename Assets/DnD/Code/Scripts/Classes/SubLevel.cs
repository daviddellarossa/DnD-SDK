using System.Collections.Generic;
using DnD.Code.Scripts.Classes.ClassFeatures;
using UnityEngine;

namespace DnD.Code.Scripts.Classes
{
    public class SubLevel: ScriptableObject
    {
        public List<ClassFeature> ClassFeatures = new();
    }
}
