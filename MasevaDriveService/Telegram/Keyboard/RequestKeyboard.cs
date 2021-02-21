using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MasevaDriveService
{
	public class RequestKeyboard : List<IEnumerable<RequestButton>>, IReplyMarkup
	{
		public RequestKeyboard()
		{					
		}

		public RequestKeyboard(RequestButton requestButton)
		{
			Add(new RequestButton[] { requestButton });
		}
		public RequestKeyboard(IEnumerable<RequestButton> requestButtonsRow)
		{
			Add(requestButtonsRow);

		}

		public RequestKeyboard(IEnumerable<IEnumerable<RequestButton>> requestButtons)
		{			
			AddRange(requestButtons);
		}

		public RequestKeyboard(params RequestButton[] requestButtonsRow) 
		{
			List<RequestButton> row = new List<RequestButton>();
			foreach (var button in requestButtonsRow)
			{
				if (button != null)
					row.Add(button);
				else
				{
					Add(row.ToArray());
					row.Clear();
				}
			}
			Add(row.ToArray());
		}

		public RequestKeyboard(IEnumerable<RequestButton[]> requestButtonsRow)
		{
			AddRange(requestButtonsRow);
		}

		public IEnumerable<IEnumerable<RequestButton>> Keyboard 
		{
			get
			{
				return this;
			}
		}

		public static implicit operator InlineKeyboardMarkup(RequestKeyboard board)
		{
			IEnumerable<IEnumerable<InlineKeyboardButton>> items = board.Select(line => line.Select(but => (InlineKeyboardButton)but).ToArray()).ToArray();
			var result = new InlineKeyboardMarkup(items);
			return result;
		}
	}

}
