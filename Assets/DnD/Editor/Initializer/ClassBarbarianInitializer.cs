﻿using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.Barbarian.ClassFeatures;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Weapons;
using Infrastructure.Helpers;
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
            var cls =  ScriptableObjectHelper.CreateScriptableObject<Class>(ClassName, ClassPath);
            
            cls.DisplayName = $"{NameHelper.Naming.Classes}.{ClassName}";
            cls.DisplayDescription = $"{NameHelper.Naming.Classes}.{ClassName}.{NameHelper.Naming.Description}";

            cls.HitPointDie = Dice.Single(die => die.name == NameHelper.Dice.D12);
            
            cls.PrimaryAbility = Abilities.Single(ab => ab.name == NameHelper.Abilities.Strength);
            
            cls.SavingThrowProficiencies.AddRange( new [] {
                Abilities.Single(ab => ab.name == NameHelper.Abilities.Strength),
                Abilities.Single(ab => ab.name == NameHelper.Abilities.Constitution),
                });
            
            cls.SkillProficienciesAvailable.AddRange(new []
            {
                Skills.Single(skill => skill.name == NameHelper.Skills.AnimalHandling),
                Skills.Single(skill => skill.name == NameHelper.Skills.Athletics),
                Skills.Single(skill => skill.name == NameHelper.Skills.Intimidation),
                Skills.Single(skill => skill.name == NameHelper.Skills.Nature),
                Skills.Single(skill => skill.name == NameHelper.Skills.Perception),
                Skills.Single(skill => skill.name == NameHelper.Skills.Survival),

            });

            cls.NumberOfSkillProficienciesToChoose = 2;
            
            cls.WeaponProficiencies.AddRange(new []
            {
                WeaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.SimpleMeleeWeapon),
                WeaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.SimpleRangedWeapon),
                WeaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.MartialMeleeWeapon),
                WeaponTypes.Single(wt => wt.name == NameHelper.WeaponTypes.MartialRangedWeapon),

            });
            
            cls.ArmourTraining.AddRange(new []
            {
                ArmourTypes.Single(at => ((ScriptableObject)at).name == NameHelper.ArmourType.LightArmour),
                ArmourTypes.Single(at => ((ScriptableObject)at).name == NameHelper.ArmourType.MediumArmour),
                ArmourTypes.Single(at => ((ScriptableObject)at).name == NameHelper.ArmourType.Shield),

            });
            
            EditorUtility.SetDirty(cls);
                
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
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    3,
                    2,
                    new BarbarianFeatureStats()
                    {
                        Rages = 2,
                        RageDamage = 2,
                        WeaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<DivineFury>(NameHelper.ClassFeaturesBarbarian.DivineFury, level),
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<WarriorOfTheGods>(NameHelper.ClassFeaturesBarbarian.WarriorOfTheGods, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    6,
                    3,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 2,
                        WeaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<FanaticalFocus>(NameHelper.ClassFeaturesBarbarian.FanaticalFocus, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    10,
                    4,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<ZealousPresence>(NameHelper.ClassFeaturesBarbarian.ZealousPresence, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    14,
                    5,
                    new BarbarianFeatureStats()
                    {
                        Rages = 5,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<RageOfTheGods>(NameHelper.ClassFeaturesBarbarian.RageOfTheGods, level),

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
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    3,
                    2,
                    new BarbarianFeatureStats()
                    {
                        Rages = 2,
                        RageDamage = 2,
                        WeaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<VitalityOfTheTree>(NameHelper.ClassFeaturesBarbarian.VitalityOfTheTree, level),

                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    6,
                    3,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 2,
                        WeaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<BranchesOfTheTree>(NameHelper.ClassFeaturesBarbarian.BranchesOfTheTree, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    10,
                    4,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<BatteringRoots>(NameHelper.ClassFeaturesBarbarian.BatteringRoots, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    14,
                    5,
                    new BarbarianFeatureStats()
                    {
                        Rages = 5,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<TravelAlongTheTree>(NameHelper.ClassFeaturesBarbarian.TravelAlongTheTree, level),

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
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    3,
                    2,
                    new BarbarianFeatureStats()
                    {
                        Rages = 2,
                        RageDamage = 2,
                        WeaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<AnimalSpeaker>(NameHelper.ClassFeaturesBarbarian.AnimalSpeaker, level),
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<RageOfTheWilds>(NameHelper.ClassFeaturesBarbarian.RageOfTheWilds, level),

                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    6,
                    3,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 2,
                        WeaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<AspectOfTheWilds>(NameHelper.ClassFeaturesBarbarian.AspectOfTheWilds, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    10,
                    4,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<NatureSpeaker>(NameHelper.ClassFeaturesBarbarian.NatureSpeaker, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    14,
                    5,
                    new BarbarianFeatureStats()
                    {
                        Rages = 5,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<PowerOfTheWilds>(NameHelper.ClassFeaturesBarbarian.PowerOfTheWilds, level),

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
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    3,
                    2,
                    new BarbarianFeatureStats()
                    {
                        Rages = 2,
                        RageDamage = 2,
                        WeaponMastery = 2
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<Frenzy>($"{NameHelper.ClassFeaturesBarbarian.Frenzy}", level),
                });
                
                return level;
            }

            protected override Level InitializeLevel06()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    6,
                    3,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 2,
                        WeaponMastery = 3
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<MindlessRage>(NameHelper.ClassFeaturesBarbarian.MindlessRage, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel10()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    10,
                    4,
                    new BarbarianFeatureStats()
                    {
                        Rages = 4,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);
            
                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<Retaliation>(NameHelper.ClassFeaturesBarbarian.Retaliation, level),
                });
                
                return level;
            }

            protected override Level InitializeLevel14()
            {
                var level = InitializeLevel(
                    $"{ClassName}.{SubClassName}.{NameHelper.Naming.Level}",
                    14,
                    5,
                    new BarbarianFeatureStats()
                    {
                        Rages = 5,
                        RageDamage = 3,
                        WeaponMastery = 4
                    },
                    SubClassLevelsPath);

                level.ClassFeatures.AddRange(new ClassFeature []
                {
                    ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<IntimidatingPresence>(NameHelper.ClassFeaturesBarbarian.IntimidatingPresence, level),

                });
                
                return level;
            }
        }
        
        protected override StartingEquipment InitializeStartingEquipmentOptionB(Weapon[] weapons, CoinValue[] coins)
        {
            var optionB = CreateStartingEquipmentOption(
                NameHelper.StartingEquipmentOptions.OptionB,
                ClassStartingEquipmentPath,
                new StartingEquipment.EquipmentWithAmount[]
                {
                    new StartingEquipment.EquipmentWithAmount()
                    {
                        Equipment = coins.Single(w => w.name == NameHelper.CoinValues.GoldPiece),
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
                new StartingEquipment.EquipmentWithAmount[]
                {
                    new StartingEquipment.EquipmentWithAmount()
                    {
                        Equipment = weapons.Single(w => w.name == NameHelper.Weapons_MartialMelee.Greataxe),
                        Amount = 1
                    },
                    new StartingEquipment.EquipmentWithAmount()
                    {
                        Equipment = weapons.Single(w => w.name == NameHelper.Weapons_SimpleMelee.Handaxe),
                        Amount = 4
                    },
                    
                    // TODO: Items missing here
                    
                    new StartingEquipment.EquipmentWithAmount()
                    {
                        Equipment = coins.Single(w => w.name == NameHelper.CoinValues.GoldPiece),
                        Amount = 15
                    }
                });
            return optionA;
        }

        protected override Level InitializeLevel01()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                1,
                2,
                new BarbarianFeatureStats()
                {
                    Rages = 2,
                    RageDamage = 2,
                    WeaponMastery = 2
                },
                ClassLevelsPath);
            
            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<Rage>(NameHelper.ClassFeaturesBarbarian.Rage, level),
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<UnarmouredDefense>(NameHelper.ClassFeaturesBarbarian.UnarmouredDefense, level),
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<WeaponMastery>(NameHelper.ClassFeaturesBarbarian.WeaponMastery, level),
            });
            
            return level;
        }

        protected override Level InitializeLevel02()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                2,
                2,
                new BarbarianFeatureStats()
                {
                    Rages = 2,
                    RageDamage = 2,
                    WeaponMastery = 2
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<DangerSense>(NameHelper.ClassFeaturesBarbarian.DangerSense, level),
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<RecklessAttack>(NameHelper.ClassFeaturesBarbarian.RecklessAttack, level),
            });
            return level;
        }

        protected override Level InitializeLevel03()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                3,
                2,
                new BarbarianFeatureStats()
                {
                    Rages = 3,
                    RageDamage = 2,
                    WeaponMastery = 2
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<BarbarianSubclass>(NameHelper.ClassFeaturesBarbarian.BarbarianSubclass, level),
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<PrimalKnowledge>(NameHelper.ClassFeaturesBarbarian.PrimalKnowledge, level),
            });
            return level;
        }
        
        protected override Level InitializeLevel04()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                4,
                2,
                new BarbarianFeatureStats()
                {
                    Rages = 3,
                    RageDamage = 2,
                    WeaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>(NameHelper.ClassFeaturesBarbarian.AbilityScoreImprovement, level),
            });
            return level;
        }

        protected override Level InitializeLevel05()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                5,
                3,
                new BarbarianFeatureStats()
                {
                    Rages = 3,
                    RageDamage = 2,
                    WeaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<ExtraAttack>(NameHelper.ClassFeaturesBarbarian.ExtraAttack, level),
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<FastMovement>(NameHelper.ClassFeaturesBarbarian.FastMovement, level),
            });
            return level;
        }
        
        protected override Level InitializeLevel06()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                6,
                3,
                new BarbarianFeatureStats()
                {
                    Rages = 4,
                    RageDamage = 2,
                    WeaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<SubclassFeature>(NameHelper.ClassFeaturesBarbarian.SubclassFeature, level),
            });
            return level;
        }

        protected override Level InitializeLevel07()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                7,
                3,
                new BarbarianFeatureStats()
                {
                    Rages = 4,
                    RageDamage = 2,
                    WeaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<FeralInstinct>(NameHelper.ClassFeaturesBarbarian.FeralInstinct, level),
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<InstinctivePounce>(NameHelper.ClassFeaturesBarbarian.InstinctivePounce, level),
            });
            return level;
        }
        
        protected override Level InitializeLevel08()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                8,
                3,
                new BarbarianFeatureStats()
                {
                    Rages = 4,
                    RageDamage = 2,
                    WeaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>(NameHelper.ClassFeaturesBarbarian.AbilityScoreImprovement, level),
            });
            return level;
        }

        protected override Level InitializeLevel09()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                9,
                4,
                new BarbarianFeatureStats()
                {
                    Rages = 4,
                    RageDamage = 3,
                    WeaponMastery = 3
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<BrutalStrike>(NameHelper.ClassFeaturesBarbarian.BrutalStrike, level),
            });
            return level;
        }
        
        protected override Level InitializeLevel10()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                10,
                4,
                new BarbarianFeatureStats()
                {
                    Rages = 4,
                    RageDamage = 3,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<SubclassFeature>(NameHelper.ClassFeaturesBarbarian.SubclassFeature, level),
            });
            return level;
        }
        
        protected override Level InitializeLevel11()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                11,
                4,
                new BarbarianFeatureStats()
                {
                    Rages = 4,
                    RageDamage = 3,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<RelentlessRage>(NameHelper.ClassFeaturesBarbarian.RelentlessRage, level),
            });
            return level;
        }
        
        protected override Level InitializeLevel12()
        {
            var  level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                12,
                4,
                new BarbarianFeatureStats()
                {
                    Rages = 5,
                    RageDamage = 3,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>(NameHelper.ClassFeaturesBarbarian.AbilityScoreImprovement, level),
            });
            return level;
        }

        protected override Level InitializeLevel13()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                13,
                5,
                new BarbarianFeatureStats()
                {
                    Rages = 5,
                    RageDamage = 3,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<ImprovedBrutalStrike>(NameHelper.ClassFeaturesBarbarian.ImprovedBrutalStrike, level),
            });
            return level;
        }

        protected override Level InitializeLevel14()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                14,
                5,
                new BarbarianFeatureStats()
                {
                    Rages = 5,
                    RageDamage = 3,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<SubclassFeature>(NameHelper.ClassFeaturesBarbarian.SubclassFeature, level),
            });
            return level;
        }

        protected override Level InitializeLevel15()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                15,
                5,
                new BarbarianFeatureStats()
                {
                    Rages = 5,
                    RageDamage = 3,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<PersistentRage>(NameHelper.ClassFeaturesBarbarian.PersistentRage, level),
            });
            return level;
        }

        protected override Level InitializeLevel16()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                16,
                5,
                new BarbarianFeatureStats()
                {
                    Rages = 5,
                    RageDamage = 4,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<AbilityScoreImprovement>(NameHelper.ClassFeaturesBarbarian.AbilityScoreImprovement, level),
            });
            return level;
        }

        protected override Level InitializeLevel17()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                17,
                6,
                new BarbarianFeatureStats()
                {
                    Rages = 6,
                    RageDamage = 4,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<ImprovedBrutalStrike>(NameHelper.ClassFeaturesBarbarian.ImprovedBrutalStrike, level),
            });
            return level;
        }

        protected override Level InitializeLevel18()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                18,
                6,
                new BarbarianFeatureStats()
                {
                    Rages = 6,
                    RageDamage = 4,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<SubclassFeature>(NameHelper.ClassFeaturesBarbarian.SubclassFeature, level),
            });
            return level;
        }

        protected override Level InitializeLevel19()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                19,
                6,
                new BarbarianFeatureStats()
                {
                    Rages = 6,
                    RageDamage = 4,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<EpicBoon>(NameHelper.ClassFeaturesBarbarian.EpicBoon, level),
            });
            return level;
        }

        protected override Level InitializeLevel20()
        {
            var level = InitializeLevel(
                $"{ClassName}.{NameHelper.Naming.Level}",
                20,
                6,
                new BarbarianFeatureStats()
                {
                    Rages = 6,
                    RageDamage = 4,
                    WeaponMastery = 4
                },
                ClassLevelsPath);

            level.ClassFeatures.AddRange(new ClassFeature []
            {
                ScriptableObjectHelper.CreateScriptableObjectAndAddToObject<PrimalChampion>(NameHelper.ClassFeaturesBarbarian.PrimalChampion, level),
            });
            return level;
        }
    }
}