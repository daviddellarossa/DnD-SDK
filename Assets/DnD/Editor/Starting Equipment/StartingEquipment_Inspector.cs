#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections.Generic;
using System;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Items;
using DnD.Code.Scripts.Weapons;
using Button = UnityEngine.UIElements.Button;

namespace Assets.Scripts.Game.Characters.Classes
{
    [CustomEditor(typeof(StartingEquipment))]
    public class StartingEquipmentEditor : Editor
    {
        private string uxmlFilePath;
        private VisualTreeAsset rootXML;

        private StartingEquipment startingEquipment;
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

            startingEquipment = (StartingEquipment)target;

            lvItems = root.Q<ListView>("lvItems");

            SetupListViewItems(lvItems);

            SetupItemSection<Shield, string>(
                root,
                nameof(Shield),
                (Shield sh) => sh.DisplayText,
                (Shield sh) => sh.DisplayText);

            SetupSection(
                root,
                FindAllArmourTypes,
                FindAllArmourOfType,
                (DnD.Code.Scripts.Armour.Armour arm) => arm.DisplayText,
                (DnD.Code.Scripts.Armour.Armour arm) => arm.DisplayText);

            SetupSection(
                root,
                FindAllWeaponTypes,
                FindAllWeaponsOfType,
                (Weapon wep) => wep.DisplayText,
                (Weapon wep) => wep.DisplayText);

            SetupItemSection<CoinValue, float>(
                root,
                nameof(CoinValue),
                (CoinValue cv) => $"{cv.DisplayText} - {cv.Value}",
                (CoinValue cv) => cv.Value);

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
                var label = new Label(item.AsIItem.DisplayText);
                label.style.flexGrow = 1;  // Allow label to take available space
                label.style.unityTextAlign = TextAnchor.LowerLeft;

                // Add the amount field
                var amountField = new IntegerField("Amount")
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

        private void SetupItemSection<TType, TOrderType>(VisualElement root, string sectionTitle, Func<TType, string> displayName, Func<TType, TOrderType> orderBy)
            where TType : ScriptableObject, IItem
        {
            var availableItems = FindAllIItemAssets<TType, TOrderType>(orderBy);

            var vePopupField = root.Q<VisualElement>($"ve{sectionTitle}").Q<VisualElement>("vePopupField");
            if (vePopupField is null)
            {
                Debug.Log($"Popup container not found for section {sectionTitle}");
                return;
            }
            var popup = new PopupField<TType>(string.Empty, availableItems, 0, item => item.name, item => item.name);
            //popup.label = $"Add {sectionTitle}";
            popup.choices = availableItems;
            popup.formatSelectedValueCallback = displayName;
            popup.formatListItemCallback = displayName;
            vePopupField.Add(popup);

            var addButton = root.Q<VisualElement>($"ve{sectionTitle}").Q<Button>("btnAdd");
            addButton.clicked += () =>
            {
                AddButton_clicked<TType>(popup);
            };
        }

        private void AddButton_clicked<TType>(PopupField<TType> popup)
            where TType : ScriptableObject, IItem
        {
            if (popup.value != null)
            {
                startingEquipment.AddItem(popup.value);
                EditorUtility.SetDirty(startingEquipment);
            };
        }

        private void SetupSection<TType, UItem>(
            VisualElement root,
            Func<List<TType>> findAllItemTypes,
            Func<TType, List<UItem>> findAllItemsOfType,
            Func<UItem, string> displayText,
            Func<UItem, string> orderBy)
            where TType : ScriptableObject
            where UItem : ScriptableObject, IItem
        {
            // Get all available ArmourTypes
            var itemTypes = findAllItemTypes();

            foreach (var itemType in itemTypes)
            {
                var vePopupField = root.Q<VisualElement>($"ve{itemType.name}").Q<VisualElement>("vePopupField");

                if (vePopupField is null)
                {
                    continue;
                }

                // Dropdown to select available Armour of this type
                var items = findAllItemsOfType(itemType);
                var popup = new PopupField<UItem>(string.Empty, items, 0, displayText, displayText);
                vePopupField.Add(popup);

                var addButton = root.Q<VisualElement>($"ve{itemType.name}").Q<Button>("btnAdd");
                addButton.clicked += () =>
                {
                    AddButton_clicked<UItem>(popup);
                };

            }
        }

        private List<ArmourType> FindAllArmourTypes()
        {
            string[] guids = AssetDatabase.FindAssets("t:ArmourType");
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ArmourType>)
                .Where(asset => asset != null)
                .ToList();
        }

        private List<DnD.Code.Scripts.Armour.Armour> FindAllArmourOfType(ArmourType armourType)
        {
            string[] guids = AssetDatabase.FindAssets("t:Armour");
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<DnD.Code.Scripts.Armour.Armour>)
                .Where(asset => asset != null && asset.Type == armourType)
                .ToList();
        }

        private List<WeaponType> FindAllWeaponTypes()
        {
            string[] guids = AssetDatabase.FindAssets("t:WeaponType");
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<WeaponType>)
                .Where(asset => asset != null)
                .ToList();
        }

        private List<Weapon> FindAllWeaponsOfType(WeaponType weaponType)
        {
            string[] guids = AssetDatabase.FindAssets("t:Weapon");
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Weapon>)
                .Where(asset => asset != null && asset.Type == weaponType)
                .ToList();
        }

        private List<T> FindAllIItemAssets<T, U>(Func<T, U> orderByDelegate) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .Where(asset => asset != null)
                .OrderBy(orderByDelegate)
                .ToList();
        }
    }
}
#endif
