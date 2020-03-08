using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using DriveApi.Storage;
using Unity.Lifetime;

namespace DriveApi.Network
{
	public class UnityConfig
	{
		public static IUnityContainer GetConfiguredContainer()
		{
			var container = new UnityContainer();
			RegisterTypes(container);
			return container;
		}

		public static void RegisterTypes(IUnityContainer container)
		{
			// ContainerControlledLifetimeManager - singleton's lifetime
			container.RegisterType<IStorageItemsProvide, StorageItemsProvider>(new ContainerControlledLifetimeManager());			

			// HierarchicalLifetimeManager - container's lifetime
			//container.RegisterType<ISameInARequest, SameInARequest>(new HierarchicalLifetimeManager());

			// TransientLifetimeManager (RegisterType's default) - no lifetime
			//container.RegisterType<IAlwaysDifferent, AlwaysDifferent>(new TransientLifetimeManager());
		}
	}
}
