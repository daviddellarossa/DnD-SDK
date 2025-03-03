using System;
using System.Linq;
using DnD.Code.Scripts.Weapons.MasteryProperties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MasteryPropertySelector : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private Button btnOK;

    public string SelectedValue;

    private static ScriptableObject windowInstance;
    private Action<string> onItemSelected; // Callback to return the value

    // Change these fields if needed
    private static readonly Type typeFilter = typeof(MasteryProperty);
    private static readonly string typeName = "Mastery Property";
    private static readonly string typeNamePlural = "Mastery Properties";
    // ------------------------------------------------------

    private static readonly string windowTitle = $"Select a {typeName}...";
    private readonly string btnOKName = "btnOK";
    private readonly string btnCancelName = "btnCancel";
    private readonly string ddfTypesName = "ddfTypes";
    private readonly string txtSelectedOption = "Selected option: ";

    public static void ShowWindow(Action<string> callback)
    {
        if (windowInstance == null)
        {
            windowInstance = CreateInstance($"{typeFilter.Name}Selector");
            // windowInstance.titleContent = new GUIContent(windowTitle);
            // windowInstance.onItemSelected = callback; // Store callback function
            // windowInstance.ShowAuxWindow();
        }
        else
        {
            //windowInstance.Focus();
        }
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        btnOK = root.Q<Button>(btnOKName);

        btnOK.clicked += BtnOK_clicked;

        btnOK.SetEnabled(false);

        var btnCancel = root.Q<Button>(btnCancelName);
        btnCancel.clicked += BtnCancel_clicked;

        // Get the list of types inheriting from TraitType
        var listOfTypes = GetDerivedTypes(typeFilter);

        // Find the DropDownList
        var ddfType = root.Q<DropdownField>(ddfTypesName);

        ddfType.label = typeNamePlural;

        // Assign the list to the DropDownField
        ddfType.choices = listOfTypes.Select(x => x.Name).OrderBy(x => x).ToList();

        // Handle selection change
        ddfType.RegisterValueChangedCallback(evt =>
        {
            // This is called whenever the user selects a new option
            string selectedValue = evt.newValue;
            Debug.Log(txtSelectedOption + selectedValue);

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
        if(onItemSelected != null)
        {
            onItemSelected(this.SelectedValue);
        }

        this.Close();
    }

    private void OnOptionSelected(string selectedValue)
    {
        this.SelectedValue = selectedValue;
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
