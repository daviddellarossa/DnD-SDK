using System;
using System.Linq;
using DnD.Code.Scripts.Classes.ClassFeatures;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DnD.Editor.ClassFeatures
{
    [CustomEditor(typeof(ClassFeature))]
    public class ClassFeature_Inspector : UnityEditor.Editor
    {
        // Path of the uxml file
        private string uxmlFilePath;
        private VisualTreeAsset m_InspectorXML;

        private ListView lvClassFeatureBehaviour;
        private Button btnAddItem;
        private Button btnRemoveItem;
        private TextField tfClassFeatureBehaviour;

        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our Inspector UI.
            VisualElement myInspector = new VisualElement();

            if (string.IsNullOrEmpty(uxmlFilePath))
            {
                var path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                path = $"{path.Remove(path.Length - 2)}uxml";
                uxmlFilePath = path;
            }

            // Load the UXML file.
            m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlFilePath);

            // Instantiate the UXML.
            myInspector = m_InspectorXML.Instantiate();

            // Get the current instance of ClassFeature
            var classFeature = (ClassFeature)target;
            
            btnRemoveItem = myInspector.Q<Button>("btnRemoveItem");
            btnRemoveItem.clicked += BtnRemoveItem_clicked;

            btnAddItem = myInspector.Q<Button>("btnAddItem");
            btnAddItem.clicked += BtnAddItem_Clicked;

            //Return the finished Inspector UI.
            return myInspector;
        }

        private void BtnAddItem_Clicked()
        {
            ClassFeatureBehaviourSelector.ShowWindow((string selectedValue) => { OnOptionSelected(selectedValue); });
        }

        private void BtnRemoveItem_clicked()
        {
            var classFeature = (ClassFeature)target;

            //classFeature.ClassFeatureBehaviourName = null;

            EditorUtility.SetDirty(target);

            AssetDatabase.SaveAssets();
        }
        
        public void OnOptionSelected(string selectedOption)
        {
            // Handle the selected option
            Debug.Log("Option selected in component: " + selectedOption);

            // Get the path details of the current SpecialTrait
            var classFeature = (ClassFeature)target;

            var classFeatureBehaviourType = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SingleOrDefault(type => type.Name == selectedOption);

            if (classFeatureBehaviourType == null)
            {
                throw new Exception($"Could not find class feature behaviour: {selectedOption}");
            }
            
            //classFeature.ClassFeatureBehaviourName = classFeatureBehaviourType.FullName;
            
            EditorUtility.SetDirty(classFeature);

            AssetDatabase.SaveAssets();
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