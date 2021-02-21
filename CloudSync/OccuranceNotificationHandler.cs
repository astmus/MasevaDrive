using CloudSync.Framework;
using CloudSync.Models;
using FrameworkData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using static CloudSync.OneDriveClient;

namespace CloudSync
{
	internal class OccuranceNotificationHandler
	{
		ChannelFactory<IStorageDataDriveService> connection;		
		ConcurrentQueue<MasevaMessage> notPushedNotifications = new ConcurrentQueue<MasevaMessage>();
		IStorageDataDriveService _serviceChannel;
		IStorageDataDriveService serviceChannel
		{
			get
			{
				if (connection.State == CommunicationState.Faulted)
					_serviceChannel = null;
				
				return _serviceChannel ?? (_serviceChannel = connection.CreateChannel());
			}
		}
		
		public OccuranceNotificationHandler()
		{
			InitializeNetwork();
		}

		private void InitializeNetwork()
		{
			connection = StorageServicePipeAccessPoint.GetConnection();
			connection.Faulted += OnConnectionFall;			
			_serviceChannel = null;
		}

		private void ResendNotPuched()
		{
			MasevaMessage notification;
			while (notPushedNotifications.IsEmpty == false)
				if (notPushedNotifications.TryDequeue(out notification))
					serviceChannel.SendStorageMessage(notification);
		}

		private void OnConnectionFall(Object sender, EventArgs e)
		{
			_serviceChannel = null;
		}

		public void NotifyDownLoadSuccess(OwnerInfo owner, string pathToFile)
		{
			MasevaMessage message = MasevaMessage.NewFileObtained(owner.PrincipalName, pathToFile);
			PushMessage(message);
		}	

		public void NotifyOneDriveError(OwnerInfo owner, string messageError)
		{
			MasevaMessage message = MasevaMessage.OneDriveError(owner.PrincipalName, messageError);
			PushMessage(message);
		}

		public void NotifyDownLoadFailed(OwnerInfo owner, string fileName, string errorMessage)
		{
			MasevaMessage message = MasevaMessage.FileSyncronizationProblem(owner.PrincipalName, fileName);
			message.PayloadData = errorMessage;
			PushMessage(message);
		}

		private void PushMessage(MasevaMessage message)
		{
			try
			{
				serviceChannel.SendStorageMessage(message);
				if (notPushedNotifications.Count > 0)
					ResendNotPuched();
			}
			catch (System.Exception ex)
			{
				_serviceChannel = null;
				notPushedNotifications.Enqueue(message);
				var logger = NLog.LogManager.GetCurrentClassLogger();
				logger.Error(ex.ToString());
			}
		}
	}
}
