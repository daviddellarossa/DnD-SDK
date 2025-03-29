using System;
using UnityEngine;

namespace DnD.Code.Scripts.Common
{
    [Serializable]
    public class Proficient
    {
        [SerializeField]
        private string proficiency;
        
        public string Proficiency => proficiency;

        private Proficient(string proficiency)
        {
            this.proficiency = proficiency;
        }

        public Proficient() { }

        public static Proficient Of<T>()
            where T: ScriptableObject, IProficiency
        {
            return new Proficient(typeof(T).FullName);
        }
    }
}