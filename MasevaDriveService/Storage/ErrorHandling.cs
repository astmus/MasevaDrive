using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	/// <summary>
	/// Provides FaultExceptions for all Methods Calls of a Service that fails with an Exception
	/// </summary>
	public class SvcErrorHandlerBehaviourAttribute : Attribute, IServiceBehavior
	{
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{ } //implementation not needed

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{ } //implementation not needed

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase chanDispBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = chanDispBase as ChannelDispatcher;
				if (channelDispatcher == null)
					continue;
				channelDispatcher.ErrorHandlers.Add(new SvcErrorHandler());
			}
		}
	}

	public class SvcErrorHandler : IErrorHandler
	{
		public bool HandleError(Exception error)
		{
			TelegramClient.Instance.SendNotifyAboutError(error.Message.ToString());
			return true;
		}

		public void ProvideFault(Exception error, MessageVersion version, ref Message msg)
		{
			if (error is FaultException)
				return;
					 
			msg = Message.CreateMessage(version, "somthing wrong", error.Message);
		}
	}
}
