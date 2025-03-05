
using DnD.Code.Scripts.Equipment;
using UnityEngine;

namespace DnD.Editor.Initializer
{
    public static class Classes
    {
        public static readonly int ClassLevels = 20;

        public static StartingEquipment CreateStartingEquipmentOption(string optionName, Object parent, StartingEquipment.ItemWithAmount[] itemsWithAmount)
        {
            var startingEquipment = Common.CreateScriptableObjectAndAddToObject<StartingEquipment>(optionName, parent);

            foreach (var item in itemsWithAmount)
            {
                startingEquipment.Items.Add(item);
            }
            return startingEquipment;
        }
        
        public static StartingEquipment CreateStartingEquipmentOption(string optionName, string assetPath, StartingEquipment.ItemWithAmount[] itemsWithAmount)
        {
            var startingEquipment = Common.CreateScriptableObject<StartingEquipment>(optionName, assetPath);

            foreach (var item in itemsWithAmount)
            {
                startingEquipment.Items.Add(item);
            }
            return startingEquipment;
        }
    }
}