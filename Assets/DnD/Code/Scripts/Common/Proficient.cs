using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Common
{
    [Serializable]
    public class Proficient
    {
        [SerializeField]
        private string proficiencyFullName;
        
        [SerializeField]
        private string proficiencyName;

        public string ProficiencyFullName => proficiencyFullName;

        public string ProficiencyName => proficiencyName;

        private Proficient(string proficiencyFullName, string proficiencyName)
        {
            this.proficiencyFullName = proficiencyFullName;
            this.proficiencyName = proficiencyName;
        }

        public Proficient() { }

        public static Proficient Of<T>()
            where T: ScriptableObject, IProficiency
        {
            return Of(typeof(T));
        }

        internal static Proficient Of(Type type)
        {
            return new Proficient(type.FullName, type.Name);
        }
    }
}