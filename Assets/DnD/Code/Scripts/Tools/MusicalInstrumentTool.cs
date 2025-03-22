using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewMusicalInstrument", menuName = "Game Entities/Equipment/Tools/Musical Instrument")]
    public class MusicalInstrumentTool : ScriptableObject, IMusicalInstrumentTool
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string DisplayDescription
        {
            get => displayDescription;
            set => displayDescription = value;
        }
    }
}
