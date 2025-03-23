using System.Collections.Generic;
using DnD.Code.Scripts.Equipment;
using UnityEngine;

namespace DnD.Code.Scripts.Backgrounds
{
    [CreateAssetMenu(fileName = "NewStartingEquipment", menuName = "Game Entities/Character/Background/Starting Equipment")]
    public class StartingEquipment : ScriptableObject
    {
        [SerializeReference]
        public List<EquipmentWithAmount> Items = new ();

        public void AddItem(IEquipment item, int amount = 1)
        {
            Items.Add(new EquipmentWithAmount { Item = item as ScriptableObject, Amount = amount });
        }

        [System.Serializable]
        public class EquipmentWithAmount
        {
            public ScriptableObject Item;
            public float Amount;

            public IEquipment AsIEquipment() => Item as IEquipment;

            public EquipmentWithAmount()
            {
                
            }

            public EquipmentWithAmount(ScriptableObject item, float amount)
            {
                this.Item = item;
                this.Amount = amount;
            }
        }
    }
}
