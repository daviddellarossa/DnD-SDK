using System;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    [Serializable]
    public class HitPoints
    {
        [SerializeField]
        private int currentHitPoints;
        
        [SerializeField]
        private int maxHitPoints;
        
        [SerializeField]
        private int tempHitPoints;

        public int CurrentHitPoints
        {
            get => currentHitPoints;
            set => currentHitPoints = value;
        }

        public int MaxHitPoints
        {
            get => maxHitPoints;
            set => maxHitPoints = value;
        }

        public int TempHitPoints
        {
            get => tempHitPoints;
            set => tempHitPoints = value;
        }
    }
}
