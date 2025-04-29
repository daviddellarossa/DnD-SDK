// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class MenusCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Menus;
			instance.OnBackToMainMenu += Handle_OnBackToMainMenu;
			instance.OnLoadGame += Handle_OnLoadGame;
			instance.OnLoadLatestGame += Handle_OnLoadLatestGame;
			instance.OnQuitGame += Handle_OnQuitGame;
			instance.OnStartGame += Handle_OnStartGame;
		}
		protected override void Unsubscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Menus;
			instance.OnBackToMainMenu -= Handle_OnBackToMainMenu;
			instance.OnLoadGame -= Handle_OnLoadGame;
			instance.OnLoadLatestGame -= Handle_OnLoadLatestGame;
			instance.OnQuitGame -= Handle_OnQuitGame;
			instance.OnStartGame -= Handle_OnStartGame;
		}

		private void Handle_OnBackToMainMenu(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Back To Main Menu");
		}

		private void Handle_OnLoadGame(System.Object sender, DeeDeeR.MessageBroker.LoadGameEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Load Game");
		}

		private void Handle_OnLoadLatestGame(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Load Latest Game");
		}

		private void Handle_OnQuitGame(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Quit Game");
		}

		private void Handle_OnStartGame(System.Object sender, MessageBroker.MessageBrokerEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Start Game");
		}
	}
}
