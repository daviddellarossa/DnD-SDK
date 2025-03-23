using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.Barbarian.ClassFeatures;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Weapons;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace DnD.Editor.Initializer
{
    public class ClassBarbarianInitializer : ClassInitializer
    {
        protected override string ClassName => PathHelper.Classes.Barbarian.ClassName;
        protected override string ClassPath => PathHelper.Classes.Barbarian.ClassPath;
        protected override string ClassStartingEquipmentPath => PathHelper.Classes.Barbarian.ClassStartingEquipmentPath;
        protected override string ClassLevelsPath => PathHelper.Classes.Barbarian.ClassLevelsPath;
        protected override string ClassSubClassesPath => PathHelper.Classes.Barbarian.ClassSubClassesPath;
        
        public void InitializeBarbarian()
        {
            InitializeClass();
        }
        
        protected override Class CreateClassInstance()
        {
            var cls =  Common.CreateScriptableObject<Class>(ClassName, ClassPath);

            var dice = DiceInitializer.GetAllDice();
            var abilities = AbilitiesInitializer.GetAllAbilities();
            var skills = AbilitiesInitializer.GetAllSkills();
            var weaponTypes = WeaponsInitializer.GetAllWeaponTypes();
            var armourTypes = ArmoursInitializer.GetAllArmourTypes();

            cls.HitPointDie = dice.Single(die => die.name == NameHelper.Dice.D12);
            
            cls.PrimaryAbility = abilities.Single(ab => ab.name == NameHelper.Abilities.Strength);
            
            cls.SavingThrowProficiencies.AddRange( new [] {
                abilities.Single(ab => ab.name == NameHelper.Abilities.Strength),
                abilities.Single(ab => ab.name == NameHelper.Abilities.Constitution),
                });
            
            cls.SkillProficienciesAvailable.AddRange(new []
            {
                skills.Single(skill => skill.name == NameHelper.Skills.AnimalHandling),
                skills.Single(skill => skill.name == NameHelper.Skills.Athletics),
                skills.Single(skill => skill.name == NameHelper.Skills.Intimidation),
                skills.Single(skill => skill.name == NameHelper.Skills.Nature),
                skills.Single(skill => skill.name == NameHelper.Skills.Perception),
                skills.Single(skill => skill.name == NameHelper.Skills.Survival),

            });
            
            cls.WeaponProficiencies.AddRange(new []
            {
                weaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.SimpleMeleeWeapon),
                weaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.SimpleRangedWeapon),
                weaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.MartialMeleeWeapon),
                weaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.MartialRangedWeapon),

            });
            
            cls.ArmorTraining.AddRange(new []
            {
                armourTypes.Single(at => ((ScriptableObject)at).name == NameHelper.ArmourType.LightArmour),
                armourTypes.Single(at => ((ScriptableObject)at).name == NameHelper.ArmourType.MediumArmour),
                armourTypes.Single(at => ((ScriptableObject)at).name == NameHelper.ArmourType.Shield),

            });
                
            return cls;
        }
        
        protected override IEnumerable<SubClass> InitializeSubClasses()
        {
            
            var subClasses = new List<SubClass>();
            var pathOfTheBerserker = new PathOfTheBerserker(this.ClassSubClassesPath);
            var pathOfTheWildHeart = new PathOfTheWildHeart(this.ClassSubClassesPath);
            var pathOfTheWorldTree = new PathOfTheWorldTree(this.ClassSubClassesPath);
            var pathOfTheZealot = new PathOfTheZealot(this.ClassSubClassesPath);
            
            subClasses.AddRange(new []
            {
                pathOfTheBerserker.InitializeSubClass(),
                pathOfTheWildHeart.InitializeSubClass(),
                pathOfTheWorldTree.InitializeSubClass(),
                pathOfTheZealot.InitializeSubClass()
            });
            
            return subClasses;
        }

        private class PathOfTheZealot : ClassInitializer.SubClassInitializer
        {
            public override string ClassName => NameHelper.Classes.Barbarian;
            public override string SubClassName => NameHelper.BarbarianSubClasses.PathOfTheZealot;
            public override string SubClassPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheZealot.SubClassPath;
            public override string SubClassLevelsPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheZealot.SubClassLevelsPath;

            private readonly string parentSubClassPath;
            
            public PathOfTheZealot(string parentSubClassPath)
            {
                this.parentSubClassPath = parentSubClassPath;
            }
            
            protected override Level InitializeLevel03()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_03",
                    3,
                    2,
                    new BarbarianFP()
                    {
                        rages = 2,
                        rageDamage = 2,
                        weaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<DivineFury>($"{nameof(DivineFury)}", level),
                    Common.CreateScriptableObjectAndAddToObject<WarriorOfTheGods>($"{nameof(WarriorOfTheGods)}", level),

                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_06",
                    6,
                    3,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 2,
                        weaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<FanaticalFocus>($"{nameof(FanaticalFocus)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_10",
                    10,
                    4,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<ZealousPresence>($"{nameof(ZealousPresence)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_14",
                    14,
                    5,
                    new BarbarianFP()
                    {
                        rages = 5,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<RageOfTheGods>($"{nameof(RageOfTheGods)}", level),

                });
                
                return level;
            }
        }
        
        private class PathOfTheWorldTree : ClassInitializer.SubClassInitializer
        {
            public override string ClassName => NameHelper.Classes.Barbarian;
            public override string SubClassName => NameHelper.BarbarianSubClasses.PathOfTheWorldTree;
            public override string SubClassPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheWorldTree.SubClassPath;
            public override string SubClassLevelsPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheWorldTree.SubClassLevelsPath;
            
            private readonly string parentSubClassPath;
            
            public PathOfTheWorldTree(string parentSubClassPath)
            {
                this.parentSubClassPath = parentSubClassPath;
            }
            protected override Level InitializeLevel03()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_03",
                    3,
                    2,
                    new BarbarianFP()
                    {
                        rages = 2,
                        rageDamage = 2,
                        weaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<VitalityOfTheTree>($"{nameof(VitalityOfTheTree)}", level),

                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_06",
                    6,
                    3,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 2,
                        weaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<BranchesOfTheTree>($"{nameof(BranchesOfTheTree)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_10",
                    10,
                    4,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<BatteringRoots>($"{nameof(BatteringRoots)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_14",
                    14,
                    5,
                    new BarbarianFP()
                    {
                        rages = 5,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<TravelAlongTheTree>($"{nameof(TravelAlongTheTree)}", level),

                });
                
                return level;
            }
        }

        private class PathOfTheWildHeart : ClassInitializer.SubClassInitializer
        {
            public override string ClassName => NameHelper.Classes.Barbarian;
            public override string SubClassName => NameHelper.BarbarianSubClasses.PathOfTheWildHeart;
            public override string SubClassPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheWildHeart.SubClassPath;
            public override string SubClassLevelsPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheWildHeart.SubClassLevelsPath;
            
            private readonly string parentSubClassPath;
            
            public PathOfTheWildHeart(string parentSubClassPath)
            {
                this.parentSubClassPath = parentSubClassPath;
            }
            protected override Level InitializeLevel03()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_03",
                    3,
                    2,
                    new BarbarianFP()
                    {
                        rages = 2,
                        rageDamage = 2,
                        weaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<AnimalSpeaker>($"{nameof(AnimalSpeaker)}", level),
                    Common.CreateScriptableObjectAndAddToObject<RageOfTheWilds>($"{nameof(RageOfTheWilds)}", level),

                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_06",
                    6,
                    3,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 2,
                        weaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<AspectOfTheWilds>($"{nameof(AspectOfTheWilds)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_10",
                    10,
                    4,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<NatureSpeaker>($"{nameof(NatureSpeaker)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_14",
                    14,
                    5,
                    new BarbarianFP()
                    {
                        rages = 5,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<PowerOfTheWilds>($"{nameof(PowerOfTheWilds)}", level),

                });
                
                return level;
            }
        }

        private class PathOfTheBerserker : ClassInitializer.SubClassInitializer
        {
            public override string ClassName => NameHelper.Classes.Barbarian;
            public override string SubClassName => NameHelper.BarbarianSubClasses.PathOfTheBerserker;
            public override string SubClassPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheBerserker.SubClassPath;
            public override string SubClassLevelsPath => PathHelper.Classes.Barbarian.SubClasses.PathOfTheBerserker.SubClassLevelsPath;
            
            private readonly string parentSubClassPath;
            
            public PathOfTheBerserker(string parentSubClassPath)
            {
                this.parentSubClassPath = parentSubClassPath;
            }
            protected override Level InitializeLevel03()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_03",
                    3,
                    2,
                    new BarbarianFP()
                    {
                        rages = 2,
                        rageDamage = 2,
                        weaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<Frenzy>($"{nameof(Frenzy)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_06",
                    6,
                    3,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 2,
                        weaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<MindlessRage>($"{nameof(MindlessRage)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_10",
                    10,
                    4,
                    new BarbarianFP()
                    {
                        rages = 4,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<Retaliation>($"{nameof(Retaliation)}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}_{SubClassName}_{NameHelper.Naming.Level}_14",
                    14,
                    5,
                    new BarbarianFP()
                    {
                        rages = 5,
                        rageDamage = 3,
                        weaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    Common.CreateScriptableObjectAndAddToObject<IntimidatingPresence>($"{nameof(IntimidatingPresence)}", level),

                });
                
                return level;
            }
        }
        
        protected override StartingEquipment InitializeStartingEquipmentOptionB(Weapon[] weapons, CoinValue[] coins)
        {
            var optionB = CreateStartingEquipmentOption(
                NameHelper.StartingEquipmentOptions.OptionB,
                ClassStartingEquipmentPath,
                new StartingEquipment.ItemWithAmount[]
                {
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = coins.Single(w => w.name == NameHelper.CoinValues.GoldPiece),
                        Amount = 75
                    }
                });
            return optionB;
        }

        protected override StartingEquipment InitializeStartingEquipmentOptionA(Weapon[] weapons, CoinValue[] coins)
        {
            var optionA = CreateStartingEquipmentOption(
                NameHelper.StartingEquipmentOptions.OptionA,
                ClassStartingEquipmentPath,
                new StartingEquipment.ItemWithAmount[]
                {
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = weapons.Single(w => w.name == NameHelper.Weapons_MartialMelee.Greataxe),
                        Amount = 1
                    },
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = weapons.Single(w => w.name == NameHelper.Weapons_SimpleMelee.Handaxe),
                        Amount = 4
                    },
                    
                    // TODO: Items missing here
                    
                    new StartingEquipment.ItemWithAmount()
                    {
                        Item = coins.Single(w => w.name == NameHelper.CoinValues.GoldPiece),
                        Amount = 15
                    }
                });
            return optionA;
        }

        protected override Level InitializeLevel01()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_01",
                1,
                2,
                new BarbarianFP()
                {
                    rages = 2,
                    rageDamage = 2,
                    weaponMastery = 2
                },
                ClassLevelsPath);
            
            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<Rage>($"{nameof(Rage)}", level),
                Common.CreateScriptableObjectAndAddToObject<UnarmouredDefense>($"{nameof(UnarmouredDefense)}", level),
                Common.CreateScriptableObjectAndAddToObject<WeaponMastery>($"{nameof(WeaponMastery)}", level),
            });
            
            return level;
        }

        protected override Level InitializeLevel02()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_02",
                2,
                2,
                new BarbarianFP()
                {
                    rages = 2,
                    rageDamage = 2,
                    weaponMastery = 2
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<DangerSense>($"{nameof(DangerSense)}", level),
                Common.CreateScriptableObjectAndAddToObject<RecklessAttack>($"{nameof(RecklessAttack)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel03()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_03",
                3,
                2,
                new BarbarianFP()
                {
                    rages = 3,
                    rageDamage = 2,
                    weaponMastery = 2
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<BarbarianSubclass>($"{nameof(BarbarianSubclass)}", level),
                Common.CreateScriptableObjectAndAddToObject<PrimalKnowledge>($"{nameof(PrimalKnowledge)}", level),

            });
            return level;
        }
        
        protected override Level InitializeLevel04()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_04",
                4,
                2,
                new BarbarianFP()
                {
                    rages = 3,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel05()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_05",
                5,
                3,
                new BarbarianFP()
                {
                    rages = 3,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<ExtraAttack>($"{nameof(ExtraAttack)}", level),
                Common.CreateScriptableObjectAndAddToObject<FastMovement>($"{nameof(FastMovement)}", level),

            });
            return level;
        }
        
        protected override Level InitializeLevel06()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_06",
                6,
                3,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel07()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_07",
                7,
                3,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<FeralInstinct>($"{nameof(FeralInstinct)}", level),
                Common.CreateScriptableObjectAndAddToObject<InstinctivePounce>($"{nameof(InstinctivePounce)}", level),

            });
            return level;
        }
        
        protected override Level InitializeLevel08()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_08",
                8,
                3,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 2,
                    weaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel09()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_09",
                9,
                4,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 3,
                    weaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<BrutalStrike>($"{nameof(BrutalStrike)}", level),

            });
            return level;
        }
        
        protected override Level InitializeLevel10()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_10",
                10,
                4,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            return level;
        }
        
        protected override Level InitializeLevel11()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_11",
                11,
                4,
                new BarbarianFP()
                {
                    rages = 4,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<RelentlessRage>($"{nameof(RelentlessRage)}", level),

            });
            return level;
        }
        
        protected override Level InitializeLevel12()
        {
            var  level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_12",
                12,
                4,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel13()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_13",
                13,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<ImprovedBrutalStrike>($"{nameof(ImprovedBrutalStrike)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel14()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_14",
                14,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel15()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_15",
                15,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 3,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<PersistentRage>($"{nameof(PersistentRage)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel16()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_16",
                16,
                5,
                new BarbarianFP()
                {
                    rages = 5,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>($"{nameof(AbilityScoreImprovement)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel17()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_17",
                17,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<ImprovedBrutalStrike>($"{nameof(ImprovedBrutalStrike)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel18()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_18",
                18,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<SubclassFeature>($"{nameof(SubclassFeature)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel19()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_19",
                19,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<EpicBoon>($"{nameof(EpicBoon)}", level),

            });
            return level;
        }

        protected override Level InitializeLevel20()
        {
            var level = InitializeLevel(
                $"{NameHelper.Classes.Barbarian}_{NameHelper.Naming.Level}_20",
                20,
                6,
                new BarbarianFP()
                {
                    rages = 6,
                    rageDamage = 4,
                    weaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                Common.CreateScriptableObjectAndAddToObject<PrimalChampion>($"{nameof(PrimalChampion)}", level),

            });
            return level;
        }
    }
}