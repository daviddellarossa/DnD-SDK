#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MessageLogger.Editor
{
    public class MessageLoggerSettingsEditor : EditorWindow
    {
        private MessageLoggerSettings settings;

        [MenuItem("DeeDeeR/Message Logger/Settings")]
        public static void OpenWindow()
        {
            var window = GetWindow<MessageLoggerSettingsEditor>("Message Logger Settings");
            window.minSize = new Vector2(400, 300);
            window.LoadOrCreateSettings();
        }

        private void LoadOrCreateSettings()
        {
            string[] guids = AssetDatabase.FindAssets("t:MessageLoggerSettings");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                settings = AssetDatabase.LoadAssetAtPath<MessageLoggerSettings>(path);
            }
            else
            {
                settings = CreateInstance<MessageLoggerSettings>();
                AssetDatabase.CreateAsset(settings, "Assets/EditorOnly/LoggerSettings.asset");
                AssetDatabase.SaveAssets();
                Debug.Log("Created new MessageLoggerSettings at Assets/EditorOnly/LoggerSettings.asset");
            }

            EnsureAllCategoriesPresent();
        }

        private void EnsureAllCategoriesPresent()
        {
            var baseType = typeof(MessageCategory);
            var allTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));

            foreach (var type in allTypes)
            {
                settings.EnsureCategory(type.FullName);
            }

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }

        private Vector2 scroll;

        private void OnGUI()
        {
            if (settings == null)
            {
                EditorGUILayout.HelpBox("Settings not loaded.", MessageType.Warning);
                if (GUILayout.Button("Reload"))
                    LoadOrCreateSettings();
                return;
            }

            EditorGUILayout.LabelField("Logger Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            settings.globalLoggingEnabled = EditorGUILayout.Toggle("Enable Logging", settings.globalLoggingEnabled);
            settings.filterLevel = (Level)EditorGUILayout.EnumPopup("Minimum Log Level", settings.filterLevel);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Message Categories", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (var category in settings.categoryConfigs)
            {
                EditorGUILayout.BeginHorizontal();
                category.enabled = EditorGUILayout.ToggleLeft(ObjectNames.NicifyVariableName(category.categoryTypeName), category.enabled);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(settings);
            }

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Force Save"))
            {
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif