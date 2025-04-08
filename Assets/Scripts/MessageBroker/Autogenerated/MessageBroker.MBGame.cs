//------------------------------------------------------------------------------
// <auto-generated>
// Code auto-generated by MessageBrokerGenerator version <version undefined>.
// Re-run the generator every time a new Message is added or removed.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEditor;

namespace DeeDeeR.MessageBroker
{
	/// <summary>
	/// MessageBroker publisher for Game category.
	/// </summary>
	 public class MBGame
	{
		#region Event declaration

		/// <summary>
		/// 
		/// </summary>
		public event Action<object, object, object, object> GameOver;

		/// <summary>
		/// 
		/// </summary>
		public event Action<object, object, object, object> GamePaused;

		/// <summary>
		/// 
		/// </summary>
		public event Action<object, object, object, object> GameResumed;

		/// <summary>
		/// 
		/// </summary>
		public event Action<object, object, object, object> GameStart;

		/// <summary>
		/// 
		/// </summary>
		public event Action<object, object> GameStarted;


		#endregion

		#region Send methods

		/// <summary>
		/// Send a message of type GameOver.
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// </summary>
		public void Send_GameOver(object sender, object target)
		{
			if (sender == null)
			{
				Debug.LogError("sender is required.");
				return;
			}

			GameOver?.Invoke(sender, target, sender, target);
		}

		/// <summary>
		/// Send a message of type GamePaused.
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// </summary>
		public void Send_GamePaused(object sender, object target)
		{
			if (sender == null)
			{
				Debug.LogError("sender is required.");
				return;
			}

			GamePaused?.Invoke(sender, target, sender, target);
		}

		/// <summary>
		/// Send a message of type GameResumed.
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// </summary>
		public void Send_GameResumed(object sender, object target)
		{
			if (sender == null)
			{
				Debug.LogError("sender is required.");
				return;
			}

			GameResumed?.Invoke(sender, target, sender, target);
		}

		/// <summary>
		/// Send a message of type GameStart.
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// </summary>
		public void Send_GameStart(object sender, object target)
		{
			if (sender == null)
			{
				Debug.LogError("sender is required.");
				return;
			}

			GameStart?.Invoke(sender, target, sender, target);
		}

		/// <summary>
		/// Send a message of type GameStarted.
		/// <param name="sender">The sender of the message. Required.</param>
		/// <param name="target">The target of the message. Optional.</param>
		/// </summary>
		public void Send_GameStarted(object sender, object target)
		{
			if (sender == null)
			{
				Debug.LogError("sender is required.");
				return;
			}

			GameStarted?.Invoke(sender, target);
		}


		#endregion

	}
}
