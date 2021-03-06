﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// Class of registered user
	/// </summary>
	public class RegisteredUser : User
	{
		/// <summary>
		/// Email of registered user
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int ChatId { get; set; }
		/// <summary>
		/// 
		/// </summary>		
		public RegisteredUser() : base()
		{

		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static class UserExtension
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <param name="registeredInfo"></param>
		/// <returns></returns>
		public static RegisteredUser ToRegisteredUser(this User info, RegisteredUser registeredInfo)
		{
			var serializedCar = JsonConvert.SerializeObject(info);
			var res = JsonConvert.DeserializeObject<RegisteredUser>(serializedCar);
			res.ChatId = registeredInfo.ChatId;
			res.Email = registeredInfo.Email;
			return res;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="interaction"></param>
		/// <returns></returns>
		public static User GetOwner(this Update interaction)
		{
			return interaction.Message.From;
		}
	}
}
