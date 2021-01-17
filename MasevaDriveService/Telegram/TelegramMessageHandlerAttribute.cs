using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	[AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited = false)]
	public class TelegramMessageHandlerAttribute : Attribute
	{
		public Type TypeOfHandler { get; private set; }

		public string Id { get; private set; }

		public TelegramMessageHandlerAttribute(string id, Type name)
		{
			Id = id;
			TypeOfHandler = name;
		}

		public override string ToString() => TypeOfHandler.FullName;

		public static IEnumerable<TelegramMessageHandlerAttribute> GetAllDefined(Type typeOfBaseClass)
		{			
			var attributeType = typeof(TelegramMessageHandlerAttribute);
			var assembly = Assembly.GetExecutingAssembly();
			foreach (Type type in assembly.GetTypes())
			{
				if (IsDefined(type, attributeType) && type.IsSubclassOf(typeOfBaseClass))
					yield return (type.GetCustomAttribute(attributeType) as TelegramMessageHandlerAttribute);
			}
		}
	}
}
