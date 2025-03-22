using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewMusicalInstrument", menuName = "Game Entities/Equipment/Tools/Musical Instrument")]
    public class MusicalInstrumentTool : ScriptableObject, IMusicalInstrumentTool
    {
        public string DisplayName { get; set; }
        public string DisplayDescription { get; set; }
    }
}
