using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MessageLogger.Editor
{
    [CreateAssetMenu(menuName = "Game/Settings/MessageLogger Settings", fileName = "MessageLogger Settings")]
    public class MessageLoggerSettings : ScriptableObject
    {
        public bool globalLoggingEnabled = false;
        public Level filterLevel = Level.Assert;
        
        public List<CategoryConfig> categoryConfigs = new();
        
        public bool IsCategoryEnabled(string typeName)
        {
            var config = categoryConfigs.Find(c => c.categoryTypeName == typeName);
            return config == null || config.enabled; // default to enabled
        }
        
        public void EnsureCategory(string typeName)
        {
            if (!categoryConfigs.Exists(c => c.categoryTypeName == typeName))
            {
                categoryConfigs.Add(new CategoryConfig { categoryTypeName = typeName, enabled = true });
            }
        }
        
        [System.Serializable]
        public class CategoryConfig
        {
            public string categoryTypeName;
            public bool enabled = true;
        }
    }
}