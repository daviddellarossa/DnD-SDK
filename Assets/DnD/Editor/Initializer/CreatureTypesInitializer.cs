using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Species;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class CreatureTypesInitializer
    {
        // public static readonly string PathHelper.CreatureTypesPath = $"{Common.FolderPath}/{NameHelper.Naming.CreatureTypes}";

        public static CreatureType[] GetAllCreatureTypes()
        {
            return Common.GetAllScriptableObjects<CreatureType>(PathHelper.CreatureTypesPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Creature Types")]
        public static void InitializeCreatureTypes()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(PathHelper.CreatureTypesPath);

                {
                    var aberration = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Aberration, PathHelper.CreatureTypesPath);
                    aberration.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Aberration}";
                }
                
                {
                    var beast = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Beast, PathHelper.CreatureTypesPath);
                    beast.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Beast}";
                }
                
                {
                    var celestial = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Celestial, PathHelper.CreatureTypesPath);
                    celestial.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Celestial}";
                }
                
                {
                    var construct = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Construct, PathHelper.CreatureTypesPath);
                    construct.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Construct}";
                }
                
                {
                    var dragon = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Dragon, PathHelper.CreatureTypesPath);
                    dragon.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Dragon}";
                }
                
                {
                    var elemental = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Elemental, PathHelper.CreatureTypesPath);
                    elemental.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Elemental}";
                }
                
                {
                    var fey = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Fey, PathHelper.CreatureTypesPath);
                    fey.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fey}";
                }
                
                {
                    var fiend = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Fiend, PathHelper.CreatureTypesPath);
                    fiend.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fiend}";
                }
                
                {
                    var giant = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Giant, PathHelper.CreatureTypesPath);
                    giant.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Giant}";
                }
                
                {
                    var humanoid = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Humanoid, PathHelper.CreatureTypesPath);
                    humanoid.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Humanoid}";
                }
                
                {
                    var monstrosity = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Monstrosity, PathHelper.CreatureTypesPath);
                    monstrosity.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Monstrosity}";
                }
                
                {
                    var ooze = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Ooze, PathHelper.CreatureTypesPath);
                    ooze.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Ooze}";
                }
                
                {
                    var plant = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Plant, PathHelper.CreatureTypesPath);
                    plant.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Plant}";
                }
                
                {
                    var undead = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Undead, PathHelper.CreatureTypesPath);
                    undead.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Undead}";
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}