﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Data.SQLite;

namespace StorageProviders.SQLite
{
	public partial class SQLiteStorage : LinqToDB.Data.DataConnection
	{
		[SQLiteFunction(FuncType = FunctionType.Collation, Name = "NoCaseUnicode")]
		public class NoCaseUnicode : SQLiteFunction
		{
			public override int Compare(string x, string y) => string.Compare(x, y, ignoreCase: true);
		}

		[SQLiteFunction(FuncType = FunctionType.Collation, Name = "NoCaseLinguistic")]
		public class NoCaseLinguistic : SQLiteFunction
		{
			public override int Compare(string x, string y) => string.Compare(x, y, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}

namespace StorageProviders.SQLite.Extensions
{
	public class LinqToDbLoggerFactoryAdapter
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<DataConnection> _logger;

		public LinqToDbLoggerFactoryAdapter(ILoggerFactory loggerFactory)
		{
			_loggerFactory = loggerFactory;
			_logger = _loggerFactory.CreateLogger<DataConnection>();
		}

		public void OnTrace(string? message, string? category, TraceLevel level)
		{
			var logLevel = level switch
			{
				TraceLevel.Error => LogLevel.Error,
				TraceLevel.Info => LogLevel.Information,
				TraceLevel.Verbose => LogLevel.Trace,
				TraceLevel.Warning => LogLevel.Warning,
				_ => LogLevel.None,
			};

			_logger.Log(logLevel, 0, message, null, (s, exception) => s);
		}
	}	

	public static class ServiceConfigurationExtensions
	{
		/// <summary>
		/// Configures the connection to use the <see cref="ILoggerFactory"/> resolved from the container.
		/// </summary>
		/// <param name="builder">Builder to configure.</param>
		/// <param name="provider">Container used to resolve the factory.</param>
		/// <returns>The builder instance so calls can be chained.</returns>
		public static LinqToDbConnectionOptionsBuilder UseDefaultLogging(this LinqToDbConnectionOptionsBuilder builder,
			IServiceProvider provider)
		{
			var factory = provider.GetRequiredService<ILoggerFactory>();
			return UseLoggerFactory(builder, factory);
		}

		/// <summary>
		/// Configures the connection to use the <see cref="ILoggerFactory"/> passed in.
		/// </summary>
		/// <param name="builder">Builder to configure.</param>
		/// <param name="factory">Factory used to resolve loggers.</param>
		/// <returns>The builder instance so calls can be chained.</returns>
		public static LinqToDbConnectionOptionsBuilder UseLoggerFactory(this LinqToDbConnectionOptionsBuilder builder,
			ILoggerFactory factory)
		{
			var adapter = new LinqToDbLoggerFactoryAdapter(factory);
			return builder.WithTraceLevel(TraceLevel.Verbose).WriteTraceWith(adapter.OnTrace);
		}
		/// <summary>
		///     Registers <see cref="DataConnection"/> as the service <see cref="IDataContext"/> in the <see cref="IServiceCollection" />.
		///     You use this method when using dependency injection in your application, such as with ASP.NET.
		///     For more information on setting up dependency injection, see http://go.microsoft.com/fwlink/?LinkId=526890.
		/// </summary>
		/// <example>
		///     <code>
		///           public void ConfigureServices(IServiceCollection services)
		///           {
		///               var connectionString = "connection string to database";
		///
		///               services.AddLinqToDb(options => {
		///                   options.UseSqlServer(connectionString);
		///               });
		///           }
		///       </code>
		/// </example>
		/// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
		/// <param name="configure">
		///     <para>
		///         An action to configure the <see cref="LinqToDbConnectionOptionsBuilder" /> for the context.
		///     </para>
		/// </param>
		/// <param name="lifetime"> The lifetime with which to register the Context service in the container.
		/// For one connection per request use <see cref="ServiceLifetime.Scoped"/> (the default).
		/// </param>
		/// <remarks>
		/// 	<para>
		/// 		This will only work when you have 1 database connection across your whole application.
		/// 		If your application needs multiple different connections with different configurations
		/// 		then use <see cref="AddLinqToDbContext{TContext}"/> or <see cref="AddLinqToDbContext{TContext, TContextImplementation}"/>.
		/// 	</para>
		/// 	<para>
		/// 		To Resolve the connection inject <see cref="IDataContext"/> into your services.
		/// 	</para>
		/// </remarks>
		/// <returns>
		///     The same service collection so that multiple calls can be chained.
		/// </returns>
		public static IServiceCollection AddLinqToDb(this IServiceCollection serviceCollection, Action<IServiceProvider, LinqToDbConnectionOptionsBuilder> configure, ServiceLifetime lifetime = ServiceLifetime.Scoped)
		{
			return AddLinqToDbContext<IDataContext, DataConnection>(serviceCollection, configure, lifetime);
		}

		/// <summary>
		///     Registers <typeparamref name="TContext"/> as a service in the <see cref="IServiceCollection" />.
		///     You use this method when using dependency injection in your application, such as with ASP.NET.
		///     For more information on setting up dependency injection, see http://go.microsoft.com/fwlink/?LinkId=526890.
		/// </summary>
		/// <example>
		///     <code>
		///           public void ConfigureServices(IServiceCollection services)
		///           {
		///               var connectionString = "connection string to database";
		///
		///               services.AddLinqToDbContext&lt;MyContext&gt;(options => {
		///                   options.UseSqlServer(connectionString);
		///               });
		///           }
		///       </code>
		/// </example>
		/// <typeparam name="TContext">
		/// 	The type of context to be registered. Must inherit from <see cref="IDataContext"/>
		/// 	and expose a constructor that takes <see cref="LinqToDbConnectionOptions{TContext}" /> (where T is <typeparamref name="TContext"/>)
		/// 	and passes it to the base constructor of <see cref="DataConnection" />.
		/// </typeparam>
		/// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
		/// <param name="configure">
		///     <para>
		///         An action to configure the <see cref="LinqToDbConnectionOptionsBuilder" /> for the context.
		///     </para>
		///     <para>
		///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
		///         <see cref="LinqToDbConnectionOptions{TContext}" /> and passes it to the base constructor of <see cref="DataConnection" />.
		///     </para>
		/// </param>
		/// <param name="lifetime">
		/// 	The lifetime with which to register the Context service in the container.
		/// 	For one connection per request use <see cref="ServiceLifetime.Scoped"/> (the default).
		/// </param>
		/// <remarks>
		/// 	This method should be used when a custom context is required or
		/// 	when multiple contexts with different configurations are required.
		/// </remarks>
		/// <returns>
		///     The same service collection so that multiple calls can be chained.
		/// </returns>
		public static IServiceCollection AddLinqToDbContext<TContext>(
			this IServiceCollection serviceCollection,
			Action<IServiceProvider, LinqToDbConnectionOptionsBuilder> configure,
			ServiceLifetime lifetime = ServiceLifetime.Scoped) where TContext : IDataContext
		{
			return AddLinqToDbContext<TContext, TContext>(serviceCollection, configure, lifetime);
		}

		/// <summary>
		///     Registers <typeparamref name="TContext"/> as a service in the <see cref="IServiceCollection" />.
		///     You use this method when using dependency injection in your application, such as with ASP.NET.
		///     For more information on setting up dependency injection, see http://go.microsoft.com/fwlink/?LinkId=526890.
		/// </summary>
		/// <example>
		///     <code>
		///           public void ConfigureServices(IServiceCollection services)
		///           {
		///               var connectionString = "connection string to database";
		///
		///               services.AddLinqToDbContext&lt;IMyContext, MyContext&gt;(options => {
		///                   options.UseSqlServer(connectionString);
		///               });
		///           }
		///       </code>
		/// </example>
		/// <typeparam name="TContext">
		/// 	The class or interface that will be used to resolve the context from the container.
		/// </typeparam>
		/// <typeparam name="TContextImplementation">
		///		The concrete implementation type used to fulfill requests for <typeparamref name="TContext"/> from the container.
		/// 	Must inherit from <see cref="IDataContext"/> and <typeparamref name="TContext"/>
		/// 	and expose a constructor that takes <see cref="LinqToDbConnectionOptions{TContext}" /> (where T is <typeparamref name="TContextImplementation"/>)
		/// 	and passes it to the base constructor of <see cref="DataConnection" />.
		/// </typeparam>
		/// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
		/// <param name="configure">
		///     <para>
		///         An action to configure the <see cref="LinqToDbConnectionOptionsBuilder" /> for the context.
		///     </para>
		///     <para>
		///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
		///         <see cref="LinqToDbConnectionOptions{TContext}" /> and passes it to the base constructor of <see cref="DataConnection" />.
		///     </para>
		/// </param>
		/// <param name="lifetime">
		/// 	The lifetime with which to register the Context service in the container.
		/// 	For one connection per request use <see cref="ServiceLifetime.Scoped"/> (the default).
		/// </param>
		/// <remarks>
		/// 	This method should be used when a custom context is required or
		/// 	when multiple contexts with different configurations are required.
		/// </remarks>
		/// <returns>
		///     The same service collection so that multiple calls can be chained.
		/// </returns>
		public static IServiceCollection AddLinqToDbContext<TContext, TContextImplementation>(
			this IServiceCollection serviceCollection,
			Action<IServiceProvider, LinqToDbConnectionOptionsBuilder> configure,
			ServiceLifetime lifetime = ServiceLifetime.Scoped) where TContextImplementation : TContext, IDataContext
		{
			CheckContextConstructor<TContextImplementation>();
			serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContext), typeof(TContextImplementation), lifetime));
			serviceCollection.TryAdd(new ServiceDescriptor(typeof(LinqToDbConnectionOptions<TContextImplementation>),
				provider =>
				{
					var builder = new LinqToDbConnectionOptionsBuilder();
					configure(provider, builder);
					return builder.Build<TContextImplementation>();
				},
				lifetime));
			serviceCollection.TryAdd(new ServiceDescriptor(typeof(LinqToDbConnectionOptions),
				provider => provider.GetRequiredService(typeof(LinqToDbConnectionOptions<TContextImplementation>)), lifetime));
			return serviceCollection;
		}

		private static void CheckContextConstructor<TContext>()
		{
			var constructorInfo =
				typeof(TContext).GetConstructor(new[] { typeof(LinqToDbConnectionOptions<TContext>) }) ??
				typeof(TContext).GetConstructor(new[] { typeof(LinqToDbConnectionOptions) });
			if (constructorInfo == null)
			{
				throw new ArgumentException("Missing constructor accepting 'LinqToDbContextOptions' on type "
											+ typeof(TContext).Name);
			}
		}
	}
}
