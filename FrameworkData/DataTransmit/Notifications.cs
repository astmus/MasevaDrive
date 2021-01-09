using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkData.DataTransmit
{
	public enum ServiceType
	{
		CloudSynchronization,
		StorageInformation,
		TelegramCommunication,
		DispatchManipulation,
		StructureActualization
	}

	public enum NotificationType
	{
		CloudSynchronization,
		StorageInformation,
		TelegramCommunication,
		DispatchManipulation,
		StructureActualization
	}

	interface INotify
	{
		void OnNotificationReceive();
		void RaiseNotify();
	}

	class Notifications
	{
	}
}
