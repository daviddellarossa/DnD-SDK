using System.Collections.Generic;
using UnityEngine;

namespace DnD.Code.Scripts.Items
{
[CreateAssetMenu(fileName = "NewStartingEquipment", menuName = "Game Entities/Character/Classes/Starting Equipment")]
    public class StartingEquipment : ScriptableObject
    {
        [SerializeReference]
        public List<ItemWithAmount> Items = new ();

        public void AddItem(IItem item, int amount = 1)
        {
            Items.Add(new ItemWithAmount { Item = item as ScriptableObject, Amount = amount });
        }

        [System.Serializable]
        public class ItemWithAmount
        {
            public ScriptableObject Item;
            public int Amount;

            public IItem AsIItem => Item as IItem;
        }
    }
}
