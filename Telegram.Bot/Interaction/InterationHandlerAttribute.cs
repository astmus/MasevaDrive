using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Interaction
{	
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class InteractionsSupportedAttribute : Attribute
	{
		List<string> _supportedCommands;
		public InteractionsSupportedAttribute(params string[] commands)
		{
			_supportedCommands = new List<string>(commands);
		}

		public bool IsCommandSupport(string command) => _supportedCommands.Contains(command);

		//public static IEnumerable<InteractionsSupportedAttribute> GetAllDefined(Type typeOfBaseClass)
		//{
		//	var attributeType = typeof(InteractionsSupportedAttribute);
		//	var assembly = Assembly.GetExecutingAssembly();
		//	foreach (Type type in assembly.GetTypes()) 
		//	{
		//		if (IsDefined(type, attributeType) && type.IsSubclassOf(typeOfBaseClass))
		//			yield return (type.GetCustomAttribute(attributeType) as InteractionsSupportedAttribute);
		//	}
		//}
	}

	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public class InterationRunMethodAttribute : Attribute
	{
		string _commandName;
		public InterationRunMethodAttribute(string commandName)
		{
			_commandName = commandName;
		}
		public string CommandName => _commandName;
		/*
				public static IEnumerable<InterationRunMethodAttribute> GetAllDefined(Type typeOfBaseClass)
				{
					var attributeType = typeof(InterationRunMethodAttribute);
					var assembly = Assembly.GetExecutingAssembly();
					foreach (Type type in assembly.GetTypes())
					{
						if (IsDefined(type, attributeType) && type.IsSubclassOf(typeOfBaseClass))
							yield return (type.GetCustomAttribute(attributeType) as InterationRunMethodAttribute);
					}
				}
		*/
	}
}
