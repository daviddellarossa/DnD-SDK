// Auto-generated MessageCategory
using System;

namespace MessageLogger.Editor
{
	internal class LoggerCategory : MessageCategory
	{
		protected override void Subscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Logger;
			instance.Log += Handle_Log;
		}
		protected override void Unsubscribe()
		{
			var instance = DeeDeeR.MessageBroker.MessageBroker.Instance.Logger;
			instance.Log -= Handle_Log;
		}

		private void Handle_Log(System.Object arg1, System.Object arg2, System.String arg3, UnityEngine.LogType arg4)
		{
			Logger.LogEvent(arg1?.ToString() ?? string.Empty, arg2?.ToString() ?? string.Empty, arg3);
		}
	}
}
