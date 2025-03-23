using System;
using System.Linq;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.FeatureProperties;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DnD.Editor.Classes
{
    public class ClassFeatureTraitSelector : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

        private Button btnOK;
        
        private DropdownField ddfClassFeatureTraits;

        public string SelectedValue;

        private static ClassFeatureTraitSelector windowInstance;
        private Action<string> onItemSelected; // Callback to return the value

        public static void ShowWindow(Action<string> callback)
        {
            if (windowInstance == null)
            {
                windowInstance = CreateInstance<ClassFeatureTraitSelector>();
                windowInstance.titleContent = new GUIContent("Select a Class Feature Trait");
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
            
            // Find the Class Features DropDownList
            ddfClassFeatureTraits = root.Q<DropdownField>("ddfClassFeatureTraits");
            
            var availableItems = GetDerivedTypes<IClassFeatureTraits>();
            
            ddfClassFeatureTraits.choices.Clear();
            ddfClassFeatureTraits.choices = availableItems.Select(x => x.FullName).ToList();
            
            // Handle selection change
            ddfClassFeatureTraits.RegisterValueChangedCallback(evt =>
            {
                // This is called whenever the user selects a new option
                string selectedValue = evt.newValue;
                Debug.Log("Selected class feature behaviour: " + selectedValue);
                
                // You can also call a method on your component to handle the selection
                this.OnClassFeaturesSelected(selectedValue);
            });
        }
        
        private void OnClassSelected(string selectedValue)
        {
            var classFeatures = GetClassFeaturesByClass(selectedValue);

            ddfClassFeatureTraits.choices.Clear();
            ddfClassFeatureTraits.choices = classFeatures.Select(x => x.FullName).OrderBy(x => x).ToList();
            
            // // Get the list of types inheriting from ClassFeature
            // var listOfClassFeatures = GetDerivedTypes(typeof(ClassFeature));
            //
            // // Assign the list to the DropDownField
            // ddfClassFeatures.choices = listOfClassFeatures.Select(x => x.Name).OrderBy(x => x).ToList();
        }
        
        private void OnClassFeaturesSelected(string selectedValue)
        {
            this.SelectedValue = selectedValue;
            this.btnOK.SetEnabled(!string.IsNullOrEmpty(selectedValue));
        }

        private System.Type[] GetClassFeaturesByClass(string className)
        {
            var typeName = $@"I{className}ClassFeature";
            var iClassFeatureBehaviourType = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SingleOrDefault(type => type.Name == typeName);

            if (iClassFeatureBehaviourType == null)
            {
                throw new Exception($"Could not find class feature behaviour: {className}");
            }
            
            return System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => iClassFeatureBehaviourType.IsAssignableFrom(type) && !type.IsAbstract)
                .ToArray();
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

        private System.Type[] GetDerivedTypes(System.Type baseType)
        {
            return System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract)
                .ToArray();
        }
        
        private Class[] FindAllClasses()
        {
            string[] guids = AssetDatabase.FindAssets("t:Class");
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Class>)
                .Where(asset => asset != null)
                .OrderBy(className => className.name)
                .ToArray();
        }
        
        private Type[] GetDerivedTypes<T>()
        {
            var baseType = typeof(T);

            return System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .OrderBy(type => type.Name) 
                .ToArray();
        }
    }
}