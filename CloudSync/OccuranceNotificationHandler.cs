using CloudSync.Framework;
using FrameworkData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using PipeNotification = System.Tuple<string , string, string, string>;

namespace CloudSync
{
	internal class OccuranceNotificationHandler : IOccurrenceNotifyReceiver
	{
		ChannelFactory<IStorageDataDriveService> connection;		
		ConcurrentQueue<PipeNotification> notPushedNotifications = new ConcurrentQueue<PipeNotification>();
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
			PipeNotification notification;
			while (notPushedNotifications.IsEmpty == false)
				if (notPushedNotifications.TryDequeue(out notification))
					serviceChannel.SendNotifyFileLoadSuccess(notification.Item1, notification.Item2, notification.Item3, notification.Item4);
		}

		private void OnConnectionFall(Object sender, EventArgs e)
		{
			_serviceChannel = null;
		}

		public void NotifyDownLoadCompletedSuccess(string email, string fileName, string formattedSize, string pathToLoadedFile)
		{
			try
			{
				serviceChannel.SendNotifyFileLoadSuccess(email, fileName, formattedSize, pathToLoadedFile);
				if (notPushedNotifications.Count > 0)
					ResendNotPuched();
			}
			catch (System.Exception ex)
			{
				_serviceChannel = null;
				notPushedNotifications.Enqueue(new PipeNotification(email,fileName,formattedSize,pathToLoadedFile));
#warning need to inplement notification toas
				var logger = NLog.LogManager.GetCurrentClassLogger();
				logger.Error(ex.ToString());
			}
		}

		public void NotifyDownLoadFailed(string email,string errorMessage)
		{
			try
			{
#warning need to inplement by analogy above
				serviceChannel.SendNotifyAboutSyncError(email, errorMessage);
				if (notPushedNotifications.Count > 0)
					ResendNotPuched();
			}
			catch (System.Exception ex)
			{
				_serviceChannel = null;
				var logger = NLog.LogManager.GetCurrentClassLogger();
				logger.Error("Send notification to telegram service error " + ex.ToString());
			}
		}
	}
}
