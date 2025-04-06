using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Species;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace Management.CharacterBuild
{
    public class CharacterBuildSceneManager : MonoBehaviour
    {
        public CharacterBuildSceneManagerCore Core { get; protected set; }
        
        private UIDocument uiDocument;
        
        void Awake()
        {
            
            Core = new CharacterBuildSceneManagerCore(this);

            Core.OnAwake();
            
            uiDocument = gameObject.GetComponent<UIDocument>();

            if (uiDocument == null)
            {
                Debug.LogError("UI Document is null");
            }
            
            var root = uiDocument.rootVisualElement;
            
            // Set localized text for Character Name
            var txtCharacterName = root.Q<TextField>("txtCharacterName");
            var txtCharacterNameLabel = new LocalizedString("CharacterUI", "UI.CharacterBuild.CharacterName.Label");
            txtCharacterNameLabel.StringChanged += (localizedText) => txtCharacterName.label = localizedText;
            
            List<string> speciesLocalizedNames = new List<string>();
            Dictionary<string, Spex> keyToSpecies = new Dictionary<string, Spex>();

            {
                // Set localized text for Character Name
                var ddfSpecies = root.Q<DropdownField>("ddfSpecies");
                var ddfSpeciesLabel = new LocalizedString("CharacterUI", "UI.CharacterBuild.Species.Label");
                ddfSpeciesLabel.StringChanged += (localizedText) => ddfSpecies.label = localizedText;

                // Set up species
                
                var species =
                    Infrastructure.Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Spex>(DnD.Code.Scripts.Helpers
                        .PathHelper.PathHelper.Species.SpeciesPath);

                int pending = species.Length;

                foreach (var spex in species)
                {
                    var spexLocalizedString = new LocalizedString("GameEntities", spex.DisplayName);
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
            
            List<string> classesLocalizedNames = new List<string>();
            Dictionary<string, Class> keyToClasses = new Dictionary<string, Class>();
            {
                // Set up class
                var ddfClasses = root.Q<DropdownField>("ddfClasses");
                var ddfClassesLabel = new LocalizedString("CharacterUI", "UI.CharacterBuild.Classes.Label");
                ddfClassesLabel.StringChanged += (localizedText) => ddfClasses.label = localizedText;
                
                var classes =
                    Infrastructure.Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Class>(DnD.Code.Scripts.Helpers
                        .PathHelper.PathHelper.Classes.ClassesPath);

                int pending = classes.Length;

                foreach (var @class in classes)
                {
                    var classLocalizedString = new LocalizedString("GameEntities", @class.DisplayName);
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
        }

        private void OnLocaleChanged(Locale obj)
        {
            throw new System.NotImplementedException();
        }
    }
}