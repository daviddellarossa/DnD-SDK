// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class CharacterCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Character;
			instance.OnCharacterCreated += Handle_OnCharacterCreated;
		}
		protected override void Unsubscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Character;
			instance.OnCharacterCreated -= Handle_OnCharacterCreated;
		}

		private void Handle_OnCharacterCreated(System.Object sender, DeeDeeR.MessageBroker.CharacterCreatedEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Character Created");
		}
	}
}
