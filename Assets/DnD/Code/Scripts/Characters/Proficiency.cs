using System;

namespace DnD.Code.Scripts.Characters
{
    [Serializable]
    public class Proficiency<T>
    {
        public T Feature;
        public bool IsExpert;
    }
}
