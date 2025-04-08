//------------------------------------------------------------------------------
// <auto-generated>
// Code auto-generated by MessageBrokerGenerator
// Re-run the generator every time a new Message is added or removed.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeeDeeR.MessageBroker
{
	/// <summary>
	/// Interface for the Message Broker component.
	/// </summary>
	public partial interface IMessageBroker
	{
		/// <summary>
		/// Message Broker for `Character` category
		/// </summary>
		MBCharacter Character { get; }

		/// <summary>
		/// Message Broker for `Game` category
		/// </summary>
		MBGame Game { get; }

		/// <summary>
		/// Message Broker for `Menus` category
		/// </summary>
		MBMenus Menus { get; }

	}
}
