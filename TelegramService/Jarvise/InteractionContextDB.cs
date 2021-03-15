using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageProviders.SQLite;
using Telegram.Bot;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Interaction;
using Telegram.Bot.Sessions;
using Telegram.Bot.Types;

namespace TelegramService.Storage
{
	public class InteractionContextDB : InteractionContext, IDisposable
	{
		private Update update;
		private bool disposedValue;		

		public SQLiteStorage SQLite { get; set; }
		public Update Update { get => update; set => updateValues(value); }		
		public string RawCommand { get; private set; }
		public ICommand Command { get; private set; }
		public Session Session { get; set; }
		public User User { get; protected set; }
		public Message Message { get; protected set; }
		public ITelegramBot BotConnection { get; set; }
		public static implicit operator InteractionContextDB(Update u) => new InteractionContextDB() { Update = u };
		public InteractionContextDB()
		{

		}
		private void updateValues(Update value)
		{
			update = value;
			switch (value.Type)
			{
				case Telegram.Bot.Types.Enums.UpdateType.ChannelPost:
					User = value.ChannelPost.From;
					Message = value.ChannelPost;
					break;
				case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
					break;
				default:
					User = value.Message.From;
					Message = value.Message;
					break;
			}
			RawCommand = Message?.Text ?? update.ChannelPost?.Text ?? update.EditedChannelPost?.Text ?? update.EditedMessage?.Text ?? update.InlineQuery?.Query ?? "";		
		}                   		

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Session = null;
					User = null;
					Message = null;
					BotConnection = null;
					SQLite = null;
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
