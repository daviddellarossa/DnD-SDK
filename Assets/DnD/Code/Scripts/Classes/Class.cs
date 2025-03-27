using System.Collections.Generic;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Weapons;
using UnityEngine;

namespace DnD.Code.Scripts.Classes
{
    [CreateAssetMenu(fileName = "NewClass", menuName = "Game Entities/Character/Classes/Class")]
    public class Class : ScriptableObject,  ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        
        public Ability PrimaryAbility;
        public Die HitPointDie;
        public List<Ability> SavingThrowProficiencies = new ();
        public List<Skill> SkillProficienciesAvailable = new List<Skill>();
        public List<WeaponType> WeaponProficiencies = new ();
        [SerializeReference]
        public List<IBaseArmourType> ArmorTraining = new List<IBaseArmourType>();
        public List<StartingEquipment> StartingEquipmentOptions = new List<StartingEquipment>();
        public Level[] Levels = new Level[20];
        public List<SubClass> SubClasses = new List<SubClass>();
        
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
