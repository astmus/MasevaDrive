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

		public OneDriveAccount(OneDriveClient client)
		{
			Client = client;			
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
					if (folder.IsActive && Directory.Exists(folder.PathToSync) && !folder.HasWorkerReadySubscribers)
					{						
						folder.NewWorkerReady += OnNewWorkerReady;
						folder.Sync();
					}
				}
		}

		private void OnNewWorkerReady(CloudWorker worker)
		{
			CurrentWorkers.Add(worker);
			worker.Completed += OnWorkerCompleted;
			worker.DoWork();
		}

		private void OnWorkerCompleted(CloudWorker worker, ProgressableEventArgs e)
		{
			if (e.Successfull)
			{
				CurrentWorkers.Remove(worker);			
			}			
		}
	}
}
