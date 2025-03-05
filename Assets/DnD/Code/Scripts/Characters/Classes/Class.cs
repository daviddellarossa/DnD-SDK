using System.Collections.Generic;
using Assets.Scripts.Game;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Items;
using DnD.Code.Scripts.Weapons;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Classes
{
    [CreateAssetMenu(fileName = "NewClass", menuName = "Game Entities/Character/Classes/Class")]
    public class Class : ScriptableObject
    {
        public AbilityStats PrimaryAbility;
        public Die HitPointDie;
        public AbilityStats[] SavingThrowProficiencies;
        public List<Skill> SkillProficienciesAvailable = new List<Skill>();
        public List<WeaponType> WeaponProficiencies = new ();
        public List<ArmourType> ArmorTraining = new List<ArmourType>();
        public List<Shield> ShieldTraining = new List<Shield>();
        public List<StartingEquipment> StartingEquipmentOptions = new List<StartingEquipment>();
        public Level[] Levels = new Level[20];
        public List<SubClass> SubClasses = new List<SubClass>();
    }
}
