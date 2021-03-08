using System;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB;
using LinqToDB.SchemaProvider;
using System.Collections.Generic;
using System.Linq;
using StorageProviders.SQLite;//NetCore.DBs.SQLite;
using System.Data.SQLite;
using LinqToDB.DataProvider.SQLite;

namespace StorageProviders
{
	public class Program
	{
		public static string ConnectionStringExample()
		{
			var b = new System.Data.SQLite.SQLiteConnectionStringBuilder(@"Data Source=e:\\StorageDB\\StorageDB.sqlite");
			b.CacheSize = 8192;
			b.JournalMode = System.Data.SQLite.SQLiteJournalModeEnum.Wal;
			b.ForeignKeys = true;
			b.RecursiveTriggers = true;
			b.Enlist = true;
			b.SyncMode = System.Data.SQLite.SynchronizationModes.Normal;
			b.ReadOnly = true;
			b.Pooling = true;
			return b.ToString();
		}
		public static void Main(string[] args)
		{
			// create options builder


			/*builder. Mode = SqliteOpenMode.ReadOnly;
			builder.Cache = SqliteCacheMode.Shared;
			builder.ForeignKeys = true;
			builder.RecursiveTriggers = true;*/
			// configure connection string
			//builder.UseSQLite(@"e:\StorageDB\StorageDB.sqlite");
			var b = new System.Data.SQLite.SQLiteConnectionStringBuilder(@"Data Source=e:\\StorageDB\\StorageDB.sqlite");
			b.CacheSize = 8192;
			b.JournalMode = System.Data.SQLite.SQLiteJournalModeEnum.Wal;
			b.ForeignKeys = true;
			b.RecursiveTriggers = true;
			b.Enlist = true;
			b.PageSize = 4096;
			b.SyncMode = System.Data.SQLite.SynchronizationModes.Full;
			b.ReadOnly = true;
			b.Pooling = true;


			var o = new LinqToDbConnectionOptionsBuilder();
			var p = o.UseSQLite(b.ToString());
			//System.Data.SQLite.SQLiteFunction
			// pass configured options to data connection constructor
			//List<Item> items = null;
			/*using (var dc = new SQLiteProvider(p.Build()))
			{

				//		items = (from i in dc.Items where i.ItemFileName.StartsWith("P") select i).ToList();
			}

			foreach (var i in items)
				Console.WriteLine($"{i}");*/
			Console.ReadKey();

		}
	}


	public class ConnectionStringSettings : IConnectionStringSettings
	{
		public string? ConnectionString { get; set; } = " ";
		public string? Name { get; set; }
		public string? ProviderName { get; set; }
		public bool IsGlobal => false;
	}

	public class MySettings : ILinqToDBSettings
	{
		public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

		public string DefaultConfiguration => @"Data Source = e:\\StorageDB\\StorageDB.sqlite; Mode=ReadOnly; Cache=Shared; Pooling=True; Max Pool Size=100; Cache Size=2000; Journal Mode=Wal;";
		public string DefaultDataProvider => "SQLite";
		//options.UseConnectionString(ProviderName.SQLiteMS, hostContext.Configuration.GetConnectionString("DbConnectionString")).UseDefaultLogging(provider).Build<SQLiteDbContext>();
		//(Context.SQLite.Connection as Microsoft.Data.Sqlite.SqliteConnection).CreateCollation("NoCaseUnicode", (x, y) => string.Compare(x, y, ignoreCase: true));
		//(Context.SQLite.Connection as Microsoft.Data.Sqlite.SqliteConnection).CreateCollation("NoCaseLinguistic", (x, y) => StringComparer.InvariantCultureIgnoreCase.Compare(x, y));
		////System.Data.SQLite.SQLiteFunction
		//// pass configured options to data connection constructor
		////List<Item> items = null;
		//using (var dc = new SQLiteDbContext(b.Build()))
		//{
		//	dc.Col
		//	//		items = (from i in dc.Items where i.ItemFileName.StartsWith("P") select i).ToList();
		//}

		//foreach (var i in items)
		//	Console.WriteLine($"{i}");*/


		public IEnumerable<IConnectionStringSettings> ConnectionStrings
		{
			get
			{
				var b = new System.Data.SQLite.SQLiteConnectionStringBuilder(@"Data Source=e:\\StorageDB\\StorageDB.sqlite");
				b.CacheSize = 8192;
				b.JournalMode = System.Data.SQLite.SQLiteJournalModeEnum.Wal;
				b.ForeignKeys = true;
				b.RecursiveTriggers = true;
				b.SyncMode = System.Data.SQLite.SynchronizationModes.Full;
				b.ReadOnly = true;
				b.Pooling = true;

				yield return
					new ConnectionStringSettings
					{
						Name = "Media",
						ProviderName = ProviderName.SQLiteClassic,
						ConnectionString = b.ToString()
					};
			}
		}
	}
}