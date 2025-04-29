// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class LoggerCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Logger;
			instance.OnLog += Handle_OnLog;
			instance.OnLogException += Handle_OnLogException;
		}
		protected override void Unsubscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Logger;
			instance.OnLog -= Handle_OnLog;
			instance.OnLogException -= Handle_OnLogException;
		}

		private void Handle_OnLog(System.Object sender, DeeDeeR.MessageBroker.LogEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Log");
		}

		private void Handle_OnLogException(System.Object sender, DeeDeeR.MessageBroker.LogExceptionEventArgs e)
		{
			Logger.LogEvent(sender?.ToString() ?? string.Empty, e?.ToString() ?? string.Empty, "On Log Exception");
		}
	}
}
