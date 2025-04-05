using UnityEngine;
using UnityEngine.UIElements;

namespace Scenes.CharacterBuild
{
    public class CharacterBuild : MonoBehaviour
    {
        private UIDocument uiDocument;
        void Awake()
        {
            uiDocument = GetComponent<UIDocument>();

            if (uiDocument != null)
            {
                var root = uiDocument.rootVisualElement;
                
            }
        }
    }
}