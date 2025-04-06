using UnityEngine;
using UnityEngine.Localization;
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
            var txtCharacterNameLabel = new LocalizedString("CharacterUI", "UI.CharacterBuild.CharacterName");
            txtCharacterNameLabel.StringChanged += (localizedText) => txtCharacterName.label = localizedText;
            
            // Set localized text for Character Name
            var ddfSpecies = root.Q<DropdownField>("ddfSpecies");
            var ddfSpeciesLabel = new LocalizedString("CharacterUI", "UI.CharacterBuild.Species");
            ddfSpeciesLabel.StringChanged += (localizedText) => ddfSpecies.label = localizedText;
            
            //Dvar species = DnD.Helpers.
        }
        
    }
}