using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Interaction
{	
	public enum CommandParam : ushort
	{
		ItemId
	}

	public interface ICommand
	{
		string ModuleName { get; }
		string Action { get; }
		string RawCommand { get; }
		IReadOnlyList<CommandParam> Parameters { get; }
		
		//Task<IHandleResult> HandleAsync(IInteractionContext context, CancellationToken cancelToken);
	}

	public class InteractionCommand : ICommand
	{
		public string ModuleName { get; }
		public string Action { get; }
		public IReadOnlyList<CommandParam> Parameters { get; }
		public string RawCommand { get; }
	}
}
