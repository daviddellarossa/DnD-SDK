using System;
using System.ComponentModel;
using DnD.Code.Scripts.Classes.FeatureProperties;
using UnityEngine;

namespace DnD.Code.Scripts.Classes.Bard.FeatureProperties
{
    [Serializable]
    public class BardFeatureStats : IClassFeatureStats
    {
        [SerializeField]
        private Die bardicDie;
        [SerializeField]
        private int cantrips;
        [SerializeField]
        private int preparedSpells;
        [SerializeField]
        private SpellSlotsPerSpellLevel spellSlots;
        //{
        //    new SpellSlotsPerSpellLevel(new[]{ 2, 0, 0, 0, 0, 0, 0, 0, 0}), //  0
            //new SpellSlotsPerSpellLevel(new[]{ 3, 0, 0, 0, 0, 0, 0, 0, 0}), //  1
            //new SpellSlotsPerSpellLevel(new[]{ 4, 2, 0, 0, 0, 0, 0, 0, 0}), //  2
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 0, 0, 0, 0, 0, 0, 0}), //  3
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 2, 0, 0, 0, 0, 0, 0}), //  4
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 0, 0, 0, 0, 0, 0}), //  5
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 1, 0, 0, 0, 0, 0}), //  6
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 2, 0, 0, 0, 0, 0}), //  7
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 1, 0, 0, 0, 0}), //  8
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 0, 0, 0, 0}), //  9
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 1, 0, 0, 0}), // 10
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 1, 0, 0, 0}), // 11
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 1, 1, 0, 0}), // 12
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 1, 1, 0, 0}), // 13
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 1, 1, 1, 0}), // 14
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 1, 1, 1, 0}), // 15
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 2, 1, 1, 1, 1}), // 16
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 3, 1, 1, 1, 1}), // 17
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 3, 2, 1, 1, 1}), // 18
            //new SpellSlotsPerSpellLevel(new[]{ 4, 3, 3, 3, 3, 2, 2, 1, 1}), // 19
        //};

        [DisplayName("Bardic Die")]
        public Die BardicDie => bardicDie;
        [DisplayName("Cantrips")]
        public int Cantrips => cantrips;
        [DisplayName("Prepared Spells")]
        public int PreparedSpells => preparedSpells;
        
        public SpellSlotsPerSpellLevel SpellSlots => this.spellSlots;

        public int GetSpellSlots(int spellLevel)
        {
            if (spellLevel < 1 || spellLevel > 9)
            {
                Debug.LogError("SpellLevel out of range. Expected values: 1-9");
            }

            return this.spellSlots.SpellSlots[spellLevel];
        }
    }
}
