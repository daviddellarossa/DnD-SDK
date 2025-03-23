using System;
using System.Linq;
using DnD.Code.Scripts.Classes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DnD.Editor.Classes
{
    public class ClassFeatureSelector : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

        private Button btnOK;
        
        private DropdownField ddfClasses;
        private DropdownField ddfClassFeatures;

        public string SelectedValue;

        private static ClassFeatureSelector windowInstance;
        private Action<string> onItemSelected; // Callback to return the value

        public static void ShowWindow(Action<string> callback)
        {
            if (windowInstance == null)
            {
                windowInstance = CreateInstance<ClassFeatureSelector>();
                windowInstance.titleContent = new GUIContent("Select a Class Feature");
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

            
            // Find the Classes DropDownList
            ddfClasses = root.Q<DropdownField>("ddfClasses");

            // Get the list of types inheriting from Class
            var classes = FindAllClasses();
            
            // Assign the list to the DropDownField
            ddfClasses.choices = classes.Select(x => x.name).OrderBy(x => x).ToList();
            
            // Handle selection change
            ddfClasses.RegisterValueChangedCallback(evt =>
            {
                // This is called whenever the user selects a new option
                string selectedValue = evt.newValue;
                Debug.Log("Selected class: " + selectedValue);
                
                // You can also call a method on your component to handle the selection
                this.OnClassSelected(selectedValue);
            });
            
            // Find the Class Features DropDownList
            ddfClassFeatures = root.Q<DropdownField>("ddfClassFeatures");
            
            // Handle selection change
            ddfClassFeatures.RegisterValueChangedCallback(evt =>
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

            ddfClassFeatures.choices.Clear();
            ddfClassFeatures.choices = classFeatures.Select(x => x.FullName).OrderBy(x => x).ToList();
            
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
    }
}