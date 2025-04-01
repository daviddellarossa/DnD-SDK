using System;
using System.Linq;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Classes.FeatureProperties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DnD.Editor.Classes
{
    [CustomEditor(typeof(Level))]
    public class Level_Inspector : UnityEditor.Editor
    {
        private string _uxmlFilePath;
        private VisualTreeAsset _rootXML;

        private ListView _lvClassFeatures;

        private Level _level;

        private VisualElement _veClassFeatureTraits;
        
        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our Inspector UI.
            VisualElement root = new VisualElement();

            if (string.IsNullOrEmpty(_uxmlFilePath))
            {
                var path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                path = $"{path.Remove(path.Length - 2)}uxml";
                _uxmlFilePath = path;
            }

            // Load the UXML file.
            _rootXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(_uxmlFilePath);

            // Instantiate the UXML.
            root = _rootXML.Instantiate();

            _level = (Level)target;


            var pfClassFeatures = root.Q<PropertyField>("pfClassFeatures");
            pfClassFeatures.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                root.schedule.Execute(() =>
                {
                    _lvClassFeatures = pfClassFeatures.Q<ListView>();
                    if (_lvClassFeatures != null)
                    {
                        // Debug.Log("✅ ListView Found!");

                        _lvClassFeatures.showAddRemoveFooter = false;
                    }
                    else
                    {
                        Debug.LogWarning("⚠️ ListView for Class Features still not found.");
                    }
                }).ExecuteLater(100); // Delay execution to ensure UI is initialized
            });


            var btnRemoveClassFeature = root.Q<VisualElement>("veClassFeaturesToolbar").Q<Button>("btnRemoveItem");
            btnRemoveClassFeature.clicked += BtnRemoveClassFeature_clicked;

            var btnAddClassFeature = root.Q<VisualElement>("veClassFeaturesToolbar").Q<Button>("btnAddItem");
            btnAddClassFeature.clicked += BtnAddClassFeature_clicked;

            
            var btnRemoveClassFeatureTrait = root.Q<VisualElement>("veClassFeatureTraitsToolbar").Q<Button>("btnRemoveItem");
            btnRemoveClassFeatureTrait.clicked += BtnRemoveClassFeatureTrait_clicked;

            var btnAddClassFeatureTrait = root.Q<VisualElement>("veClassFeatureTraitsToolbar").Q<Button>("btnAddItem");
            btnAddClassFeatureTrait.clicked += BtnAddClassFeatureTrait_clicked;
            
            _veClassFeatureTraits = root.Q<VisualElement>("veClassFeatureTraits");

            SetupClassFeatureTraitsSection(root);

            return root;
        }

        private void BtnRemoveClassFeatureTrait_clicked()
        {
            ClassFeatureTraits_RemoveButton_clicked();
        }

        private void BtnAddClassFeatureTrait_clicked()
        {
            ClassFeatureTraitSelector.ShowWindow((string selectedValue) => { ClassFeatureTraits_AddButton_clicked(selectedValue); });
        }
        
        private void BtnAddClassFeature_clicked()
        {
            ClassFeatureSelector.ShowWindow((string selectedValue) => { OnClassFeatureSelected(selectedValue); });
        }

        private void BtnRemoveClassFeature_clicked()
        {
            var selectedIndex = _lvClassFeatures.selectedIndex;
            if (selectedIndex >= 0)
            {
                bool shouldRemove = EditorUtility.DisplayDialog("Confirm Deletion",
                    $"Are you sure you want to remove at index {selectedIndex}?", "Yes", "No");

                if (shouldRemove)
                {
                    var itemToRemove = _level.ClassFeatures.ElementAt(selectedIndex);
                    CleanupBeforeRemoval(itemToRemove);
                    RemoveItem(selectedIndex);
                }
            }
        }

        private void CleanupBeforeRemoval(ClassFeature itemToRemove)
        {
            AssetDatabase.RemoveObjectFromAsset(itemToRemove);
            EditorUtility.SetDirty(target);

            AssetDatabase.SaveAssets();
        }

        // Remove an item from the list
        private void RemoveItem(int index)
        {
            _level.ClassFeatures.RemoveAt(index);
            _lvClassFeatures.Rebuild(); // Refresh ListView
        }
        
        public void OnClassFeatureSelected(string selectedOption)
        {
            // Handle the selected option
            Debug.Log("Option selected in component: " + selectedOption);
            
            // Get the type of TraitType selected by the user
            //var selectedType = GetDerivedTypes(typeof(ClassFeature)).Single(x => x.Name == selectedOption);
            var selectedType = GetType(selectedOption);
            
            // Create a new instance of a scriptable object
            ScriptableObject newScriptableObjectInstance = ScriptableObject.CreateInstance(selectedType);

            // Name for the new file
            newScriptableObjectInstance.name = GetNewAssetFileName(selectedOption);

            var newClassFeature = newScriptableObjectInstance as ClassFeature;

            // Add new class feature to the current level
            _level.ClassFeatures.Add(newClassFeature);
            AssetDatabase.AddObjectToAsset(newClassFeature, _level);
            EditorUtility.SetDirty(_level);

            AssetDatabase.SaveAssets();
        }

        private string GetNewAssetFileName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return string.Empty;
            }
            return filename.Substring(filename.LastIndexOf('.') + 1);
        }

        private void SetupClassFeatureTraitsSection(VisualElement root)
        {
            var obj = new SerializedObject(target);
            SerializedProperty arrayProperty = obj.FindProperty("classFeatureStats");

            // Create a PropertyField for the array

            var propertyField = _veClassFeatureTraits.Q<PropertyField>("pfClassFeatureTraits");
            propertyField.bindingPath = arrayProperty.propertyPath;
            propertyField.Bind(obj);
        }

        private void ClassFeatureTraits_RemoveButton_clicked()
        {
            _level.ClassFeatureStats = null;
            EditorUtility.SetDirty(_level);

            var propertyField = _veClassFeatureTraits.Q<PropertyField>("pfClassFeatureTraits");

            var obj = new SerializedObject(target);

            propertyField.Bind(obj);
        }
        
        private void ClassFeatureTraits_AddButton_clicked(string selectedOption)
        {
            if (selectedOption != null)
            {
                var selectedType = GetType(selectedOption);
                var instance = Activator.CreateInstance(selectedType);
                _level.ClassFeatureStats = (IClassFeatureStats)instance;
                EditorUtility.SetDirty(_level);

                var propertyField = _veClassFeatureTraits.Q<PropertyField>("pfClassFeatureTraits");

                var obj = new SerializedObject(target);

                propertyField.Bind(obj);
            }
        }
        
        private System.Type GetType(string typeName)
        {
            return System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SingleOrDefault(type => type.FullName == typeName);
        }
    }
}