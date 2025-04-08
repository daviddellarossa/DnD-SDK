using System.Collections.Generic;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Equipment
{
[CreateAssetMenu(fileName = "NewStartingEquipment", menuName = "Game Entities/Character/Classes/Starting Equipment")]
    public class StartingEquipment : ScriptableObject, ILocalizable
    {
        [SerializeField] private string displayName;

        [SerializeField] private string displayDescription;
        
        [SerializeReference]
        private List<EquipmentWithAmount> equipmentsWithAmountList = new ();

        public List<EquipmentWithAmount> EquipmentsWithAmountList
        {
            get => equipmentsWithAmountList;
            set => equipmentsWithAmountList = value;
        }
        public void AddEquipmentWithAmount(IEquipment equipment, int amount = 1)
        {
            equipmentsWithAmountList.Add(new EquipmentWithAmount { Equipment = equipment as ScriptableObject, Amount = amount });
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

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string DisplayDescription
        {
            get => displayDescription;
            set => displayDescription = value;
        }
    }
}
