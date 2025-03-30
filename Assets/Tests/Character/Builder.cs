using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Species;
using NUnit.Framework;

namespace Tests.Character
{
    [TestFixture]
    public class Builder
    {
        private BuilderTestModel _model;
        private Fixture _fixture;
        
        private Class[] _classes;
        private Background[] _backgrounds;
        private Spex[] _species;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _model = new BuilderTestModel();

            SetupClassCollection();
            SetupBackgroundCollection();
            SetupSpeciesCollection();
            
            void SetupClassCollection()
            {
                _classes = ScriptableObjectHelper.GetAllScriptableObjects<Class>(PathHelper.Classes.ClassesPath);
                if (_classes is null || _classes.Any() == false)
                {
                    Assert.Inconclusive("Class not found.");
                }
            }

            void SetupBackgroundCollection()
            {
                _backgrounds =
                    ScriptableObjectHelper.GetAllScriptableObjects<Background>(PathHelper.Backgrounds.BackgroundsPath);
                if (_backgrounds is null || _backgrounds.Any() == false)
                {
                    Assert.Inconclusive("Background not found.");
                }
            }
            
            void SetupSpeciesCollection()
            {
                _species =
                    ScriptableObjectHelper.GetAllScriptableObjects<Spex>(PathHelper.Species.SpeciesPath);
                if (_species is null || _species.Any() == false)
                {
                    Assert.Inconclusive("Species not found.");
                }
            }
        }
        
        [Test, AutoData]
        public void CheckName_Should_Return_True_When_Name_Assigned(string name)
        {
            _model.SetName(name);
            Assert.That(_model.CheckName(), Is.True);
        }
        
        [Test]
        public void CheckName_Should_Return_False_When_Name_Not_Assigned()
        {
            Assert.That(_model.CheckName(), Is.False);
        }
        
        [Test]
        public void CheckClass_Should_Return_True_When_Class_Assigned()
        {
            _model.SetClass(_classes.First());
            Assert.That(_model.CheckClass(), Is.True);
        }
        
        [Test]
        public void CheckClass_Should_Return_False_When_Class_Not_Assigned()
        {
            Assert.That(_model.CheckClass(), Is.False);
        }
        
        [Test]
        [Ignore("At the moment there is no class with no subclasses")]
        public void CheckSubClass_Should_Return_True_When_Class_HasNo_SubClass()
        {
            var @class = _classes.FirstOrDefault(x => x.SubClasses.Count == 0);
            if (@class is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@class);
            Assert.That(_model.CheckSubClass(), Is.True);
        }

        [Test]
        public void CheckSubClass_Should_Return_False_When_Class_Has_SubClasses_And_No_SubClass_Is_Assigned()
        {
            var @class = _classes.FirstOrDefault(x => x.SubClasses.Count > 0);
            if (@class is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@class);
            
            Assert.That(_model.CheckSubClass(), Is.False);
        }
        
        [Test]
        [Ignore("This test needs two classes with subclasses available. At the moment only one class is available.")]
        public void CheckSubClass_Should_Return_False_When_Class_Has_SubClasses_And_Wrong_SubClass_Is_Assigned()
        {
            var classAssigned = _classes.FirstOrDefault(x => x.SubClasses.Count > 0);
            if (classAssigned is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@classAssigned);
            
            var otherClass = _classes.Skip(1).FirstOrDefault(x => x.SubClasses.Count > 0);
            if (otherClass is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetSubClass(otherClass.SubClasses.First());
            
            Assert.That(_model.CheckSubClass(), Is.False);
        }

        [Test]
        public void CheckSubClass_Should_Return_True_When_Class_Has_SubClasses_And_SubClass_Is_Assigned()
        {
            var classAssigned = _classes.FirstOrDefault(x => x.SubClasses.Count > 0);
            if (classAssigned is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@classAssigned);
            
            _model.SetSubClass(classAssigned.SubClasses.First());
            
            Assert.That(_model.CheckSubClass(), Is.True);
        }
        
        [Test]
        public void CheckBackground_Should_Return_True_When_Background_Assigned()
        {
            var background = _backgrounds.FirstOrDefault();
            if (background is null)
            {
                Assert.Inconclusive("Background not found.");
            }
            
            _model.SetBackground(background);
            Assert.That(_model.CheckBackground(), Is.True);
        }
        
        [Test]
        public void CheckBackground_Should_Return_False_When_Background_Not_Assigned()
        {
            Assert.That(_model.CheckBackground(), Is.False);
        }
        
        [Test]
        public void CheckSpex_Should_Return_True_When_Spex_Assigned()
        {
            var spex = _species.FirstOrDefault();
            if (spex is null)
            {
                Assert.Inconclusive("Spex not found.");
            }
            
            _model.SetSpex(spex);
            Assert.That(_model.CheckSpex(), Is.True);
        }
        
        [Test]
        public void CheckSpex_Should_Return_False_When_Spex_Not_Assigned()
        {
            Assert.That(_model.CheckSpex(), Is.False);
        }

    }

    public class BuilderTestModel : CharacterStats.Builder
    {
        public new bool CheckName() => base.CheckName();
        public new bool CheckClass() => base.CheckClass();
        public new bool CheckSubClass() => base.CheckSubClass();
        public new bool CheckBackground() => base.CheckBackground();
        
        public new bool CheckSpex() => base.CheckSpex();
    }
}