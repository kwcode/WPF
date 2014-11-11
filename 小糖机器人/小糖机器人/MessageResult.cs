using Newtonsoft.Json;
using System;
namespace QT
{
	public class MessageResult
	{
		[JsonProperty("poll_type")]
		public string PollType
		{
			get;
			set;
		}
		[JsonProperty("value")]
		public MessageValue MessageValue
		{
			get;
			set;
		}
	}
}
