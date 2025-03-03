using Codice.Client.Common;
using System;
using System.Drawing;
using System.Linq;
using DnD.Code.Scripts.Weapons;
using DnD.Code.Scripts.Weapons.MasteryProperties;
using DnD.Code.Scripts.Weapons.Properties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


// TODO: Cleanup of the code, display the two buttons on a single line.
[CustomEditor(typeof(Weapon))]
public class Weapon_Inspector : Editor
{
    // Path of the uxml file
    private string uxmlFilePath;
    private VisualTreeAsset m_InspectorXML;

    private ListView lvProperties;
    private PropertyField pfMasteryProperty;

    private Button btnPropertiesAddItem;
    private Button btnPropertiesRemoveItem;
    private Button btnMasteryPropertiesAddItem;
    private Button btnMasteryPropertiesRemoveItem;

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
        Weapon weapon = (Weapon)target;

        var pfProperties = myInspector.Q<PropertyField>("pfProperties");
        pfProperties.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            myInspector.schedule.Execute(() =>
            {
                lvProperties = pfProperties.Q<ListView>();
                if (lvProperties != null)
                {
                    lvProperties.showAddRemoveFooter = false;
                }
                else
                {
                    Debug.LogWarning("⚠️ ListView for Properties still not found.");
                }
            }).ExecuteLater(100); // Delay execution to ensure UI is initialized
        });

        pfMasteryProperty = myInspector.Q<PropertyField>("pfMasteryProperty");

        btnPropertiesRemoveItem = myInspector.Q<VisualElement>("veToolbarForProperties").Q<Button>("btnRemoveItem");
        btnPropertiesRemoveItem.clicked += BtnPropertiesRemoveItem_clicked;

        btnPropertiesAddItem = myInspector.Q<VisualElement>("veToolbarForProperties").Q<Button>("btnAddItem");
        btnPropertiesAddItem.clicked += BtnPropertiesAddItem_Clicked;

        btnMasteryPropertiesRemoveItem = myInspector.Q<VisualElement>("veToolbarForMasteryProperties").Q<Button>("btnRemoveItem");
        btnMasteryPropertiesRemoveItem.clicked += BtnMasteryPropertiesRemoveItem_clicked;
        btnMasteryPropertiesAddItem = myInspector.Q<VisualElement>("veToolbarForMasteryProperties").Q<Button>("btnAddItem");
        btnMasteryPropertiesAddItem.clicked += BtnMasteryPropertiesAddItem_Clicked;

        //Return the finished Inspector UI.
        return myInspector;
    }

    private void BtnMasteryPropertiesAddItem_Clicked()
    {
        MasteryPropertySelector.ShowWindow((string selectedValue) =>
        {
            OnMasteryPropertyOptionSelected(selectedValue);
        });
    }

    private void BtnMasteryPropertiesRemoveItem_clicked()
    {
        Weapon weapon = (Weapon)target;

        // Handle the selected option
        Debug.Log("Removing selected Mastery Property");

        var prevMasteryProperty = weapon.MasteryProperty;

        // Remove previous item
        if (prevMasteryProperty != null)
        {
            weapon.MasteryProperty = null;
            AssetDatabase.RemoveObjectFromAsset(prevMasteryProperty);
            EditorUtility.SetDirty(weapon);

            AssetDatabase.SaveAssets();
        }
    }

    private void BtnPropertiesAddItem_Clicked()
    {
        PropertySelector.ShowWindow((string selectedValue) =>
        {
            OnPropertyOptionSelected(selectedValue);
        });
    }

    private void BtnPropertiesRemoveItem_clicked()
    {
        Weapon weapon = (Weapon)target;

        var selectedIndex = lvProperties.selectedIndex;
        if (selectedIndex >= 0)
        {
            bool shouldRemove = EditorUtility.DisplayDialog("Confirm Deletion", $"Are you sure you want to remove at index {selectedIndex}?", "Yes", "No");

            if (shouldRemove)
            {
                var itemToRemove = weapon.Properties.ElementAt(selectedIndex);
                CleanupBeforeRemoval(itemToRemove);
                RemovePropertyItem(selectedIndex);
            }
        }
    }

    private void CleanupBeforeRemoval<T>(T itemToRemove)
        where T: UnityEngine.Object
    {
        AssetDatabase.RemoveObjectFromAsset(itemToRemove);
        EditorUtility.SetDirty(target);

        AssetDatabase.SaveAssets();
    }

    // Remove an item from the list
    private void RemovePropertyItem(int index)
    {
        Weapon weapon = (Weapon)target;
        weapon.Properties.RemoveAt(index);
        lvProperties.Rebuild(); // Refresh ListView
    }

    private void OnPropertyOptionSelected(string selectedOption)
    {
        // Handle the selected option
        Debug.Log("Option selected in component: " + selectedOption);

        // Get the path details of the current object
        Weapon weapon = (Weapon)target;
        var weaponFilePath = AssetDatabase.GetAssetPath(weapon);
        var weaponFolderPath = PathHelper.GetParentPath(weaponFilePath);
        var weaponFileName = System.IO.Path.GetFileNameWithoutExtension(weaponFilePath);

        // Get the type of Property selected by the user
        var selectedType = GetDerivedTypes(typeof(Property)).Single(x => x.Name == selectedOption);

        // Create a new instance of a scriptable object
        ScriptableObject newScriptableObjectInstance = ScriptableObject.CreateInstance(selectedType);

        // Name for the new file
        var newAssetFileName = $"{selectedOption}_{Guid.NewGuid().ToString()}";
        newScriptableObjectInstance.name = newAssetFileName;

        var newProperty = newScriptableObjectInstance as Property;
        newProperty.Name = selectedOption;

        // Add new object to the current special trait
        weapon.Properties.Add(newProperty);
        AssetDatabase.AddObjectToAsset(newProperty, weapon);
        EditorUtility.SetDirty(weapon);

        AssetDatabase.SaveAssets();
    }

    private void OnMasteryPropertyOptionSelected(string selectedOption)
    {
        // Handle the selected option
        Debug.Log("Option selected in component: " + selectedOption);

        // Get the path details of the current object
        Weapon weapon = (Weapon)target;

        var prevMasteryProperty = weapon.MasteryProperty;

        var weaponFilePath = AssetDatabase.GetAssetPath(weapon);
        var weaponFolderPath = PathHelper.GetParentPath(weaponFilePath);
        var weaponFileName = System.IO.Path.GetFileNameWithoutExtension(weaponFilePath);

        // Get the type of Property selected by the user
        var selectedType = GetDerivedTypes(typeof(MasteryProperty)).Single(x => x.Name == selectedOption);

        // Create a new instance of a scriptable object
        ScriptableObject newScriptableObjectInstance = ScriptableObject.CreateInstance(selectedType);

        // Name for the new file
        var newAssetFileName = $"{selectedOption}_{Guid.NewGuid().ToString()}";
        newScriptableObjectInstance.name = newAssetFileName;

        var newProperty = newScriptableObjectInstance as MasteryProperty;
        newProperty.Name = selectedOption;

        // Remove previous item
        if (prevMasteryProperty != null)
        {
            weapon.MasteryProperty = null;
            AssetDatabase.RemoveObjectFromAsset(prevMasteryProperty);

        }
        // Add new object to the parent
        weapon.MasteryProperty = newProperty;
        AssetDatabase.AddObjectToAsset(newProperty, weapon);
        EditorUtility.SetDirty(weapon);

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