using System;

namespace DnD.Code.Scripts.Characters.Classes.FeatureProperties
{
    [Serializable]
    public class SpellSlotsPerSpellLevel
    {
        public int[] SpellSlots = new int[9];

        public SpellSlotsPerSpellLevel() { }
        public SpellSlotsPerSpellLevel(int[] spellSlots)
        {
            for (int i = 0; i < this.SpellSlots.Length; i++)
            {
                SpellSlots[i] = spellSlots[i];
            }
        }
    }
}
