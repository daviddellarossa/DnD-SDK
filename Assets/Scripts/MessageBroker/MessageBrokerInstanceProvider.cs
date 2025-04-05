using DeeDeeR.MessageBroker;
using UnityEngine;
using UnityEngine.Serialization;

namespace MessageBroker
{
    [CreateAssetMenu(menuName = "DeeDeeR/MessageBroker/InstanceProvider", fileName = "MessageBrokerInstanceProvider")]
    public class MessageBrokerInstanceProvider : ScriptableObject
    {
        [SerializeField] 
        private DeeDeeR.MessageBroker.MessageBroker messageBroker;
        
        public IMessageBroker MessageBroker => messageBroker;
    }
}
