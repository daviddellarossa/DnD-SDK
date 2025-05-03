using DnD.Code.Scripts.Classes.Barbarian.ClassFeatures;
using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Helpers.NameHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tests.DnD.Classes.Barbarian
{
    [TestFixture]
    public class ClassFeatures
    {
        private static IEnumerable<Type> GetAllBarbarianClassFeatureTypes()
        {
            var classFeatureInterface = typeof(IBarbarianClassFeature);
            var assembly = Assembly.GetAssembly(classFeatureInterface);
            
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && classFeatureInterface.IsAssignableFrom(t));
        }
        
        [Test]
        [TestCaseSource(nameof(GetAllBarbarianClassFeatureTypes))]
        public void ClassFeature_Properties_AreCorrectlyFormatted(Type classFeatureType)
        {
            // Arrange
            var classFeature = (ClassFeature)Activator.CreateInstance(classFeatureType);
            
            // Act
            var classFeatureName = classFeature.ClassFeatureName;
            var displayName = classFeature.DisplayName;
            var displayDescription = classFeature.DisplayDescription;
            
            // Assert
            Assert.IsNotNull(classFeatureName, $"{classFeatureType.Name} ClassFeatureName should not be null");
            Assert.IsNotEmpty(classFeatureName, $"{classFeatureType.Name} ClassFeatureName should not be empty");
            
            Assert.IsNotNull(displayName, $"{classFeatureType.Name} DisplayName should not be null");
            Assert.IsNotEmpty(displayName, $"{classFeatureType.Name} DisplayName should not be empty");
            
            Assert.IsNotNull(displayDescription, $"{classFeatureType.Name} DisplayDescription should not be null");
            Assert.IsNotEmpty(displayDescription, $"{classFeatureType.Name} DisplayDescription should not be empty");
            
            // Check if DisplayName follows the expected format
            var expectedDisplayNameFormat = $"{NameHelper.Naming.ClassFeatures}.{NameHelper.Classes.Barbarian}.{classFeatureName}";
            Assert.AreEqual(expectedDisplayNameFormat, displayName, 
                $"{classFeatureType.Name} DisplayName format is incorrect");
            
            // Check if DisplayDescription follows the expected format
            var expectedDisplayDescriptionFormat = $"{displayName}.{NameHelper.Naming.Description}";
            Assert.AreEqual(expectedDisplayDescriptionFormat, displayDescription, 
                $"{classFeatureType.Name} DisplayDescription format is incorrect");
        }
    }
}