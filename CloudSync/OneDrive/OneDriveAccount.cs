using CloudSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using System.Collections.Concurrent;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Data;

namespace CloudSync
{
	public class OneDriveAccount
	{
		#region public properties
		public OneDriveClient Client { get; set; }
		public List<OneDriveFolder> RootFolders { get; set; }
		public event Action<OneDriveAccount> NeedAuthorization;

		[JsonIgnore]
		public ObservableCollection<CloudWorker> CurrentWorkers { get; set; } = new ObservableCollection<CloudWorker>();
		#endregion
		private object currentWorkersLock = new object();

		public OneDriveAccount(OneDriveClient client)
		{
			Client = client;
			BindingOperations.EnableCollectionSynchronization(CurrentWorkers, currentWorkersLock);
		}		

		public void CancelAndDestructAllActiveWorkers()
		{
			lock (currentWorkersLock)
			{
				foreach (var worker in CurrentWorkers.Where(worker => (worker.CompletedPercent < 100)))
				{
					worker.CancelWork();
					worker.Dismantle();
				}
			}
		}

		public Task<List<OneDriveFolder>> RequestRootFolders()
		{			
			return Client.RequestRootFolders();
		}

		public void StartSyncActiveFolders()
		{
			if (Client.CredentialData == null) { NeedAuthorization?.Invoke(this); return; }
			if (RootFolders != null)
				foreach (var folder in RootFolders)
				{
					if (folder.IsActive && Directory.Exists(folder.PathToSync))
					{						
						if (!folder.HasWorkerReadySubscribers)
							folder.NewWorkerReady += OnNewWorkerReady;
						folder.StartSync();
					}
				}
		}

		static Dispatcher UIdispatcher = System.Windows.Application.Current.Dispatcher as Dispatcher;
		private void OnNewWorkerReady(CloudWorker worker)
		{
			worker.Completed += OnWorkerCompleted;			
			worker.DoWorkAsync();			

			//if (UIdispatcher.CheckAccess())
				CurrentWorkers.Add(worker);
			//else
				//UIdispatcher.Invoke(() => { CurrentWorkers.Add(worker); });
		}

		private void OnWorkerCompleted(CloudWorker worker, ProgressableEventArgs e)
		{
			if (worker is DownloadFileWorker)
			{
				if (e.Successfull)
				{
					//if (UIdispatcher.CheckAccess())
						CurrentWorkers.Remove(worker);
					//else
					//	UIdispatcher.Invoke(() => { CurrentWorkers.Remove(worker); });
				}				
			}			
		}
	}
}
