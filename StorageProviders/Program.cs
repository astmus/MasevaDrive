using System;
using Microsoft.Extensions.Configuration;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.Extensions.Hosting;

namespace StorageProviders
{
	class Program
	{
		static void Main(string[] args)
		{
			// create options builder
			//var builder = new LinqToDbConnectionOptionsBuilder();

			// configure connection string
			//builder.UseSQLite(@"e:\StorageDB\MediaDb.v1.sqlite");


			// pass configured options to data connection constructor
			//var dc = new DataConnection(builder.Build());



		}
		static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);
	}
}
