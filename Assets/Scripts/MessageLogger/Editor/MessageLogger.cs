#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MessageBroker;
using UnityEditor;
using UnityEngine;

namespace MessageLogger.Editor
{
    public partial class MessageLogger : MonoBehaviour
    {
        private static string _MsgLogPrefix = "MsgLog";
        
        [SerializeField] private MessageLoggerSettings settings;

        private bool GlobalLoggingEnabled => settings.globalLoggingEnabled;
        private Level FilterLevel => settings.filterLevel;

        private readonly List<MessageCategory> messageCategories = new();
        
        private readonly Dictionary<Level, string> levelPrefixes = new()
        {
            { Level.Assert,     "🧪 ASSERT" },
            { Level.Debug,      "🐛 DEBUG" },
            { Level.Information,"ℹ️ INFO" },
            { Level.Event,      "✅ EVENT" },
            { Level.Warning,    "⚠️ WARNING" },
            { Level.Exception,  "🔥 EXCEPTION" },
            { Level.Error,      "❌ ERROR" },
            { Level.Panic,      "💀 PANIC" }
        };
        
        private enum UnityLogType
        {
            Log,
            Warning,
            Error
        }

        private UnityLogType GetUnityLogType(Level level)
        {
            return level switch
            {
                Level.Warning => UnityLogType.Warning,
                Level.Exception => UnityLogType.Error,
                Level.Error => UnityLogType.Error,
                Level.Panic => UnityLogType.Error,
                _ => UnityLogType.Log
            };
        }
        
        private void Awake()
        {
            if (settings == null)
            {
                LoadLoggerSettings();
            }
            
            RegisterAllCategories();
        }
        
        private void OnDestroy()
        {
            foreach (var category in messageCategories)
            {
                category.Dispose();
            }
        }
        
        private void RegisterAllCategories()
        {
            var categoryType = typeof(MessageCategory);
            var allTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in allTypes.Where(t => t.IsClass && !t.IsAbstract && categoryType.IsAssignableFrom(t)))
            {
                string typeName = type.FullName;

                settings?.EnsureCategory(typeName);

                if (settings == null || settings.IsCategoryEnabled(typeName))
                {
                    try
                    {
                        var category = (MessageCategory)Activator.CreateInstance(type);
                        category.Initialize(this);
                        messageCategories.Add(category);
                        this.Log($"Registered category: {type.Name}", Level.Debug);
                    }
                    catch (Exception ex)
                    {
                        this.Log($"Could not create {type.Name}: {ex.Message}", Level.Warning);
                    }
                }
                else
                {
                    this.Log($"Skipped disabled category: {type.Name}", Level.Debug);
                }
            }
        }
        
        void Start()
        {
            this.Log("MessageLogger started", Level.Debug);
        }
        
        public bool IsLogTypeAllowed(Level logType)
        {
            return logType >= FilterLevel;
        }
        
        public void LogEvent(MessageBrokerEventArgs message, Level level = Level.Event, UnityEngine.Object context = null)
        {
            if (settings == null || !settings.globalLoggingEnabled)
                return;

            if (level < settings.filterLevel)
                return;
            this.Log(message.ToString(), level, context);
        }
        
        public void Log(string message, Level level = Level.Event, UnityEngine.Object context = null)
        {
            if (settings == null || !settings.globalLoggingEnabled)
                return;

            if (level < settings.filterLevel)
                return;

            string prefix = levelPrefixes.TryGetValue(level, out var label) ? label : "🔍 LOG";
            string formatted = $"[{_MsgLogPrefix}][{prefix}]: {message}";

            switch (GetUnityLogType(level))
            {
                case UnityLogType.Warning:
                    Debug.LogWarning(formatted, context ?? this);
                    break;
                case UnityLogType.Error:
                    Debug.LogError(formatted, context ?? this);
                    break;
                default:
                    Debug.Log(formatted, context ?? this);
                    break;
            }
        }
        
        private void LoadLoggerSettings()
        {
            string[] guids = AssetDatabase.FindAssets("t:MessageLoggerSettings");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                settings = AssetDatabase.LoadAssetAtPath<MessageLoggerSettings>(path);
            }
            else
            {
                Debug.LogWarning("MessageLoggerSettings not found in project.");
            }
        }
    }
}
#endif