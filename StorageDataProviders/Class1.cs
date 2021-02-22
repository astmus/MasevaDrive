using System;
using Microsoft.EntityFrameworkCore;

namespace StorageDataProviders
{
	public class Class1
	{
		public Class1()
		{
			var B = new DbContextOptionsBuilder().UseSqlite("");

			var i = new SQLiteDbContext();
			

		}
	}
}
