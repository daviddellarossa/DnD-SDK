// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class CharacterCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Character;
			instance.CharacterCreated += Handle_CharacterCreated;
		}
		protected override void Unsubscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Character;
			instance.CharacterCreated -= Handle_CharacterCreated;
		}

		private void Handle_CharacterCreated(System.Object arg1, System.Object arg2, DnD.Code.Scripts.Characters.CharacterStats arg3)
		{
			Logger.LogEvent(arg1?.ToString() ?? string.Empty, arg2?.ToString() ?? string.Empty, "Character Created");
		}
	}
}
