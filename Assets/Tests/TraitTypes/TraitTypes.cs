using System;
using System.Collections;
using System.Linq;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Species.SpecialTraits;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using NUnit.Framework;
using UnityEditor;

namespace Tests.TraitTypes
{
    public class TraitTypes
    {
        private TypeTrait[] _traits;
        private SpecialTrait[] _specialTraits;

        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(TypeTrait)}");
            _traits = guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<TypeTrait>)
                .Where(asset => asset != null)
                .ToArray();

            guids = AssetDatabase.FindAssets($"t:{nameof(SpecialTrait)}");
            _specialTraits = guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<SpecialTrait>)
                .Where(asset => asset != null)
                .ToArray();
        }
    }
}