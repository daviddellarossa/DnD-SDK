using System.Linq;
using System.Runtime.InteropServices;
using Assets.Scripts.Game.Equipment.Gear;
using DnD.Code.Scripts.Characters.Backgrounds;
using DnD.Code.Scripts.Common;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class BackgroundInitializer
    {
        public static readonly string BackgroundsPath = $"{Common.FolderPath}/{NameHelper.Naming.Backgrounds}";

        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Background Data")]
        public static void InitializeBackgrounds()
        {
            try
            {
                var feats = FeatsInitializer.GetAllFeats();
                
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(BackgroundsPath);

                var abilities = AbilitiesInitializer.GetAllAbilities();
                
                var skills = AbilitiesInitializer.GetAllSkills();

                var coins = EquipmentInitializer.GetAllCoinValues();

                {
                    var acolytePath = $"{BackgroundsPath}/{NameHelper.Backgrounds.Acolyte}";
                    var acolyteToolsPath = $"{acolytePath}/Tools";
                    var acolyteStartingEquipmentPath = $"{acolytePath}/StartingEquipment";

                    Common.EnsureFolderExists(acolytePath);
                    Common.EnsureFolderExists(acolyteToolsPath);
                    Common.EnsureFolderExists(acolyteStartingEquipmentPath);

                    var acolyte =
                        Common.CreateScriptableObject<Code.Scripts.Characters.Backgrounds.Background>(
                            NameHelper.Backgrounds.Acolyte, acolytePath);
                    acolyte.Name = $"{nameof(NameHelper.Backgrounds)}.{NameHelper.Backgrounds.Acolyte}";
                    acolyte.Abilities[0] = abilities.Single(ability => ability.name == NameHelper.Abilities.Intelligence);
                    acolyte.Abilities[1] = abilities.Single(ability => ability.name == NameHelper.Abilities.Wisdom);
                    acolyte.Abilities[2] = abilities.Single(ability => ability.name == NameHelper.Abilities.Charisma);

                    acolyte.SkillProficiencies[0] = skills.Single(skill => skill.name == NameHelper.Skills.Insight);
                    acolyte.SkillProficiencies[1] = skills.Single(skill => skill.name == NameHelper.Skills.Religion);

                    acolyte.Feat =  feats.Single(feat => feat.name == NameHelper.Feats.MagicInitiate);

                    {
                        var optionA = Common.CreateScriptableObject<StartingEquipment>(NameHelper.StartingEquipmentOptions.OptionA, acolyteStartingEquipmentPath);
                    
                        {
                            var holySymbol = Common.CreateScriptableObject<HolySymbol>(NameHelper.Equipment.Gear.Acolyte.HolySymbol, acolyteToolsPath);
                            holySymbol.Name = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.HolySymbol}";
                            holySymbol.Description = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.HolySymbol}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(holySymbol, 1.0f));
                            
                            EditorUtility.SetDirty(holySymbol);
                        }
                        {
                            var parchment = Common.CreateScriptableObject<HolySymbol>(NameHelper.Equipment.Gear.Acolyte.Parchment, acolyteToolsPath);
                            parchment.Name = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Parchment}";
                            parchment.Description = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Parchment}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(parchment, 10.0f));
                            
                            EditorUtility.SetDirty(parchment);
                        }
                        {
                            var robe = Common.CreateScriptableObject<HolySymbol>(NameHelper.Equipment.Gear.Acolyte.Robe, acolyteToolsPath);
                            robe.Name = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Robe}";
                            robe.Description = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Robe}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(robe, 1.0f));
                            
                            EditorUtility.SetDirty(robe);
                        }
                        {
                            var prayersBook = Common.CreateScriptableObject<HolySymbol>(NameHelper.Equipment.Gear.Acolyte.Book, acolyteToolsPath);
                            prayersBook.Name = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Book}";
                            prayersBook.Description = $"{nameof(NameHelper.Equipment.Gear)}.{NameHelper.Equipment.Gear.Acolyte.Book}.{NameHelper.Naming.Description}";
                            optionA.Items.Add(new StartingEquipment.EquipmentWithAmount(prayersBook, 1.0f));
                            
                            EditorUtility.SetDirty(prayersBook);
                        }
                        {
                            var calligrapherTool = Common.CreateScriptableObject<HolySymbol>(NameHelper.Equipment.Tools.CalligrapherTool, acolyteToolsPath);
                            calligrapherTool.Name = $"{nameof(NameHelper.Equipment.Tools)}.{NameHelper.Equipment.Tools.CalligrapherTool}";
                            calligrapherTool.Description = $"{nameof(NameHelper.Equipment.Tools)}.{NameHelper.Equipment.Tools.CalligrapherTool}.{NameHelper.Naming.Description}";
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
                        var optionB = Common.CreateScriptableObject<StartingEquipment>(NameHelper.StartingEquipmentOptions.OptionB, acolyteStartingEquipmentPath);
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