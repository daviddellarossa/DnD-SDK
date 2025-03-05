
using DnD.Code.Scripts.Characters.Classes;
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
        
        public static SubClass InitializeSubClass(string subClassName, string subClassesPath, Level level_03, Level level_06, Level level_10, Level level_14) 
        {
            var subClassPath = $"{subClassesPath}/{subClassName}";
            Common.EnsureFolderExists(subClassPath);
            
            var subClass = Common.CreateScriptableObject<SubClass>(subClassName, subClassPath);
            subClass.Name = subClassName;
            subClass.Description = string.Empty;
            
            var subClassLevelsPath = $"{subClassPath}/{Common.LevelsSubPath}";
            Common.EnsureFolderExists(subClassLevelsPath);
            
            subClass.Level_3 = level_03;
            subClass.Level_6 = level_06;
            subClass.Level_10 = level_10;
            subClass.Level_14 = level_14;
            
            return subClass;
        }
    }
}