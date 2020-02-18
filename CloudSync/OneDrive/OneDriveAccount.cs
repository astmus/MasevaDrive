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
using CloudSync.Framework;
using CloudSync.OneDrive;

namespace CloudSync
{
	public class OneDriveAccount
	{
		#region public properties
		public OneDriveClient Client { get; set; }
		public List<OneDriveFolder> RootFolders { get; set; }		

		[JsonIgnore]
		public ObservableCollection<Worker> CurrentWorkers { get; set; } = new ObservableCollection<Worker>();
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
				/*foreach (var worker in CurrentWorkers.Where(worker => (worker.CompletedPercent < 100)))
				{
					worker.CancelWork();
					worker.Dismantle();
				}*/
				var res = Parallel.ForEach<Worker>(CurrentWorkers.Where(worker => (worker.CompletedPercent < 100)).ToList(), new Action<Worker>((worker) => 
				{
					worker.CancelWork();
					worker.Dismantle();
				}));
			}
		}

		public Task<List<OneDriveFolder>> RequestRootFolders()
		{			
			return Client.RequestRootFolders();
		}

		public void StartSyncActiveFolders()
		{			
			if (RootFolders != null)
				foreach (var folder in RootFolders)
				{
					if (folder.IsActive)
					{
						if (!folder.HasWorkerReadySubscribers)
							folder.NewWorkerReady += OnNewWorkerReady;
						folder.StartSync();
					}
					else
						folder.Suspended = true;
				}
		}	

		static Dispatcher UIdispatcher = System.Windows.Application.Current.Dispatcher as Dispatcher;
		private void OnNewWorkerReady(Worker worker)
		{
			worker.Completed += OnWorkerCompleted;
			CurrentWorkers.Add(worker);
			worker.DoWorkAsync();					
		}

		private void OnWorkerCompleted(Worker worker, ProgressableEventArgs e)
		{
			if (e.Successfull)
				CurrentWorkers.Remove(worker);
			if (!e.Successfull && e.Error is ForceCanceledException)
				CurrentWorkers.Remove(worker);
			if (!e.Successfull && e.Error.Message.IndexOf("404") >=0)
				CurrentWorkers.Remove(worker);
		}		
	}
}
