﻿using DnD.Code.Scripts.Items;
using UnityEngine;

namespace DnD.Code.Scripts.Equipment.Coins
{
    [CreateAssetMenu(fileName = "NewCoinValue", menuName = "Game Entities/Equipment/CoinValues")]
    public class CoinValue : ScriptableObject, IItem, ICoin
    {

        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        
        public string Abbreviation;
        public float Value;

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
