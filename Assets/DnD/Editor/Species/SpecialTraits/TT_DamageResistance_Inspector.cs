using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DnD.Editor.Species.SpecialTraits
{
    [CustomEditor(typeof(DamageResistance))]
    public class TT_DamageResistance_Inspector : UnityEditor.Editor
    {
        public VisualTreeAsset m_InspectorXML;

        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our Inspector UI.
            VisualElement myInspector = new VisualElement();

            // Load the UXML file.
            m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/Scripts/Game/Characters/Species/SpecialTraits/Editor/TT_DamageResistance_Inspector.uxml");

            // Instantiate the UXML.
            myInspector = m_InspectorXML.Instantiate();

            // Return the finished Inspector UI.
            return myInspector;
        }
    }
}
