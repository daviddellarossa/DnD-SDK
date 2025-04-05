using UnityEngine;

namespace DeeDeeR.MessageBroker
{
    public sealed partial class MessageBroker : MonoBehaviour, IMessageBroker
    {
        private static MessageBroker _instance;

        public static MessageBroker Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Try to find it in the scene first
                    _instance = FindFirstObjectByType<MessageBroker>();

                    if (_instance == null)
                    {
                        Debug.LogError("No MessageBroker instance found in the scene.");
                    }
                }

                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject); // Optional if you want persistence across scenes
        }
    }
}