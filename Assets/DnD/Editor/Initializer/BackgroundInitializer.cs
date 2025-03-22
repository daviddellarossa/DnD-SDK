using System.Linq;
using System.Runtime.InteropServices;
using Assets.Scripts.Game.Equipment.Gear;
using DnD.Code.Scripts.Characters.Backgrounds;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Tools;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public static class BackgroundInitializer
    {
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Background Data")]
        public static void InitializeBackgrounds()
        {
            try
            {
                var feats = FeatsInitializer.GetAllFeats();
                
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(PathHelper.Backgrounds.BackgroundsPath);

                var abilities = AbilitiesInitializer.GetAllAbilities();
                
                var skills = AbilitiesInitializer.GetAllSkills();

                var coins = EquipmentInitializer.GetAllCoinValues();

                {
                    Common.EnsureFolderExists(PathHelper.Backgrounds.AcolytePath);
                    Common.EnsureFolderExists(PathHelper.Backgrounds.AcolyteToolsPath);
                    Common.EnsureFolderExists(PathHelper.Backgrounds.AcolyteStartingEquipmentPath);

                    var acolyte =
                        Common.CreateScriptableObject<Code.Scripts.Characters.Backgrounds.Background>(
                            NameHelper.Backgrounds.Acolyte, PathHelper.Backgrounds.AcolytePath);
                    acolyte.DisplayName = $"{nameof(NameHelper.Backgrounds)}.{NameHelper.Backgrounds.Acolyte}";
                    acolyte.DisplayDescription = $"{nameof(NameHelper.Backgrounds)}.{NameHelper.Backgrounds.Acolyte}.{NameHelper.Naming.Description}";
                    acolyte.Abilities[0] = abilities.Single(ability => ability.name == NameHelper.Abilities.Intelligence);
                    acolyte.Abilities[1] = abilities.Single(ability => ability.name == NameHelper.Abilities.Wisdom);
                    acolyte.Abilities[2] = abilities.Single(ability => ability.name == NameHelper.Abilities.Charisma);

                    acolyte.SkillProficiencies[0] = skills.Single(skill => skill.name == NameHelper.Skills.Insight);
                    acolyte.SkillProficiencies[1] = skills.Single(skill => skill.name == NameHelper.Skills.Religion);

                    acolyte.Feat =  feats.Single(feat => feat.name == NameHelper.Feats.MagicInitiate);

                    {
                        var optionA = Common.CreateScriptableObject<StartingEquipment>(NameHelper.StartingEquipmentOptions.OptionA, PathHelper.Backgrounds.AcolyteStartingEquipmentPath);
                    
                        {
                            var holySymbol = Common.CreateScriptableObject<HolySymbol>(NameHelper.Equipment.Gear.Acolyte.HolySymbol, PathHelper.Backgrounds.AcolyteToolsPath);
                            holySymbol.DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.HolySymbol}";
                            holySymbol.DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.HolySymbol}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(holySymbol, 1.0f));
                            
                            EditorUtility.SetDirty(holySymbol);
                        }
                        {
                            var parchment = Common.CreateScriptableObject<Parchment>(NameHelper.Equipment.Gear.Acolyte.Parchment, PathHelper.Backgrounds.AcolyteToolsPath);
                            parchment.DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Parchment}";
                            parchment.DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Parchment}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(parchment, 10.0f));
                            
                            EditorUtility.SetDirty(parchment);
                        }
                        {
                            var robe = Common.CreateScriptableObject<Robe>(NameHelper.Equipment.Gear.Acolyte.Robe, PathHelper.Backgrounds.AcolyteToolsPath);
                            robe.DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Robe}";
                            robe.DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Robe}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(robe, 1.0f));
                            
                            EditorUtility.SetDirty(robe);
                        }
                        {
                            var prayersBook = Common.CreateScriptableObject<Book>(NameHelper.Equipment.Gear.Acolyte.Book, PathHelper.Backgrounds.AcolyteToolsPath);
                            prayersBook.DisplayName = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Book}";
                            prayersBook.DisplayDescription = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Book}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(prayersBook, 1.0f));
                            
                            EditorUtility.SetDirty(prayersBook);
                        }
                        {
                            var calligrapherTool = Common.CreateScriptableObject<CalligrapherTool>(NameHelper.Equipment.Tools.CalligrapherTool, PathHelper.Backgrounds.AcolyteToolsPath);
                            calligrapherTool.DisplayName = $"{nameof(NameHelper.Equipment.Tools)}.{NameHelper.Equipment.Tools.CalligrapherTool}";
                            calligrapherTool.DisplayDescription = $"{nameof(NameHelper.Equipment.Tools)}.{NameHelper.Equipment.Tools.CalligrapherTool}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(calligrapherTool, 1.0f));
                            
                            EditorUtility.SetDirty(calligrapherTool);
                        }

                        {
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(coins.Single(coin => coin.name == NameHelper.CoinValues.GoldPiece), 8.0f));
                        }
                        
                        EditorUtility.SetDirty(optionA);
                    
                        acolyte.StartingEquipment[0] = optionA;
                        
                    }
                    {
                        var optionB = Common.CreateScriptableObject<StartingEquipment>(NameHelper.StartingEquipmentOptions.OptionB, PathHelper.Backgrounds.AcolyteStartingEquipmentPath);
                        {
                            optionB.Items.Add(new StartingEquipment.EquipmentWithAmount(coins.Single(coin => coin.name == NameHelper.CoinValues.GoldPiece), 50.0f));
                        }
                        
                        EditorUtility.SetDirty(optionB);

                        acolyte.StartingEquipment[1] = optionB;
                    }
                    
                    acolyte.ToolProficiency = NameHelper.Equipment.Tools.CalligrapherTool;
                    
                    EditorUtility.SetDirty(acolyte);
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