using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Species;
using Infrastructure.Helpers;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class CreatureTypesInitializer
    {
        // public static readonly string PathHelper.CreatureTypesPath = $"{Common.FolderPath}/{NameHelper.Naming.CreatureTypes}";

        public static CreatureType[] GetAllCreatureTypes()
        {
            return ScriptableObjectHelper.GetAllScriptableObjects<CreatureType>(PathHelper.CreatureTypesPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Creature Types")]
        public static void InitializeCreatureTypes()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                FileSystemHelper.EnsureFolderExists(PathHelper.CreatureTypesPath);

                {
                    var aberration = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Aberration, PathHelper.CreatureTypesPath);
                    aberration.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Aberration}";
                    aberration.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Aberration}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(aberration);
                }
                
                {
                    var beast = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Beast, PathHelper.CreatureTypesPath);
                    beast.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Beast}";
                    beast.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Beast}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(beast);
                }
                
                {
                    var celestial = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Celestial, PathHelper.CreatureTypesPath);
                    celestial.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Celestial}";
                    celestial.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Celestial}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(celestial);
                }
                
                {
                    var construct = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Construct, PathHelper.CreatureTypesPath);
                    construct.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Construct}";
                    construct.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Construct}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(construct);
                }
                
                {
                    var dragon = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Dragon, PathHelper.CreatureTypesPath);
                    dragon.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Dragon}";
                    dragon.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Dragon}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(dragon);
                }
                
                {
                    var elemental = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Elemental, PathHelper.CreatureTypesPath);
                    elemental.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Elemental}";
                    elemental.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Elemental}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(elemental);
                }
                
                {
                    var fey = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Fey, PathHelper.CreatureTypesPath);
                    fey.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fey}";
                    fey.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fey}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(fey);
                }
                
                {
                    var fiend = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Fiend, PathHelper.CreatureTypesPath);
                    fiend.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fiend}";
                    fiend.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fiend}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(fiend);
                }
                
                {
                    var giant = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Giant, PathHelper.CreatureTypesPath);
                    giant.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Giant}";
                    giant.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Giant}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(giant);
                }
                
                {
                    var humanoid = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Humanoid, PathHelper.CreatureTypesPath);
                    humanoid.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Humanoid}";
                    humanoid.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Humanoid}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(humanoid);
                }
                
                {
                    var monstrosity = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Monstrosity, PathHelper.CreatureTypesPath);
                    monstrosity.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Monstrosity}";
                    monstrosity.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Monstrosity}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(monstrosity);
                }
                
                {
                    var ooze = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Ooze, PathHelper.CreatureTypesPath);
                    ooze.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Ooze}";
                    ooze.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Ooze}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(ooze);
                }
                
                {
                    var plant = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Plant, PathHelper.CreatureTypesPath);
                    plant.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Plant}";
                    plant.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Plant}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(plant);
                }
                
                {
                    var undead = ScriptableObjectHelper.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Undead, PathHelper.CreatureTypesPath);
                    undead.DisplayName = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Undead}";
                    undead.DisplayDescription = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Undead}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(undead);
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