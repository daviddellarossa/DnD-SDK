using System.Collections.Generic;
using Assets.Scripts.Game.Equipment;
using DnD.Code.Scripts.Equipment;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Backgrounds
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
            public int Amount;

            public IEquipment AsIEquipment() => Item as IEquipment;
        }
    }
}
