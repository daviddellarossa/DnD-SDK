using System.Collections.Generic;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Classes
{
    [CreateAssetMenu(fileName = "NewClass", menuName = "Game Entities/Character/Classes/Class")]
    public class Class : ScriptableObject,  ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        
        [SerializeField]
        private Ability primaryAbility;
        
        [SerializeField]
        private Die hitPointDie;
        
        [SerializeField]
        private List<Ability> savingThrowProficiencies = new ();
        
        [SerializeField]
        private List<Skill> skillProficienciesAvailable = new List<Skill>();

        [SerializeField] private int numberOfSkillProficienciesToChoose;
        
        [SerializeField]
        private List<WeaponType> weaponProficiencies = new ();
        
        [FormerlySerializedAs("armorTraining")] [SerializeField]
        private List<BaseArmourType> armourTraining = new List<BaseArmourType>();
        
        [SerializeField]
        private List<StartingEquipment> startingEquipmentOptions = new List<StartingEquipment>();
        
        [SerializeField]
        private Level[] levels = new Level[20];
        
        [SerializeField]
        private List<SubClass> subClasses = new List<SubClass>();
        
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
        public Ability PrimaryAbility
        {
            get => primaryAbility;
            set => primaryAbility = value;
        }
        public Die HitPointDie
        {
            get => hitPointDie;
            set => hitPointDie = value;
        }
        public List<Ability> SavingThrowProficiencies
        {
            get => savingThrowProficiencies;
            set => savingThrowProficiencies = value;
        }

        public List<Skill> SkillProficienciesAvailable
        {
            get => skillProficienciesAvailable;
            set => skillProficienciesAvailable = value;
        }
        
        public int NumberOfSkillProficienciesToChoose
        {
            get => numberOfSkillProficienciesToChoose;
            set => numberOfSkillProficienciesToChoose = value;
        }

        public List<WeaponType> WeaponProficiencies
        {
            get => weaponProficiencies;
            set => weaponProficiencies = value;
        }
        
        public List<BaseArmourType> ArmourTraining
        {
            get => armourTraining;
            set => armourTraining = value;
        }

        public List<StartingEquipment> StartingEquipmentOptions
        {
            get => startingEquipmentOptions;
            set => startingEquipmentOptions = value;
        }

        public Level[] Levels
        {
            get => levels;
            set => levels = value;
        }

        public List<SubClass> SubClasses
        {
            get => subClasses;
            set => subClasses = value;
        }
    }
}
