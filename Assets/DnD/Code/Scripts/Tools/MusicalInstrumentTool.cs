using UnityEngine;

namespace DnD.Code.Scripts.Tools
{
    [CreateAssetMenu(fileName = "NewMusicalInstrument", menuName = "Game Entities/Equipment/Tools/Musical Instrument")]
    public class MusicalInstrumentTool : ScriptableObject, IMusicalInstrumentTool
    {
        public string Name;

        public string DisplayText => this.Name;
    }
}
