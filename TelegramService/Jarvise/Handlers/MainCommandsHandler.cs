using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using StorageProviders.SQLite;
using Telegram.Bot.Exceptions;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramService.Storage;

namespace Telegram.Bot.Storage.InteractionHandlers
{
/*	public abstract class BaseInteractionHandler<T> : IInteractionHandler<T> where T : IInteractionContext
	{
		public T Context { get; set; }
		public IEnumerable<string> Options { get; set; }
		public IEnumerable<string> Controls { get; set; }
		public IEnumerable<InputMediaBase> DisplayContent { get; set; }
		public string TextLabel { get; set; }
		public bool ResetKeyboard { get; set; } = false;
		public bool? ControlsIsReply => false;

		public InteractionHandleDelegate InteractionHandle { get; set; } = (h) =>  Task.CompletedTask;
		public IInteractionResult UseThisState { get; set; }
		public bool HideKeyboard { get; set; } = false;

		public abstract void Configure(T Context);
		public virtual async Task HandleExceptionsError(Exception error, CancellationToken cancelToken)
		{
			TextLabel = await Task.FromResult<string>(error.Message);
		}

		public abstract Task OnDefaultHandler(CancellationToken cancel);

		public virtual async Task RunHandler(CancellationToken cancelToken)
		{
			await InteractionHandle(cancelToken);
		}
	}
	/// <summary>
	/// 
	/// </summary>
	[WithoutHandler]
	[InteractionsSupported(new string[] { "/start", "/menu", "/reset", "Отмена", "Назад" })]
	public class MainCommandsHandler : BaseInteractionHandler<InteractionContext>
	{
		public MainCommandsHandler()
		{
		}

		public override void Configure(InteractionContext Context)
		{
			DisplayContent = DisplayContent;
			switch (Context.Message.Text)
			{
				case "/start":
					TextLabel = "Hello my name is Jarvise.You are registered";
					ResetKeyboard = true;
					break;
				case "/menu":
					TextLabel = "Основное меню";
					Options = new string[] { "Папки и файлы", "Просмотр последних", "Альбомы", "Ярлыки", "Настройки" };
					Controls = new string[] { "Отмена" };
					break;
				case "/reset":
					TextLabel = "kb";
					ResetKeyboard = true;
					break;
				case "Назад":
				case "Отмена":
					//UseThisState = Context.Session.PreviousState;
					ResetKeyboard = true;
					break;
				default:
					break;

			}
		}

		public override async Task OnDefaultHandler(CancellationToken cancel)
		{
			await Task.CompletedTask;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	[InteractionsSupported(new string[] { "Папки и файлы", "Просмотр последних", "Альбомы", "Ярлыки", "Настройки" })]
	public class MenuCommandsHandler : BaseInteractionHandler<InteractionContext>
	{
		public MenuCommandsHandler() { }

		public override void Configure(InteractionContext context)
		{
			Context = context;
		}

		[InterationRunMethod("Папки и файлы")]
		public async Task OnFolderAndFilesHandler(CancellationToken cancel)
		{

				//await (from folder in Context.SQLite.Folders
				//				 where folder.FolderDisplayName.Contains("01")
				//				 select folder.FolderDisplayName
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.Items
				//				   where folder.ItemFileName.Contains("01") || folder.ItemFileName.EndsWith("jpg")
				//				   select folder.ItemFileName
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.Folders
				//					  where folder.FolderParentFolderId == 1 || folder.FolderParentFolderId == 8
				//					  select folder
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.Albums
				//					  select folder
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.VideoFaceOccurrences
				//					  orderby folder.VideoFaceOccurrenceBeginFrame
				//					  select folder
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.Caches
				//					  orderby folder.CacheDateAccessed
				//					  select folder
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.Items
				//					  orderby folder.ItemFileName
				//					  where folder.ItemFileName.StartsWith("P")
				//					  select folder
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.Events
				//					  orderby folder.EventSize descending
				//					  where folder.EventEndDate < DateTime.Now
				//					  select folder
				//				).ToListAsync(cancel);
				//await (from folder in Context.SQLite.ItemTags
				//					   orderby folder.ItemTagsConfidence descending

				//					   select folder
				//				).ToListAsync(cancel);
				// await (from folder in Context.SQLite.Items
				//					  where folder.ItemParentFolderId < 200
				//					  orderby folder.ItemQualityScore
				//					  select folder
				//				).ToListAsync(cancel);
		
			Options = await (from folder in Context.SQLite.Folders
								 where folder.FolderParentFolderId == 1
								 orderby folder.FolderDateCreated descending
								 select $"[{folder.FolderDisplayName}]"
								).ToListAsync(cancel);
			Controls = new string[] { "Назад" };
			TextLabel = "Root";
			Context.Session.State = SessionState.Browse;
		}

		[InterationRunMethod("Просмотр последних")]
		public async Task OnRecentHandler(CancellationToken cancel)
		{
			await Task.Run(() => { Console.WriteLine("Папки"); });
		}

		[InterationRunMethod("Альбомы")]
		public async Task OnAlbumsHandler(CancellationToken cancel)
		{
			await Task.Run(() => { Console.WriteLine("Папки"); });
		}

		[InterationRunMethod("Ярлыки")]
		public async Task OnHashtagsHandler(CancellationToken cancel)
		{
			await Task.Run(() => { Console.WriteLine("Ярлыки"); });
		}

		[InterationRunMethod("Настройки")]
		public async Task OnSettingsHandler(CancellationToken cancel)
		{
			await Task.Run(() => { Console.WriteLine("Настройки"); });
		}

		public override async Task OnDefaultHandler(CancellationToken cancel)
		{
			await Task.CompletedTask;
		}
	}
*/
}
