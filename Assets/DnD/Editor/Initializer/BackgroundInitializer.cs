using System.Linq;
using DnD.Code.Scripts.Common;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class BackgroundInitializer
    {
        public static readonly string BackgroundsPath = $"{Common.FolderPath}/Backgrounds";

        public static void InitializeBackgrounds()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(BackgroundsPath);

                var abilities = AbilitiesInitializer.GetAllAbilities();
                
                var skills = AbilitiesInitializer.GetAllSkills();

                {
                    var acolytePath = $"{Common.FolderPath}/{NameHelper.Backgrounds.Acolyte}";
                    
                    Common.EnsureFolderExists(acolytePath);
                    
                    var acolyte =
                        Common.CreateScriptableObject<Code.Scripts.Characters.Backgrounds.Background>(
                            NameHelper.Backgrounds.Acolyte, acolytePath);
                    acolyte.Name = $"{nameof(NameHelper.Backgrounds)}.{NameHelper.Backgrounds.Acolyte}";
                    acolyte.Abilities[0] = abilities.Single(ability => ability.name == NameHelper.Abilities.Intelligence);
                    acolyte.Abilities[1] = abilities.Single(ability => ability.name == NameHelper.Abilities.Wisdom);
                    acolyte.Abilities[2] = abilities.Single(ability => ability.name == NameHelper.Abilities.Charisma);

                    acolyte.SkillProficiencies[0] = skills.Single(skill => skill.name == NameHelper.Skills.Insight);
                    acolyte.SkillProficiencies[1] = skills.Single(skill => skill.name == NameHelper.Skills.Religion);

                    // TODO: continue
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