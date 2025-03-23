#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Equipment;
using DnD.Code.Scripts.Equipment;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using StartingEquipment = DnD.Code.Scripts.Backgrounds.StartingEquipment;

namespace DnD.Editor.Background.StartingEquipment
{
    [CustomEditor(typeof(Code.Scripts.Backgrounds.StartingEquipment))]
    public class StartingEquipmentEditor : UnityEditor.Editor
    {
        private string uxmlFilePath;
        private VisualTreeAsset rootXML;

        private Code.Scripts.Backgrounds.StartingEquipment startingEquipment;
        private ListView lvItems;

        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our Inspector UI.
            VisualElement root = new VisualElement();

            if (string.IsNullOrEmpty(uxmlFilePath))
            {
                var path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                path = $"{path.Remove(path.Length - 2)}uxml";
                uxmlFilePath = path;
            }

            // Load the UXML file.
            rootXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlFilePath);

            // Instantiate the UXML.
            root = rootXML.Instantiate();

            startingEquipment = (Code.Scripts.Backgrounds.StartingEquipment)target;

            lvItems = root.Q<ListView>("lvItems");

            SetupListViewItems(lvItems);

            SetupEquipmentSection<IEquipment, string>(
                root,
                nameof(IEquipment),
                (IEquipment eq) => eq.DisplayName,
                (IEquipment eq) => eq.DisplayName);

            return root;
        }

        private void SetupListViewItems(ListView lvItems)
        {
            if (lvItems is null)
            {
                Debug.Log($"ListView lvItems is null.");
            }

            //lvItems.makeItem = () => new VisualElement();
            lvItems.unbindItem = (element, Index) =>
            {
            };
            lvItems.bindItem = (element, index) =>
            {
                var item = startingEquipment.Items[index];
                if (item is null || item.Item is null)
                {
                    Debug.Log("Item or Item.Item is null");
                    return;
                }

                element.Clear();

                // Create a container to hold both the DisplayText and the Amount field
                var itemContainer = new VisualElement();
                itemContainer.style.flexDirection = FlexDirection.Row;
                itemContainer.style.marginBottom = 5;
                itemContainer.style.marginLeft = 200;

                // Add the item's DisplayText (name)
                var label = new Label(item.AsIEquipment().DisplayName);
                label.style.flexGrow = 1;  // Allow label to take available space
                label.style.unityTextAlign = TextAnchor.LowerLeft;

                // Add the amount field
                var amountField = new FloatField("Amount")
                {
                    value = item.Amount,
                };

                amountField.style.minWidth = 200;

                amountField.RegisterValueChangedCallback(e =>
                {
                    item.Amount = e.newValue;
                    EditorUtility.SetDirty(startingEquipment);
                });

                itemContainer.Add(label);
                itemContainer.Add(amountField);

                element.Add(itemContainer);
            };
        }

        private void SetupEquipmentSection<TType, TOrderType>(VisualElement root, string sectionTitle, Func<IEquipment, string> displayName, Func<IEquipment, TOrderType> orderBy)
            where TType : IEquipment
        {
            var availableItems = FindAllScriptableObjectsImplementingT<IEquipment, string>((x) => x.DisplayName);

            var vePopupField = root.Q<VisualElement>($"ve{sectionTitle}").Q<VisualElement>("vePopupField");
            if (vePopupField is null)
            {
                Debug.Log($"Popup container not found for section {sectionTitle}");
                return;
            }
            var popup = new PopupField<IEquipment>(string.Empty, availableItems, 0, item => item.DisplayName, item => item.DisplayName);
            //popup.label = $"Add {sectionTitle}";
            popup.choices = availableItems;
            popup.formatSelectedValueCallback = displayName;
            popup.formatListItemCallback = displayName;
            vePopupField.Add(popup);

            var addButton = root.Q<VisualElement>($"ve{sectionTitle}").Q<Button>("btnAdd");
            addButton.clicked += () =>
            {
                AddButton_clicked(popup);
            };
        }


        private void AddButton_clicked(PopupField<IEquipment> popup)
        {
            if (popup.value != null)
            {
                startingEquipment.AddItem(popup.value);
                EditorUtility.SetDirty(startingEquipment);
            };
        }

        private List<T> FindAllScriptableObjectsImplementingT<T, U>(Func<T, U> orderByDelegate)
        {
            var equipmentList = new List<T>();
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                // Check if the asset implements T
                if (asset is T equipment)
                {
                    equipmentList.Add(equipment);
                }
            }

            return equipmentList;
        }
    }
}
#endif
