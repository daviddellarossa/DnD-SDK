// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class MenusCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Menus;
			instance.BackToMainMenu += Handle_BackToMainMenu;
			instance.LoadGame += Handle_LoadGame;
			instance.LoadLatestGame += Handle_LoadLatestGame;
			instance.QuitGame += Handle_QuitGame;
			instance.StartGame += Handle_StartGame;
		}
		protected override void Unsubscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Menus;
			instance.BackToMainMenu -= Handle_BackToMainMenu;
			instance.LoadGame -= Handle_LoadGame;
			instance.LoadLatestGame -= Handle_LoadLatestGame;
			instance.QuitGame -= Handle_QuitGame;
			instance.StartGame -= Handle_StartGame;
		}

		private void Handle_BackToMainMenu(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent(arg1?.ToString() ?? string.Empty, arg2?.ToString() ?? string.Empty, "Back To Main Menu");
		}

		private void Handle_LoadGame(System.Object arg1, System.Object arg2, Infrastructure.SaveManager.SaveGameData arg3)
		{
			Logger.LogEvent(arg1?.ToString() ?? string.Empty, arg2?.ToString() ?? string.Empty, "Load Game");
		}

		private void Handle_LoadLatestGame(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent(arg1?.ToString() ?? string.Empty, arg2?.ToString() ?? string.Empty, "Load Latest Game");
		}

		private void Handle_QuitGame(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent(arg1?.ToString() ?? string.Empty, arg2?.ToString() ?? string.Empty, "Quit Game");
		}

		private void Handle_StartGame(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent(arg1?.ToString() ?? string.Empty, arg2?.ToString() ?? string.Empty, "Start Game");
		}
	}
}
