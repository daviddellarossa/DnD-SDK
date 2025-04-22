using System;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using Infrastructure.Helpers;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.UIElements;
using Background = DnD.Code.Scripts.Backgrounds.Background;

namespace Management.CharacterBuild
{
    public class CharacterBuildSceneManager : MonoBehaviour
    {
        private static readonly string GameEntitiesLocalizationTable = "GameEntities";
        private static readonly string CharacterUILocalizationTable = "CharacterUI";

        public CharacterBuildSceneManagerCore Core { get; protected set; }
        
        private UIDocument uiDocument;
        
        private readonly List<string> speciesLocalizedNames = new();
        private readonly Dictionary<string, Spex> keyToSpecies = new();
        
        private readonly List<string> classesLocalizedNames = new();
        private readonly Dictionary<string, Class> keyToClasses = new();
            
        private readonly List<string> subClassesLocalizedNames = new();
        private readonly Dictionary<string, SubClass> keyToSubClasses = new();
        
        private readonly List<string> backgroundLocalizedNames = new();
        private readonly Dictionary<string, Background> keyToBackgrounds = new();

        private readonly List<string> skillProficienciesLocalizedNames = new();
        private readonly Dictionary<string, Skill> keyToSkillProficiencies = new();

        private readonly List<string> startingEquipmentFromClassLocalizedNames = new();
        private readonly Dictionary<string, StartingEquipment> keyToStartingEquipmentFromClass = new();

        private readonly List<string> startingEquipmentFromBackgroundLocalizedNames = new();
        private readonly Dictionary<string, StartingEquipment> keyToStartingEquipmentFromBackground = new();

        private readonly List<string> languagesLocalizedNames = new();
        private readonly Dictionary<string, StandardLanguage> keyToLanguages = new();

        private VisualElement root;
        private TextField txtCharacterName;
        private DropdownField ddfSpecies;
        private DropdownField ddfClasses;
        private DropdownField ddfSubClasses;
        private DropdownField ddfBackground;
        private Label lblSkillProficiencies;
        private Label lblStartingEquipmentFromClass;
        private Label lblStartingEquipmentFromBackground;
        private Label lblLanguages;

        private ListView lvSkillProficiencies;
        private ListView lvStartingEquipmentFromClass;
        private ListView lvStartingEquipmentFromBackground;
        private ListView lvLanguages;

        private SliderInt siCharisma;
        private SliderInt siConstitution;
        private SliderInt siDexterity;
        private SliderInt siIntelligence;
        private SliderInt siStrength;
        private SliderInt siWisdom;
        
        private Button btnCreateCharacter;
        
        void Awake()
        {
            Core = new CharacterBuildSceneManagerCore(this);

            Core.OnAwake();
            
            uiDocument = gameObject.GetComponent<UIDocument>();

            if (uiDocument == null)
            {
                Debug.LogError("UI Document is null");
            }
            
            root = uiDocument.rootVisualElement;
            
            btnCreateCharacter = root.Q<Button>("btnCreateCharacter");
            btnCreateCharacter.clicked += BtnCreateCharacterOnClicked;
            
            lblSkillProficiencies = root.Q<Label>("lblSkillProficiencies");
            lblStartingEquipmentFromClass = root.Q<Label>("lblStartingEquipmentFromClass");
            lblStartingEquipmentFromBackground = root.Q<Label>("lblStartingEquipmentFromBackground");
            lblLanguages = root.Q<Label>("lblLanguages");
            
            lvSkillProficiencies  = root.Q<ListView>("lvSkillProficiencies");
            lvSkillProficiencies.selectionChanged += LvSkillProficienciesOnSelectionChanged;
            
            lvStartingEquipmentFromClass  = root.Q<ListView>("lvStartingEquipmentFromClass");
            lvStartingEquipmentFromBackground  = root.Q<ListView>("lvStartingEquipmentFromBackground");
            lvLanguages = root.Q<ListView>("lvLanguages");
            lvLanguages.selectionChanged += LvLanguagesOnSelectionChanged;

            siCharisma = root.Q<SliderInt>("siCharisma");
            siConstitution = root.Q<SliderInt>("siConstitution");
            siDexterity = root.Q<SliderInt>("siDexterity");
            siIntelligence = root.Q<SliderInt>("siIntelligence");
            siStrength = root.Q<SliderInt>("siStrength");
            siWisdom = root.Q<SliderInt>("siWisdom");
            
            ddfSpecies = root.Q<DropdownField>("ddfSpecies");
            
            SetSpecies();
            
            ddfClasses = root.Q<DropdownField>("ddfClasses");
            ddfSubClasses = root.Q<DropdownField>("ddfSubClasses");
            
            ddfClasses.RegisterValueChangedCallback(SetSubClasses);
            ddfClasses.RegisterValueChangedCallback(SetSkillProficiencies);
            ddfClasses.RegisterValueChangedCallback(SetStartingEquipmentFromClass);
            
            SetClasses();
            
            ddfBackground = root.Q<DropdownField>("ddfBackground");
            ddfBackground.RegisterValueChangedCallback(SetStartingEquipmentFromBackground);

            SetBackground();
            
            SetLanguages();
        }

        private void BtnCreateCharacterOnClicked()
        {
            try
            {
                var abilities = ScriptableObjectHelper.GetAllScriptableObjects<Ability>(PathHelper.Abilities.AbilitiesPath);
                var builder = new CharacterStats.Builder();

                builder
                    .SetName(txtCharacterName.value)
                    .SetClass(keyToClasses[ddfClasses.value])
                    .SetSubClass(keyToSubClasses[ddfSubClasses.value])
                    .SetBackground(keyToBackgrounds[ddfBackground.value])
                    .SetSpex(keyToSpecies[ddfSpecies.value])
                    .SetSkillProficienciesFromClass(lvSkillProficiencies.selectedItems.Cast<string>()
                        .Select(x => keyToSkillProficiencies[x]).ToArray())
                    .SetStartingEquipmentFromClass(
                        keyToStartingEquipmentFromClass[(string)lvStartingEquipmentFromClass.selectedItem])
                    .SetStartingEquipmentFromBackground(
                        keyToStartingEquipmentFromBackground[(string)lvStartingEquipmentFromBackground.selectedItem])
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = abilities.Single(ability => ability.name == NameHelper.Abilities.Charisma),
                        Score = siCharisma.value,
                        SkillProficiencies = lvSkillProficiencies.selectedItems.Cast<string>()
                            .Select(sk => keyToSkillProficiencies[sk])
                            .Where(sk => sk.Ability.name == NameHelper.Abilities.Charisma)
                            .ToDictionary(x => x.name, x => new SkillStats() { Skill = x }),
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = abilities.Single(ability => ability.name == NameHelper.Abilities.Constitution),
                        Score = siConstitution.value,
                        SkillProficiencies = lvSkillProficiencies.selectedItems.Cast<string>()
                            .Select(sk => keyToSkillProficiencies[sk])
                            .Where(sk => sk.Ability.name == NameHelper.Abilities.Constitution)
                            .ToDictionary(x => x.name, x => new SkillStats() { Skill = x }),
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = abilities.Single(ability => ability.name == NameHelper.Abilities.Dexterity),
                        Score = siDexterity.value,
                        SkillProficiencies = lvSkillProficiencies.selectedItems.Cast<string>()
                            .Select(sk => keyToSkillProficiencies[sk])
                            .Where(sk => sk.Ability.name == NameHelper.Abilities.Dexterity)
                            .ToDictionary(x => x.name, x => new SkillStats() { Skill = x }),
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = abilities.Single(ability => ability.name == NameHelper.Abilities.Intelligence),
                        Score = siIntelligence.value,
                        SkillProficiencies = lvSkillProficiencies.selectedItems.Cast<string>()
                            .Select(sk => keyToSkillProficiencies[sk])
                            .Where(sk => sk.Ability.name == NameHelper.Abilities.Intelligence)
                            .ToDictionary(x => x.name, x => new SkillStats() { Skill = x }),
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = abilities.Single(ability => ability.name == NameHelper.Abilities.Strength),
                        Score = siStrength.value,
                        SkillProficiencies = lvSkillProficiencies.selectedItems.Cast<string>()
                            .Select(sk => keyToSkillProficiencies[sk])
                            .Where(sk => sk.Ability.name == NameHelper.Abilities.Strength)
                            .ToDictionary(x => x.name, x => new SkillStats() { Skill = x }),
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = abilities.Single(ability => ability.name == NameHelper.Abilities.Wisdom),
                        Score = siWisdom.value,
                        SkillProficiencies = lvSkillProficiencies.selectedItems.Cast<string>()
                            .Select(sk => keyToSkillProficiencies[sk])
                            .Where(sk => sk.Ability.name == NameHelper.Abilities.Wisdom)
                            .ToDictionary(x => x.name, x => new SkillStats() { Skill = x }),
                    });
                
                foreach (var language in lvLanguages.selectedItems.Cast<string>())
                {
                    builder.SetLanguage(keyToLanguages[language]);
                }
                
                var instance = builder.Build();

                if (instance == null)
                {
                    Debug.LogError("CharacterStat instance is null");
                }
                
                DeeDeeR.MessageBroker.MessageBroker.Instance.Character.Send_CharacterCreated(this, null, instance);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }
        }

        private void SetSpecies()
        {
            var species =
                Infrastructure.Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Spex>(DnD.Code.Scripts.Helpers
                    .PathHelper.PathHelper.Species.SpeciesPath);

            int pending = species.Length;

            foreach (var spex in species)
            {
                var spexLocalizedString = new LocalizedString(GameEntitiesLocalizationTable, spex.DisplayName);
                spexLocalizedString.StringChanged += (localizedText) =>
                {
                    speciesLocalizedNames.Add(localizedText);
                    keyToSpecies[localizedText] = spex;

                    pending--;
                    if (pending == 0)
                    {
                        ddfSpecies.choices = speciesLocalizedNames;
                        ddfSpecies.value = speciesLocalizedNames[0];
                    }
                };
            }
        }

        private void SetLanguages()
        {
            var languages =
                Infrastructure.Helpers.ScriptableObjectHelper.GetAllScriptableObjects<StandardLanguage>(DnD.Code.Scripts.Helpers
                    .PathHelper.PathHelper.Languages.StandardLanguagesPath);
            
            int pending = languages.Length;
            
            foreach (var language in languages)
            {
                var localizedString = new LocalizedString(GameEntitiesLocalizationTable, language.DisplayName);
                localizedString.StringChanged += (localizedText) =>
                {
                    languagesLocalizedNames.Add(localizedText);
                    keyToLanguages[localizedText] = language;

                    pending--;
                    if (pending == 0)
                    {
                        lvLanguages.itemsSource = languagesLocalizedNames;
                        lvLanguages.makeItem = () => new Label();
                        lvLanguages.bindItem =
                            (element, i) => ((Label)element).text = languagesLocalizedNames[i];
                        lvLanguages.selectionType = SelectionType.Multiple;
                        lvLanguages.selectedIndex = 0;
                    }
                };
            }
        }

        private void SetClasses()
        {
            var classes =
                Infrastructure.Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Class>(DnD.Code.Scripts.Helpers
                    .PathHelper.PathHelper.Classes.ClassesPath);

            int pending = classes.Length;

            foreach (var @class in classes)
            {
                var classLocalizedString = new LocalizedString(GameEntitiesLocalizationTable, @class.DisplayName);
                classLocalizedString.StringChanged += (localizedText) =>
                {
                    classesLocalizedNames.Add(localizedText);
                    keyToClasses[localizedText] = @class;

                    pending--;
                    if (pending == 0)
                    {
                        ddfClasses.choices = classesLocalizedNames;
                        ddfClasses.value = classesLocalizedNames[0];
                    }
                };
            }
        }

        private void SetSubClasses(ChangeEvent<string> selectedClassChangeEvent)
        {
            if (string.IsNullOrEmpty(selectedClassChangeEvent.newValue))
            {
                return;
            }
        
            var selectedClass = keyToClasses.FirstOrDefault(x => x.Key == selectedClassChangeEvent.newValue);
            if (selectedClass.Value == null)
            {
                return;
            }
            var subclasses = selectedClass.Value.SubClasses.ToArray();
            
            int pending = subclasses.Length;
            foreach (var subClass in subclasses)
            {
                var localizedString = new LocalizedString(GameEntitiesLocalizationTable, @subClass.DisplayName);
                localizedString.StringChanged += (localizedText) =>
                {
                    subClassesLocalizedNames.Add(localizedText);
                    keyToSubClasses[localizedText] = subClass;

                    pending--;
                    if (pending == 0)
                    {
                        ddfSubClasses.choices = subClassesLocalizedNames;
                        ddfSubClasses.value = subClassesLocalizedNames[0];
                    }
                };
            }
        }

        private void SetSkillProficienciesLabel(int numberOfSkillProficienciesToChoose = 0)
        {
            var bindingInfos = lblSkillProficiencies.GetBindingInfos().ToArray();
            if (!bindingInfos.Any())
            {
                Debug.LogWarning("Binding for Skill Proficiencies label not found");
                return;
            }
            var bindingInfo = bindingInfos.First();
            var localizedString = bindingInfo.binding as LocalizedString;
            if (localizedString == null)
            {
                Debug.LogWarning("Unable to find LocalizedString for Skill Proficiencies label");
                return;
            }
            localizedString["num"] = new IntVariable { Value = numberOfSkillProficienciesToChoose };
            
            localizedString.RefreshString();
        }
        
        private void SetSkillProficiencies(ChangeEvent<string> selectedClassChangeEvent)
        {
            if (string.IsNullOrEmpty(selectedClassChangeEvent.newValue))
            {
                return;
            }
            
            var selectedClass = keyToClasses.FirstOrDefault(x => x.Key == selectedClassChangeEvent.newValue);
            if (selectedClass.Value == null)
            {
                return;
            }
            
            var skillProficiencies = selectedClass.Value.SkillProficienciesAvailable.ToArray();
            
            SetSkillProficienciesLabel(selectedClass.Value.NumberOfSkillProficienciesToChoose);
            
            int pending = skillProficiencies.Length;
            foreach (var skillProficiency in skillProficiencies)
            {
                var localizedString = new LocalizedString(GameEntitiesLocalizationTable, skillProficiency.DisplayName);
                localizedString.StringChanged += (localizedText) =>
                {
                    skillProficienciesLocalizedNames.Add(localizedText);
                    keyToSkillProficiencies[localizedText] = skillProficiency;

                    pending--;
                    if (pending == 0)
                    {
                        lvSkillProficiencies.itemsSource = skillProficienciesLocalizedNames;
                        lvSkillProficiencies.makeItem = () => new Label();
                        lvSkillProficiencies.bindItem =
                            (element, i) => ((Label)element).text = skillProficienciesLocalizedNames[i];
                        lvSkillProficiencies.selectionType = SelectionType.Multiple;

                    }
                };
            }
        }
        
        private void LvSkillProficienciesOnSelectionChanged(IEnumerable<object> obj)
        {
            var maxSelectionCount = 0;
            var selectedClass = keyToClasses[ddfClasses.value];
                
            if (selectedClass != null)
            {
                maxSelectionCount = selectedClass.NumberOfSkillProficienciesToChoose;
            }

            //var selectedItems = obj.ToList();
                
            var selectedIndices = lvSkillProficiencies.selectedIndices.ToList();
                
            if (selectedIndices.Count() > maxSelectionCount)
            {
                // Revert the last selection by trimming the list
                selectedIndices = selectedIndices.Take(maxSelectionCount).ToList();

                // Update selection without triggering the callback again
                lvSkillProficiencies.SetSelectionWithoutNotify(selectedIndices);
            }
        }
        
        private void LvLanguagesOnSelectionChanged(IEnumerable<object> obj)
        {
            var maxSelectionCount = 3;
            
            var selectedItems = lvLanguages.selectedItems.ToList();
            var selectedIndices = lvLanguages.selectedIndices.ToList();

            if (!selectedIndices.Contains(0))
            {
                selectedIndices.Add(0);
                lvLanguages.SetSelectionWithoutNotify(selectedIndices);
            }
                
            if (selectedIndices.Count() > maxSelectionCount)
            {
                // Revert the last selection by trimming the list
                selectedIndices = selectedIndices.Take(maxSelectionCount).ToList();

                // Update selection without triggering the callback again
                lvLanguages.SetSelectionWithoutNotify(selectedIndices);
            }
        }

        private void SetStartingEquipmentFromClass(ChangeEvent<string> selectedClassChangeEvent)
        {
            if (string.IsNullOrEmpty(selectedClassChangeEvent.newValue))
            {
                return;
            }
            
            var selectedClass = keyToClasses.FirstOrDefault(x => x.Key == selectedClassChangeEvent.newValue);
            if (selectedClass.Value == null)
            {
                return;
            }
            
            var startingEquipmentOptions = selectedClass.Value.StartingEquipmentOptions.ToArray();
            
            int pending = startingEquipmentOptions.Length;
            foreach (var startingEquipment in startingEquipmentOptions)
            {
                var localizedString = new LocalizedString(GameEntitiesLocalizationTable, startingEquipment.DisplayName);
                localizedString.StringChanged += (localizedText) =>
                {
                    startingEquipmentFromClassLocalizedNames.Add(localizedText);
                    keyToStartingEquipmentFromClass[localizedText] = startingEquipment;

                    pending--;
                    if (pending == 0)
                    {
                        lvStartingEquipmentFromClass.itemsSource = startingEquipmentFromClassLocalizedNames;
                        lvStartingEquipmentFromClass.makeItem = () => new Label();
                        lvStartingEquipmentFromClass.bindItem =
                            (element, i) => ((Label)element).text = startingEquipmentFromClassLocalizedNames[i];
                        lvStartingEquipmentFromClass.selectionType = SelectionType.Single;

                    }
                };
            }
        }

        private void SetBackground()
        {
            var backgrounds =
                Infrastructure.Helpers.ScriptableObjectHelper.GetAllScriptableObjects<DnD.Code.Scripts.Backgrounds.Background>(DnD.Code.Scripts.Helpers
                    .PathHelper.PathHelper.Backgrounds.BackgroundsPath);

            int pending = backgrounds.Length;

            foreach (var background in backgrounds)
            {
                var backgroundLocalizedString = new LocalizedString(GameEntitiesLocalizationTable, background.DisplayName);
                backgroundLocalizedString.StringChanged += (localizedText) =>
                {
                    backgroundLocalizedNames.Add(localizedText);
                    keyToBackgrounds[localizedText] = background;

                    pending--;
                    if (pending == 0)
                    {
                        ddfBackground.choices = backgroundLocalizedNames;
                        ddfBackground.value = backgroundLocalizedNames[0];
                    }
                };
            }
        }
        
        private void SetStartingEquipmentFromBackground(ChangeEvent<string> selectedBackgroundChangeEvent)
        {
            if (string.IsNullOrEmpty(selectedBackgroundChangeEvent.newValue))
            {
                return;
            }
            
            var selectedBackground = keyToBackgrounds.FirstOrDefault(x => x.Key == selectedBackgroundChangeEvent.newValue);
            if (selectedBackground.Value == null)
            {
                return;
            }
            
            var startingEquipmentOptions = selectedBackground.Value.StartingEquipmentOptions.ToArray();
            
            int pending = startingEquipmentOptions.Length;
            foreach (var startingEquipment in startingEquipmentOptions)
            {
                var localizedString = new LocalizedString(GameEntitiesLocalizationTable, startingEquipment.DisplayName);
                localizedString.StringChanged += (localizedText) =>
                {
                    startingEquipmentFromBackgroundLocalizedNames.Add(localizedText);
                    keyToStartingEquipmentFromBackground[localizedText] = startingEquipment;

                    pending--;
                    if (pending == 0)
                    {
                        lvStartingEquipmentFromBackground.itemsSource = startingEquipmentFromBackgroundLocalizedNames;
                        lvStartingEquipmentFromBackground.makeItem = () => new Label();
                        lvStartingEquipmentFromBackground.bindItem =
                            (element, i) => ((Label)element).text = startingEquipmentFromBackgroundLocalizedNames[i];
                        lvStartingEquipmentFromBackground.selectionType = SelectionType.Single;
                    }
                };
            }
        }
    }
}