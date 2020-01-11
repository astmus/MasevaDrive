using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetImageService
{
	partial class ImageTable
	{
		private List<StorageItem> storageItems;
		public ImageTable(List<StorageItem> items) { this.storageItems = items; }
	}
}
