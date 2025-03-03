using Game.Characters.Species.SpecialTraits.TraitTypes;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DnD.Editor.Species.SpecialTraits
{
    public class TraitTypeSelector : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

        private Button btnOK;

        public string SelectedValue;

        private static TraitTypeSelector windowInstance;
        private Action<string> onItemSelected; // Callback to return the value

        public static void ShowWindow(Action<string> callback)
        {
            if (windowInstance == null)
            {
                windowInstance = CreateInstance<TraitTypeSelector>();
                windowInstance.titleContent = new GUIContent("Select a Trait Type");
                windowInstance.onItemSelected = callback; // Store callback function
                windowInstance.ShowAuxWindow();
            }
            else
            {
                windowInstance.Focus();
            }
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
            root.Add(labelFromUXML);

            btnOK = root.Q<Button>("btnOK");

            btnOK.clicked += BtnOK_clicked;

            btnOK.SetEnabled(false);

            var btnCancel = root.Q<Button>("btnCancel");
            btnCancel.clicked += BtnCancel_clicked;

            // Get the list of types inheriting from TraitType
            var listOfTraitTypes = GetDerivedTypes(typeof(TraitType));

            // Find the DropDownList
            var ddfTraitType = root.Q<DropdownField>("ddfTraitTypes");

            // Assign the list to the DropDownField
            ddfTraitType.choices = listOfTraitTypes.Select(x => x.Name).OrderBy(x => x).ToList();

            // Handle selection change
            ddfTraitType.RegisterValueChangedCallback(evt =>
            {
                // This is called whenever the user selects a new option
                string selectedValue = evt.newValue;
                Debug.Log("Selected option: " + selectedValue);

                // You can also call a method on your component to handle the selection
                this.OnOptionSelected(selectedValue);
            });
        }

        private void BtnCancel_clicked()
        {
            this.Close();
        }

        private void BtnOK_clicked()
        {
            if (onItemSelected != null)
            {
                onItemSelected(this.SelectedValue);
            }

            this.Close();
        }

        private void OnOptionSelected(string selectedValue)
        {
            this.SelectedValue += selectedValue;
            this.btnOK.SetEnabled(!string.IsNullOrEmpty(selectedValue));
        }

        private System.Type[] GetDerivedTypes(System.Type baseType)
        {
            return System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract)
                .ToArray();
        }
    }
}
