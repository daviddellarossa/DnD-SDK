// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class Game_TestCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			// var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Game_Test;
			// instance.OnGameOver += Handle_OnGameOver;
			// instance.OnGamePaused += Handle_OnGamePaused;
			// instance.OnGameResumed += Handle_OnGameResumed;
			// instance.OnGameStarted += Handle_OnGameStarted;
		}
		protected override void Unsubscribe()
		{
			// var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Game_Test;
			// instance.OnGameOver -= Handle_OnGameOver;
			// instance.OnGamePaused -= Handle_OnGamePaused;
			// instance.OnGameResumed -= Handle_OnGameResumed;
			// instance.OnGameStarted -= Handle_OnGameStarted;
		}

		private void Handle_OnGameOver(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Game Over");
		}

		private void Handle_OnGamePaused(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Game Paused");
		}

		private void Handle_OnGameResumed(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Game Resumed");
		}

		private void Handle_OnGameStarted(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Game Started");
		}
	}
}
