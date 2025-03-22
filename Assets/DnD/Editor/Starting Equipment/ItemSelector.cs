using System;
using System.Linq;
using DnD.Code.Scripts.Items;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Game.Characters.Classes
{
    public class ItemSelector : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private Button btnOK;

        public string SelectedValue;

        private static ItemSelector windowInstance;
        private Action<string> onItemSelected; // Callback to return the value

        public static void ShowWindow(Action<string> callback)
        {
            if (windowInstance == null)
            {
                windowInstance = CreateInstance<ItemSelector>();
                windowInstance.titleContent = new GUIContent("Select an Item");
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

            // Get the list of types inheriting from IItem
            //var listOfItems = GetDerivedTypes(typeof(IItem));

            var listOfItems = FindAllItems();

            // Find the DropDownList
            var ddfTraitType = root.Q<DropdownField>("ddfTraitTypes");

            // Assign the list to the DropDownField
            ddfTraitType.choices = listOfItems.Select(x => x.DisplayName).OrderBy(x => x).ToList();

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
            if (baseType.IsInterface)
            {
                return System.AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => baseType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    .ToArray();
            }
            else if (baseType.IsClass)
            {
                return System.AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract)
                    .ToArray();
            }
            else
            {
                throw new ArgumentException("Provided type must be an interface.", nameof(baseType));
            }
        }

        public static IItem[] FindAllItems()
        {
            // Find all ScriptableObject assets
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");

            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>) // Load as ScriptableObject
                .Where(asset => asset is IItem) // Filter those that implement IItem
                .Cast<IItem>() // Cast to IItem interface
                .ToArray();
        }
    }
}
