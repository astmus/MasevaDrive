using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetImageService
{
	partial class ImageTable
	{
		private List<string> m_data;
		public ImageTable(List<string> data) { this.m_data = data; }
	}
}
