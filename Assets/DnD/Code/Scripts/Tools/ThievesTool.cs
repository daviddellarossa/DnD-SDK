using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    public class ThievesTool : ScriptableObject, IThievesTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
