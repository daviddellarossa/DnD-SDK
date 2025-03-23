using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using Unity.VisualScripting;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static partial class SpeciesInitializer
    {
        // public static readonly string PathHelper.Species.SpeciesPath = $"{Common.FolderPath}/{NameHelper.Naming.Species}";

        public static Code.Scripts.Species.Species[] GetAllSpecies()
        {
            return Common.GetAllScriptableObjects<Code.Scripts.Species.Species>(PathHelper.Species.SpeciesPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Species Data")]
        public static void InitializeSpecies()
        {
            Common.EnsureFolderExists(PathHelper.Species.SpeciesPath);

            var creatureTypes = CreatureTypesInitializer.GetAllCreatureTypes();
            
            InitializeHuman(creatureTypes);
        }
    }
}