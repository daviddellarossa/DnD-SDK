using Codice.Client.Common;
using System;
using System.Linq;
using DnD.Code.Scripts.Species.SpecialTraits;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DnD.Editor.Species.SpecialTraits
{
// TODO: Cleanup of the code, display the two buttons on a single line.
    [CustomEditor(typeof(SpecialTrait))]
    public class SpecialTrait_Inspector : UnityEditor.Editor
    {
        // Path of the uxml file
        private string uxmlFilePath;
        private VisualTreeAsset m_InspectorXML;

        private ListView lvTraitTypes;
        private Button btnAddItem;
        private Button btnRemoveItem;

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

            // Get the current instance of SpecialTrait
            SpecialTrait specialTrait = (SpecialTrait)target;

            var pfTraitTypes = myInspector.Q<PropertyField>("pfTraitTypes");
            pfTraitTypes.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                myInspector.schedule.Execute(() =>
                {
                    lvTraitTypes = pfTraitTypes.Q<ListView>();
                    if (lvTraitTypes != null)
                    {
                        // Debug.Log("✅ ListView Found!");

                        lvTraitTypes.showAddRemoveFooter = false;
                    }
                    else
                    {
                        Debug.LogWarning("⚠️ ListView for Trait Types still not found.");
                    }
                }).ExecuteLater(100); // Delay execution to ensure UI is initialized
            });


            btnRemoveItem = myInspector.Q<Button>("btnRemoveItem");
            btnRemoveItem.clicked += BtnRemoveItem_clicked;

            btnAddItem = myInspector.Q<Button>("btnAddItem");
            btnAddItem.clicked += BtnAddItem_Clicked;

            //Return the finished Inspector UI.
            return myInspector;
        }

        private void BtnAddItem_Clicked()
        {
            TraitTypeSelector.ShowWindow((string selectedValue) => { OnOptionSelected(selectedValue); });
        }

        private void BtnRemoveItem_clicked()
        {
            SpecialTrait specialTrait = (SpecialTrait)target;

            var selectedIndex = lvTraitTypes.selectedIndex;
            if (selectedIndex >= 0)
            {
                bool shouldRemove = EditorUtility.DisplayDialog("Confirm Deletion",
                    $"Are you sure you want to remove at index {selectedIndex}?", "Yes", "No");

                if (shouldRemove)
                {
                    var itemToRemove = specialTrait.TraitTypes.ElementAt(selectedIndex);
                    CleanupBeforeRemoval(itemToRemove);
                    RemoveItem(selectedIndex);
                }
            }
        }

        private void CleanupBeforeRemoval(TraitType itemToRemove)
        {
            AssetDatabase.RemoveObjectFromAsset(itemToRemove);
            EditorUtility.SetDirty(target);

            AssetDatabase.SaveAssets();
        }

        // Remove an item from the list
        private void RemoveItem(int index)
        {
            SpecialTrait specialTrait = (SpecialTrait)target;
            specialTrait.TraitTypes.RemoveAt(index);
            lvTraitTypes.Rebuild(); // Refresh ListView
        }


        public void OnOptionSelected(string selectedOption)
        {
            // Handle the selected option
            Debug.Log("Option selected in component: " + selectedOption);

            // Get the path details of the current SpecialTrait
            SpecialTrait specialTrait = (SpecialTrait)target;
            var specialTraitFilePath = AssetDatabase.GetAssetPath(specialTrait);
            var specialTraitFolderPath = PathHelper.GetParentPath(specialTraitFilePath);
            var specialTraitFileName = System.IO.Path.GetFileNameWithoutExtension(specialTraitFilePath);

            // Get the type of TraitType selected by the user
            var selectedType = GetDerivedTypes(typeof(TraitType)).Single(x => x.Name == selectedOption);

            // Create a new instance of a scriptable object
            ScriptableObject newScriptableObjectInstance = ScriptableObject.CreateInstance(selectedType);

            // Name for the new file
            var newAssetFileName = $"{selectedOption}_{Guid.NewGuid().ToString()}";
            newScriptableObjectInstance.name = newAssetFileName;

            var newTraitType = newScriptableObjectInstance as TraitType;

            // Add new object to the current special trait
            specialTrait.TraitTypes.Add(newTraitType);
            AssetDatabase.AddObjectToAsset(newTraitType, specialTrait);
            EditorUtility.SetDirty(specialTrait);

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