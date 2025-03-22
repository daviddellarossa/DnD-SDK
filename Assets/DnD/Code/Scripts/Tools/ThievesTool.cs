using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    public class ThievesTool : ScriptableObject, IThievesTool
    {
        public string DisplayName { get; set; }
        public string DisplayDescription { get; set; }
    }
}
