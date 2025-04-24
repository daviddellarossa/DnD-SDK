// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class GameCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Game;
			instance.GameOver += Handle_GameOver;
			instance.GamePaused += Handle_GamePaused;
			instance.GameResumed += Handle_GameResumed;
			instance.GameStarted += Handle_GameStarted;
		}
		protected override void Unsubscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Game;
			instance.GameOver -= Handle_GameOver;
			instance.GamePaused -= Handle_GamePaused;
			instance.GameResumed -= Handle_GameResumed;
			instance.GameStarted -= Handle_GameStarted;
		}

		private void Handle_GameOver(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent("Game", arg1?.ToString(), "GameOver");
			Logger.LogEvent("Game", arg1?.ToString(), "GameOver");
		}

		private void Handle_GamePaused(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent("Game", arg1?.ToString(), "GamePaused");
			Logger.LogEvent("Game", arg1?.ToString(), "GamePaused");
		}

		private void Handle_GameResumed(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent("Game", arg1?.ToString(), "GameResumed");
			Logger.LogEvent("Game", arg1?.ToString(), "GameResumed");
		}

		private void Handle_GameStarted(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent("Game", arg1?.ToString(), "GameStarted");
			Logger.LogEvent("Game", arg1?.ToString(), "GameStarted");
		}
	}
}
