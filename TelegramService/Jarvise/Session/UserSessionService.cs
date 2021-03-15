using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TdLib.TdApi;

namespace TelegramService.Jarvise.Session
{
	public interface IUserSession
	{
		public User CommandSender { get; }
	}
	public class UserSessionService : IUserSession
	{
		public UserSessionService(User user)
		{
			CommandSender = user;
		}

		public User CommandSender { get; }
	}
}
