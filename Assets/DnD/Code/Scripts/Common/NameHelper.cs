using Unity.Collections.LowLevel.Unsafe;

namespace DnD.Code.Scripts.Common
{
    public static partial class NameHelper
    {
        public static class Naming
        {
            public const string Description = "Description";
            public const string Classes = "Classes";
            public const string SubClasses = "SubClasses";
            public const string Levels = "Levels";
            public const string StartingEquipment = "StartingEquipment";
            public const string Level = "Level";
            public const string SpecialTraits = "SpecialTraits";
            public const string Species = "Species";


        }
        public static class Abilities
        {
            public const string Charisma = "Charisma";
            public const string Constitution = "Constitution";
            public const string Dexterity = "Dexterity";
            public const string Intelligence = "Intelligence";
            public const string Strength = "Strength";
            public const string Wisdom = "Wisdom";

        }
        public static class DamageTypes
        {
            public const string Acid = "Acid";
            public const string Bludgeoning = "Bludgeoning";
            public const string Cold = "Cold";
            public const string Fire = "Fire";
            public const string Force = "Force";
            public const string Lightning = "Lightning";
            public const string Necrotic = "Necrotic";
            public const string Piercing = "Piercing";
            public const string Poison = "Poison";
            public const string Psychic = "Psychic";
            public const string Radiant = "Radiant";
            public const string Slashing = "Slashing";
            public const string Thunder = "Thunder";

        }

        public static class Dice
        {
            public const string D1 = "D1";
            public const string D3 = "D3";
            public const string D4 = "D4";
            public const string D6 = "D6";
            public const string D8 = "D8";
            public const string D10 = "D10";
            public const string D12 = "D12";
            public const string D20 = "D20";
            public const string D100 = "D100";

        }
        
        public static class Storage
        {
            public const string Case = "Case";
            public const string Pouch = "Pouch";
            public const string Quiver = "Quiver";

        }

        public static class MasteryProperty
        {
            public const string Cleave = "Cleave";
            public const string Graze = "Graze";
            public const string Nick = "Nick";
            public const string Push = "Push";
            public const string Sap = "Sap";
            public const string Slow = "Slow";
            public const string Topple = "Topple";
            public const string Vex = "Vex";

        }
        
        public static class Skills
        {
            public const string Acrobatics = "Acrobatics";
            public const string AnimalHandling = "AnimalHandling";
            public const string Arcana = "Arcana";
            public const string Athletics = "Athletics";
            public const string Deception = "Deception";
            public const string History = "History";
            public const string Insight = "Insight";
            public const string Intimidation = "Intimidation";
            public const string Investigation = "Investigation";
            public const string Medicine = "Medicine";
            public const string Nature = "Nature";
            public const string Perception = "Perception";
            public const string Performance = "Performance";
            public const string Persuasion = "Persuasion";
            public const string Religion = "Religion";
            public const string SleightOfHand = "SleightOfHand";
            public const string Stealth = "Stealth";
            public const string Survival = "Survival";

        }

        public static class LanguageOrigins
        {
            public const string Aberrations = "Aberrations";
            public const string Celestials = "Celestials";
            public const string DemonsOfTheAbyss = "Demons of the Abyss";
            public const string DevilsOfTheNineHells = "Devils of the Nine Hells";
            public const string Dragons = "Dragons";
            public const string DruidicCircles = "Druidic circles";
            public const string Dwarves = "Dwarves";
            public const string Elementals = "Elementals";
            public const string Elves = "Elves";
            public const string Giants = "Giants";
            public const string Gnomes = "Gnomes";
            public const string Goblinoids = "Goblinoids";
            public const string Halflings = "Halflings";
            public const string Orcs = "Orcs";
            public const string Sigil = "Sigil";
            public const string TheFeywild = "The Feywild";
            public const string TheUnderdark = "The Underdark";
            public const string VariousCriminalGuilds = "Various criminal guilds";
        }

        public static class Languages
        {
            public const string Common = "Common";
            public const string CommonSign = "Common sign";
            public const string Draconic = "Draconic";
            public const string Dwarvish = "Dwarvish";
            public const string Elvish = "Elvish";
            public const string Giant = "Giant";
            public const string Gnomish = "Gnomish";
            public const string Goblin = "Goblin";
            public const string Halfling = "Halfling";
            public const string Orc = "Orc";
            public const string Abyssal = "Abyssal";
            public const string Celestial = "Celestial";
            public const string DeepSpeech = "Deep speech";
            public const string Druidic = "Druidic";
            public const string Infernal = "Infernal";
            public const string Primordial = "Primordial";
            public const string Sylvan = "Sylvan";
            public const string ThievesCant = "Thieves' cant";
            public const string Undercommon = "Undercommon";
        }

        public static class CreatureTypes
        {
            public const string Aberration = "Aberration";
            public const string Beast = "Beast";
            public const string Celestial = "Celestial";
            public const string Construct = "Construct";
            public const string Dragon = "Dragon";
            public const string Elemental = "Elemental";
            public const string Fey = "Fey";
            public const string Fiend = "Fiend";
            public const string Giant = "Giant";
            public const string Humanoid = "Humanoid";
            public const string Monstrosity = "Monstrosity";
            public const string Ooze = "Ooze";
            public const string Plant = "Plant";
            public const string Undead = "Undead";
                        
        }

        

        public static class Species
        {
            // public const string Aasimar = "Aasimar";
            // public const string Dragonborn = "Dragonborn";
            // public const string Dwarf = "Dwarf";
            // public const string Elf = "Elf";
            // public const string Gnome = "Gnome";
            // public const string Goliath = "Goliath";
            // public const string Halfling = "Halfling";
            public const string Human = "Human";
            // public const string Orc = "Orc";
            // public const string Tiefling = "Tiefling";

        }

        public static class Classes
        {
            public const string Barbarian = "Barbarian";
            // public const string Bard = "Bard";
            // public const string Cleric = "Cleric";
            // public const string Druid = "Druid";
            // public const string Fighter = "Fighter";
            // public const string Monk = "Monk";
            // public const string Paladin = "Paladin";
            // public const string Ranger = "Ranger";
            // public const string Rogue = "Rogue";
            // public const string Sorcerer = "Sorcerer";
            // public const string Warlock = "Warlock";
            // public const string Wizard = "Wizard";

        }

        public static class SpecialTraits
        {
            public const string Resourceful = "Resourceful";
            public const string Skillful = "Skillful";
            public const string Versatile = "Versatile";
        }

        public static class TypeTraits
        {
            public const string DamageResistance = "Damage Resistance";
            public const string HasFeatByCategory = "HasFeatByCategory";
            public const string HeroicInspiration = "Heroic Inspiration";
            public const string Proficiency = "Proficiency";
            public const string SpeedBoost = "Speed Boost";

        }

        public static class FeatCategories
        {
            public const string BarbarianFeat = "Barbarian Feat";
            public const string EpicBoon = "Epic Boon";
            public const string FightingStyle = "Fighting Style";
            public const string General = "General";
            public const string Origin = "Origin";

        }

        public static class Feats
        {
            public const string MagicInitiate = "MagicInitiate";
        }

        public static class Backgrounds
        {
            public const string Acolyte = "Acolyte";

        }

        public static class CoinValues
        {
            public const string CopperPiece = "Copper Piece";
            public const string ElectrumPiece = "Electrum Piece";
            public const string GoldPiece = "Gold Piece";
            public const string PlatinumPiece = "Platinum Piece";
            public const string SilverPiece = "Silver Piece";

        }

        public static class StartingEquipmentOptions
        {
            public const string OptionA = "Option A";
            public const string OptionB = "Option B";

        }

        public static class ClassFeatures_Barbarian
        {
            public const string AbilityScoreImprovement = "Ability Score Improvement";
            public const string AnimalSpeaker = "Animal Speaker";
            public const string AspectOfTheWilds = "Aspect Of The Wilds";
            public const string BarbarianSubclass = "Barbarian Subclass";
            public const string BatteringRoots = "Battering Roots";
            public const string BranchesOfTheTree = "Branches Of The Tree";
            public const string BrutalStrike = "Brutal Strike";
            public const string DangerSense = "Danger Sense";
            public const string DivineFury = "Divine Fury";
            public const string EpicBoon = "Epic Boon";
            public const string ExtraAttack = "Extra Attack";
            public const string FanaticalFocus = "Fanatical Focus";
            public const string FastMovement = "Fast Movement";
            public const string FeralInstinct = "Feral Instinct";
            public const string Frenzy = "Frenzy";
            public const string ImprovedBrutalStrike = "Improved Brutal Strike";
            public const string IndomitableMight = "Indomitable Might";
            public const string InstinctivePounce = "Instinctive Pounce";
            public const string IntimidatingPresence = "Intimidating Presence";
            public const string MindlessRage = "Mindless Rage";
            public const string NatureSpeaker = "Nature Speaker";
            public const string PersistentRage = "Persistent Rage";
            public const string PowerOfTheWilds = "Power Of The Wilds";
            public const string PrimalChampion = "Primal Champion";
            public const string PrimalKnowledge = "Primal Knowledge";
            public const string Rage = "Rage";
            public const string RageOfTheGods = "Rage Of The Gods";
            public const string RageOfTheWilds = "Rage Of The Wilds";
            public const string RecklessAttack = "Reckless Attack";
            public const string RelentlessRage = "Relentless Rage";
            public const string Retaliation = "Retaliation";
            public const string SubclassFeature = "Subclass Feature";
            public const string TravelAlongTheTree = "Travel Along The Tree";
            public const string UnarmouredDefense = "Unarmoured Defense";
            public const string VitalityOfTheTree = "Vitality Of The Tree";
            public const string WarriorOfTheGods = "Warrior Of The Gods";
            public const string WeaponMastery = "Weapon Mastery";
            public const string ZealousPresence = "Zealous Presence";

        }

        public static class BarbarianSubClasses
        {
            public const string PathOfTheBerserker = "Path Of The Berserker";
            public const string PathOfTheWildHeart = "Path Of The Wild Heart";
            public const string PathOfTheWorldTree = "Path Of The World Tree";
            public const string PathOfTheZealot = "Path Of The Zealot";

        }
    }
}