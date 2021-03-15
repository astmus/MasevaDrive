using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace TelegramService.Jarvise.Commands
{
	public interface ICommand
	{
		public string RawValue { get; }
	}

	public interface IBotCommand
	{
		public string Command { get; set; }

		public string Description { get; set; }
	}

	public class MenuCommand : BotCommand, IBotCommand
	{
		
	}

	public enum Types
	{
		S,T,P
	}

	public struct BotCommandType<T> where T : Enum
	{
		int Id;
		public Type Type;
	}
	

	public class MainMenuOptions<T> where T : Enum
	{
		private Dictionary<string, T> manuItems = new Dictionary<string, T>();
		private List<T> Items = new List<T>();
		public MainMenuOptions<T> Add(T type)
		{
			Items.Add(type);
			return this;
		}

		public List<T> Build() => Items;
	}

	public interface IBotMenuOptions<out T>
	{

	}

	public class BotMenuOptions<T> : IBotMenuOptions<T> where T : IBotCommand
	{		
		private Dictionary<string, Type> menuItemTypes = new Dictionary<string, Type>();
		public BotMenuOptions(){}
		public Dictionary<string, Type> Build() => menuItemTypes;
		public Dictionary<string, T> instanceItems = new Dictionary<string, T>(); 
		public Dictionary<string, T> BuildInstances() => instanceItems;
		public BotMenuOptions<T> UseMenuItem<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] B>() where B : T
		{
			menuItemTypes.Add(typeof(B).Name, typeof(B));			
			return this;
		}

		public BotMenuOptions<T> UseMenuItem(T menuItem)
		{
			menuItemTypes.Add(typeof(B).Name, typeof(B));
			return this;
		}
	}
}
