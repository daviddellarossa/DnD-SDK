//------------------------------------------------------------------------------
// <auto-generated>
// Code auto-generated by CategoryGenerator version <version undefined>.
// Re-run the generator every time a new Message is added or removed.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEditor;
using MessageBroker;
using System.Collections.Generic;

namespace DeeDeeR.MessageBroker
{
    /// <summary>
    /// 
    /// </summary>
    public class CharacterCreatedEventArgs : MessageBrokerEventArgs, IResettable
    {
        public DnD.Code.Scripts.Characters.CharacterStats CharacterStats { get; set; }

        /// <inheritdoc cref = "IResettable.ResetState"/>
        public void ResetState()
        {
            this.CharacterStats = default;
        }

        public override string ToString()
        {
            return $", Sender: {Sender}, Target: {Target}, CharacterStats: {CharacterStats}";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MBCharacter
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<CharacterCreatedEventArgs> OnCharacterCreated;
        /// <summary>
        /// 
        /// </summary>
        public void Send_OnCharacterCreated(object sender, object target, DnD.Code.Scripts.Characters.CharacterStats characterStats)
        {
            if (sender == null)
            {
                var errorEventArgs = Common.CreateArgumentNullExceptionEventArgs("Character", target, "sender");
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLogException(sender, target, errorEventArgs);
                return;
            }

            if (characterStats == null)
            {
                var errorEventArgs = Common.CreateArgumentNullExceptionEventArgs("Character", target, "characterStats");
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLogException(sender, target, errorEventArgs);
                return;
            }

            var __eventArgs__ = MessageBrokerEventArgs.Pool<CharacterCreatedEventArgs>.Rent();
            __eventArgs__.Sender = sender;
            __eventArgs__.Target = target;
            __eventArgs__.CharacterStats = characterStats;
            __eventArgs__.EventName = "OnCharacterCreated";
            OnCharacterCreated?.Invoke(sender, __eventArgs__);
        }
    }
}