using DnD.Code.Scripts.Common;
using Unity.VisualScripting;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static partial class SpeciesInitializer
    {
        public static readonly string SpeciesPath = $"{Common.FolderPath}/{NameHelper.Naming.Species}";

        public static Code.Scripts.Characters.Species.Species[] GetAllSpecies()
        {
            return Common.GetAllScriptableObjects<Code.Scripts.Characters.Species.Species>(SpeciesPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Species Data")]
        public static void InitializeSpecies()
        {
            Common.EnsureFolderExists(SpeciesPath);

            var creatureTypes = CreatureTypesInitializer.GetAllCreatureTypes();
            
            InitializeHuman(creatureTypes);
        }
    }
}