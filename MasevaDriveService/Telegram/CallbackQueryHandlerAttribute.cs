using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	[AttributeUsage(AttributeTargets.Class,AllowMultiple =true,Inherited = false)]
	public class CallbackQueryHandlerAttribute : Attribute
	{
		public Type TypeOfHandler { get; private set; }

		public RequestKind Action { get; private set; }

		public CallbackQueryHandlerAttribute(RequestKind action, Type name)
		{
			Action = action;
			TypeOfHandler = name;
		}

		public override string ToString() => TypeOfHandler.FullName;

		public static IEnumerable<CallbackQueryHandlerAttribute> GetAllDefined(Type typeOfBaseClass)
		{			
			var attributeType = typeof(CallbackQueryHandlerAttribute);
			var assembly = Assembly.GetExecutingAssembly();
			foreach (Type type in assembly.GetTypes())
			{
				if (IsDefined(type, attributeType) && type.IsSubclassOf(typeOfBaseClass))
					yield return (type.GetCustomAttribute(attributeType) as CallbackQueryHandlerAttribute);
			}
		}
	}
}
