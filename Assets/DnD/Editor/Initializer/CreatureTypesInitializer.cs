using DnD.Code.Scripts.Characters.Species;
using DnD.Code.Scripts.Common;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class CreatureTypesInitializer
    {
        public static readonly string CreatureTypesPath = $"{Common.FolderPath}/CreatureTypes";


        [MenuItem("D&D Game/Game Data Initializer/Initialize all Creature Types")]
        public static void InitializeCreatureTypes()
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                Common.EnsureFolderExists(CreatureTypesPath);

                {
                    var aberration = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Aberration, CreatureTypesPath);
                    aberration.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Aberration}";
                }
                
                {
                    var beast = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Beast, CreatureTypesPath);
                    beast.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Beast}";
                }
                
                {
                    var celestial = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Celestial, CreatureTypesPath);
                    celestial.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Celestial}";
                }
                
                {
                    var construct = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Construct, CreatureTypesPath);
                    construct.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Construct}";
                }
                
                {
                    var dragon = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Dragon, CreatureTypesPath);
                    dragon.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Dragon}";
                }
                
                {
                    var elemental = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Elemental, CreatureTypesPath);
                    elemental.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Elemental}";
                }
                
                {
                    var fey = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Fey, CreatureTypesPath);
                    fey.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fey}";
                }
                
                {
                    var fiend = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Fiend, CreatureTypesPath);
                    fiend.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Fiend}";
                }
                
                {
                    var giant = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Giant, CreatureTypesPath);
                    giant.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Giant}";
                }
                
                {
                    var humanoid = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Humanoid, CreatureTypesPath);
                    humanoid.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Humanoid}";
                }
                
                {
                    var monstrosity = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Monstrosity, CreatureTypesPath);
                    monstrosity.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Monstrosity}";
                }
                
                {
                    var ooze = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Ooze, CreatureTypesPath);
                    ooze.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Ooze}";
                }
                
                {
                    var plant = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Plant, CreatureTypesPath);
                    plant.Name = $"{nameof(NameHelper.CreatureTypes)}.{NameHelper.CreatureTypes.Plant}";
                }
                
                {
                    var undead = Common.CreateScriptableObject<CreatureType>(NameHelper.CreatureTypes.Undead, CreatureTypesPath);
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