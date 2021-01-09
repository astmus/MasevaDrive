using CloudSync.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync
{
	internal class OccuranceNotificationHandler : IOccurrenceNotifyReceiver
	{
		IStorageDataDriveService connection;
		public OccuranceNotificationHandler(IStorageDataDriveService storageService)
		{
			connection = storageService;
		}

		public void NotifyDownLoadCompletedSuccess(string email, string fileName, string formattedSize, string pathToLoadedFile)
		{
			try
			{
				connection.SendNotifyFileLoadSuccess(email, fileName, formattedSize, pathToLoadedFile);
			}
			catch (System.Exception ex)
			{
				var logger = NLog.LogManager.GetCurrentClassLogger();
				logger.Error("Send notification to telegram service error " + ex.ToString());
			}
		}

		public void NotifyDownLoadFailed(string email,string errorMessage)
		{
			try
			{
				connection.SendNotifyAboutSyncError(email, errorMessage);
			}
			catch (System.Exception ex)
			{
				var logger = NLog.LogManager.GetCurrentClassLogger();
				logger.Error("Send notification to telegram service error " + ex.ToString());
			}
		}
	}
}
