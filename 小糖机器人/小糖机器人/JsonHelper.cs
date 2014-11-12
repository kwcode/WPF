using Newtonsoft.Json;
using System;
namespace QT
{
	public static class JsonHelper
	{
		public static T DeserializeToObj<T>(string jsonString)
		{
			T result;
			try
			{
				result = JsonConvert.DeserializeObject<T>(jsonString);
			}
			catch
			{
				result = default(T);
			}
			return result;
		}
		public static string SerializeObject(object obj)
		{
			string result;
			try
			{
				result = JsonConvert.SerializeObject(obj);
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
