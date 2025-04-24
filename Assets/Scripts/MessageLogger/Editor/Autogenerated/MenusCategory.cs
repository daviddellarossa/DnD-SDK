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
			Logger.LogEvent("Menus", arg1?.ToString(), "BackToMainMenu");
			Logger.LogEvent("Menus", arg1?.ToString(), "BackToMainMenu");
		}

		private void Handle_LoadGame(System.Object arg1, System.Object arg2, Infrastructure.SaveManager.SaveGameData arg3)
		{
			Logger.LogEvent("Menus", arg1?.ToString(), "LoadGame");
			Logger.LogEvent("Menus", arg1?.ToString(), "LoadGame");
		}

		private void Handle_LoadLatestGame(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent("Menus", arg1?.ToString(), "LoadLatestGame");
			Logger.LogEvent("Menus", arg1?.ToString(), "LoadLatestGame");
		}

		private void Handle_QuitGame(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent("Menus", arg1?.ToString(), "QuitGame");
			Logger.LogEvent("Menus", arg1?.ToString(), "QuitGame");
		}

		private void Handle_StartGame(System.Object arg1, System.Object arg2)
		{
			Logger.LogEvent("Menus", arg1?.ToString(), "StartGame");
			Logger.LogEvent("Menus", arg1?.ToString(), "StartGame");
		}
	}
}
