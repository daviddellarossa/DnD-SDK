#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MessageLogger.Editor
{
    [InitializeOnLoad]
    public static class MessageLoggerSpawner
    {
        static MessageLoggerSpawner()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                if (Object.FindAnyObjectByType<MessageLogger>() == null)
                {
                    var go = new GameObject("MessageLogger")
                    {
                        hideFlags = HideFlags.DontSave
                    };
                    go.AddComponent<MessageLogger>();
                    Object.DontDestroyOnLoad(go);
                }
            }
        }
    }
}
#endif