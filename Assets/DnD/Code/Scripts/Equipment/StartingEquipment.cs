using System.Collections.Generic;
using DnD.Code.Scripts.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Equipment
{
[CreateAssetMenu(fileName = "NewStartingEquipment", menuName = "Game Entities/Character/Classes/Starting Equipment")]
    public class StartingEquipment : ScriptableObject
    {
        [SerializeReference]
        public List<EquipmentWithAmount> EquipmentsWithAmountList = new ();

        public void AddEquipmentWithAmount(IEquipment equipment, int amount = 1)
        {
            EquipmentsWithAmountList.Add(new EquipmentWithAmount { Equipment = equipment as ScriptableObject, Amount = amount });
        }

        [System.Serializable]
        public class EquipmentWithAmount
        {
            [SerializeField]
            private ScriptableObject equipment;
            [SerializeField]
            private float amount;

            public ScriptableObject Equipment
            {
                get { return equipment; }
                set { equipment = value; }
            }

            public float Amount
            {
                get { return amount; }
                set { amount = value; }
            }

            public IEquipment AsIEquipment() => Equipment as IEquipment;

        }
    }
}
