namespace DnD.Code.Scripts.Helpers.PathHelper
{
    public static class PathHelper
    {
        public static readonly string FolderPath = "Assets/DnD/Code/Instances";

        public static readonly string CreatureTypesPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.CreatureTypes}";
        public static readonly string DamageTypePath = $"{FolderPath}/{NameHelper.NameHelper.Naming.DamageTypes}";
        public static readonly string DicePath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Dice}";
        public static readonly string StoragePath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Storage}";
        public static readonly string TypeTraitsPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.TypeTraits}";

        public static readonly string CharacterStatsPath =
            $"{FolderPath}/{NameHelper.NameHelper.Naming.CharacterStats}";
        public static class Abilities
        {
            public static readonly string AbilitiesPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Abilities}";
            public static readonly string SkillsPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Skills}";
        }
        
        public static class Armours
        {
            public static readonly string ArmoursPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Armours}";
            public static readonly string ArmourTypesPath = $"{ArmoursPath}/{NameHelper.NameHelper.Naming.ArmourTypes}";
            public static readonly string HeavyArmoursPath = $"{ArmoursPath}/{NameHelper.NameHelper.ArmourType.HeavyArmour}";
            public static readonly string MediumArmoursPath = $"{ArmoursPath}/{NameHelper.NameHelper.ArmourType.MediumArmour}";
            public static readonly string LightArmoursPath = $"{ArmoursPath}/{NameHelper.NameHelper.ArmourType.LightArmour}";
            public static readonly string ShieldsPath = $"{ArmoursPath}/{NameHelper.NameHelper.ArmourType.Shield}";
        }

        public static class Weapons
        {
            public static readonly string WeaponsPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Weapons}";
            public static readonly string WeaponTypesPath = $"{WeaponsPath}/{NameHelper.NameHelper.Naming.WeaponTypes}";
            public static readonly string AmmunitionTypesPath = $"{WeaponsPath}/{NameHelper.NameHelper.Naming.AmmunitionTypes}";
            public static readonly string MartialMeleeWeaponsPath = $"{WeaponsPath}/{NameHelper.NameHelper.WeaponTypes.MartialMeleeWeapon}";
            public static readonly string SimpleMeleeWeaponsPath = $"{WeaponsPath}/{NameHelper.NameHelper.WeaponTypes.SimpleMeleeWeapon}";
            public static readonly string MartialRangedWeaponsPath = $"{WeaponsPath}/{NameHelper.NameHelper.WeaponTypes.MartialRangedWeapon}";
            public static readonly string SimpleRangedWeaponsPath = $"{WeaponsPath}/{NameHelper.NameHelper.WeaponTypes.SimpleRangedWeapon}";
        }

        public static class Backgrounds
        {
            public static readonly string BackgroundsPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Backgrounds}";
            public static readonly string AcolytePath = $"{BackgroundsPath}/{NameHelper.NameHelper.Backgrounds.Acolyte}";
            public static readonly string AcolyteToolsPath = $"{AcolytePath}/{NameHelper.NameHelper.Naming.Tools}";
            public static readonly string AcolyteStartingEquipmentPath = $"{AcolytePath}/{NameHelper.NameHelper.Naming.StartingEquipment}";
        }

        public static class Classes
        {
            public static readonly string ClassesPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Classes}";

            public static class Barbarian
            {
                public static string ClassName => NameHelper.NameHelper.Classes.Barbarian;
                public static string ClassPath => $"{ClassesPath}/{ClassName}";
                public static string ClassStartingEquipmentPath => $"{ClassPath}/{NameHelper.NameHelper.Naming.StartingEquipment}";
                public static string ClassLevelsPath => $"{ClassPath}/{NameHelper.NameHelper.Naming.Levels}";
                public static string ClassSubClassesPath => $"{ClassPath}/{NameHelper.NameHelper.Naming.SubClasses}";

                public static class SubClasses
                {
                    public static class PathOfTheZealot
                    {
                        public static string SubClassPath = $"{ClassSubClassesPath}/{NameHelper.NameHelper.BarbarianSubClasses.PathOfTheZealot}";
                        public static string SubClassLevelsPath => $"{SubClassPath}/{NameHelper.NameHelper.Naming.Levels}";
                    }
                
                    public static class PathOfTheWorldTree
                    {
                        public static string SubClassPath = $"{ClassSubClassesPath}/{NameHelper.NameHelper.BarbarianSubClasses.PathOfTheWorldTree}";
                        public static string SubClassLevelsPath => $"{SubClassPath}/{NameHelper.NameHelper.Naming.Levels}";
                    }
                
                    public static class PathOfTheWildHeart
                    {
                        public static string SubClassPath = $"{ClassSubClassesPath}/{NameHelper.NameHelper.BarbarianSubClasses.PathOfTheWildHeart}";
                        public static string SubClassLevelsPath => $"{SubClassPath}/{NameHelper.NameHelper.Naming.Levels}";
                    }
                
                    public static class PathOfTheBerserker
                    {
                        public static string SubClassPath = $"{ClassSubClassesPath}/{NameHelper.NameHelper.BarbarianSubClasses.PathOfTheBerserker}";
                        public static string SubClassLevelsPath => $"{SubClassPath}/{NameHelper.NameHelper.Naming.Levels}";
                    }
                }
            }
        }

        public static class Equipments
        {
            public static readonly string EquipmentPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Equipments}";
            public static readonly string EquipmentCoinsPath = $"{EquipmentPath}/{NameHelper.NameHelper.Naming.Coins}";
            public static readonly string EquipmentToolsPath = $"{EquipmentPath}/{NameHelper.NameHelper.Naming.Tools}";
        }

        public static class Feats
        {
            public static readonly string FeatsDataPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.FeatsData}";
            public static readonly string FeatCategoriesPath = $"{FeatsDataPath}/{NameHelper.NameHelper.Naming.Categories}";
            public static readonly string FeatsPath = $"{FeatsDataPath}/{NameHelper.NameHelper.Naming.Feats}";
        }

        public static class Languages
        {
            public static readonly string LanguagesPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Languages}";
            public static readonly string OriginsPath = $"{LanguagesPath}/{NameHelper.NameHelper.Naming.Origins}";
            public static readonly string RareLanguagesPath = $"{LanguagesPath}/{NameHelper.NameHelper.Naming.RareLanguages}";
            public static readonly string StandardLanguagesPath = $"{LanguagesPath}/{NameHelper.NameHelper.Naming.StandardLanguages}";
        }

        public static class Species
        {
            public static readonly string SpeciesPath = $"{FolderPath}/{NameHelper.NameHelper.Naming.Species}";

            public static class Human
            {
                public static string SpexName => NameHelper.NameHelper.Species.Human;

                public static string SpexPath => $"{SpeciesPath}/{SpexName}";
                public static string SpexSpecialTraitsPath => $"{SpexPath}/{NameHelper.NameHelper.Naming.SpecialTraits}";
            }
        }
    }
}