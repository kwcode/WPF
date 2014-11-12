using Newtonsoft.Json;
using System;
namespace QT
{
	public class MessageValue
	{
		[JsonProperty("msg_id")]
		public int MsgId
		{
			get;
			set;
		}
		[JsonProperty("from_uin")]
		public long FromUin
		{
			get;
			set;
		}
		[JsonProperty("to_uin")]
		public long ToUin
		{
			get;
			set;
		}
		[JsonProperty("msg_id2")]
		public int MsgId2
		{
			get;
			set;
		}
		[JsonProperty("msg_type")]
		public int MsgType
		{
			get;
			set;
		}
		[JsonProperty("reply_ip")]
		public long ReplyIp
		{
			get;
			set;
		}
		[JsonProperty("time")]
		public long Time
		{
			get;
			set;
		}
		[JsonProperty("content")]
		public object Content
		{
			get;
			set;
		}
		[JsonProperty("did")]
		public long DiscuID
		{
			get;
			set;
		}
		[JsonProperty("send_uin")]
		public long SendUin
		{
			get;
			set;
		}
	}
}
