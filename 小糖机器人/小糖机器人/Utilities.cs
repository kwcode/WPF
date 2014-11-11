using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
namespace QT
{
    public class Utilities
    {
        public static string GB2Unicode(string source)
        {
            string text = "";
            string str = "";
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(source);
            for (int i = 0; i < bytes.Length; i++)
            {
                if ((bytes[i] >= 48 && bytes[i] <= 57) || (bytes[i] >= 65 && bytes[i] <= 90) || (bytes[i] >= 97 && bytes[i] <= 122))
                {
                    char c = (char)bytes[i];
                    str = c.ToString();
                }
                else
                {
                    str = "%" + bytes[i].ToString("X");
                }
                text += str;
            }
            return text;
        }
        public static string UTF8(string Source, bool toUpper = true)
        {
            string result;
            if (toUpper)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < Source.Length; i++)
                {
                    string text = Source[i].ToString();
                    string text2 = HttpUtility.UrlEncode(text, Encoding.Default);
                    if (text == text2)
                    {
                        stringBuilder.Append(text);
                    }
                    else
                    {
                        stringBuilder.Append(text2.ToUpper());
                    }
                }
                result = stringBuilder.ToString();
            }
            else
            {
                result = HttpUtility.UrlEncode(Source, Encoding.Default);
            }
            return result;
        }
        public static string EncodeStr(string Source)
        {
            return HttpUtility.UrlEncodeUnicode(Source);
        }
        public static string DecodeStr(string Source)
        {
            string text = Source;
            if (Source.Contains("&#"))
            {
                text = "";
                if (Source.EndsWith(";"))
                {
                    Source = Source.Substring(0, Source.Length - 1);
                }
                string[] array = Regex.Replace(Source, "&#", "").Split(new char[]
				{
					';'
				});
                for (int i = 0; i < array.Length; i++)
                {
                    string text2 = int.Parse(array[i]).ToString("x4");
                    text = text + "%u" + text2.Substring(text2.Length - 4);
                }
            }
            return HttpUtility.UrlDecode(text);
        }
        public static string MD5(string Source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(Source, "MD5");
        }
        public static Image ImageFromByte(object ByteOrPath)
        {
            Image result;
            try
            {
                byte[] buffer = null;
                if (ByteOrPath.GetType().Name == "String")
                {
                    string text = File.ReadAllText(ByteOrPath.ToString());
                    text = Regex.Replace(text, "\\s+|\r\n", "");
                    byte[] array = new byte[text.Length / 2];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = Convert.ToByte("0x" + text.Substring(i * 2, 2), 16);
                    }
                    buffer = array;
                }
                else
                {
                    buffer = (byte[])ByteOrPath;
                }
                MemoryStream memoryStream = new MemoryStream(buffer);
                memoryStream.Position = 0L;
                Image image = Image.FromStream(memoryStream);
                memoryStream.Close();
                result = image;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public static Image ImageFromBase64(string PathOrStr)
        {
            Image result;
            try
            {
                string s = "";
                if (PathOrStr.Contains("\\"))
                {
                    s = File.ReadAllText(PathOrStr);
                }
                else
                {
                    s = PathOrStr;
                }
                byte[] buffer = Convert.FromBase64String(s);
                MemoryStream memoryStream = new MemoryStream(buffer);
                Bitmap bitmap = new Bitmap(memoryStream);
                memoryStream.Close();
                result = bitmap;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public static string GetCookieValue(string cookies, string key)
        {
            string result;
            if (string.IsNullOrEmpty(cookies) || string.IsNullOrEmpty(key))
            {
                result = "";
            }
            else
            {
                string[] array = cookies.Split(new char[]
				{
					';'
				});
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text = array2[i];
                    string[] array3 = text.Split(new char[]
					{
						'='
					});
                    if (array3.Length == 2 && array3[0].Contains(key))
                    {
                        result = array3[1];
                        return result;
                    }
                }
                result = "";
            }
            return result;
        }
        public static string GetSmallCookie(string strcookie)
        {
            string result;
            if (string.IsNullOrWhiteSpace(strcookie))
            {
                result = string.Empty;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                string[] array = strcookie.ToString().Split(new string[]
				{
					",", 
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text = array2[i];
                    string text2 = text.ToLower().Trim().Replace("\r\n", string.Empty).Replace("\n", string.Empty);
                    if (!string.IsNullOrWhiteSpace(text2))
                    {
                        if (text2.Contains("="))
                        {
                            if (!text2.Contains("path="))
                            {
                                if (!text2.Contains("expires="))
                                {
                                    if (!text2.Contains("domain="))
                                    {
                                        if (!stringBuilder.ToString().Contains(text))
                                        {
                                            stringBuilder.Append(string.Format("{0};", text));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                result = stringBuilder.ToString();
            }
            return result;
        }
        public static string LiteCookies(string Cookies)
        {
            string result;
            try
            {
                string text = "";
                Cookies = Cookies.Replace("HttpOnly", "").Replace(";", "; ");
                Regex regex = new Regex("(?<=,|^)(?<cookie>[^ ]+=[\\s|\"]?(?![\"]?deleted[\"]?)[^;]+)[\"]?;");
                Match match = regex.Match(Cookies);
                while (match.Success)
                {
                    string value = match.Groups["cookie"].Value;
                    if (!text.Contains(value) && value != "")
                    {
                        text = text + value + ";";
                    }
                    match = match.NextMatch();
                }
                result = text;
            }
            catch
            {
                result = "";
            }
            return result;
        }
        public static string MergerCookies(string OldCookie, string NewCookie)
        {
            string result;
            if (!string.IsNullOrEmpty(OldCookie) && !string.IsNullOrEmpty(NewCookie))
            {
                List<string> list = new List<string>(OldCookie.Split(new char[]
				{
					';'
				}));
                List<string> list2 = new List<string>(NewCookie.Split(new char[]
				{
					';'
				}));
                foreach (string current in list2)
                {
                    foreach (string current2 in list)
                    {
                        if (current2 == current || current2.Split(new char[]
						{
							'='
						})[0] == current.Split(new char[]
						{
							'='
						})[0])
                        {
                            list.Remove(current2);
                            break;
                        }
                    }
                }
                List<string> list3 = new List<string>(list);
                list3.AddRange(list2);
                result = string.Join(";", list3.ToArray());
            }
            else
            {
                if (!string.IsNullOrEmpty(OldCookie))
                {
                    result = OldCookie;
                }
                else
                {
                    if (!string.IsNullOrEmpty(NewCookie))
                    {
                        result = NewCookie;
                    }
                    else
                    {
                        result = "";
                    }
                }
            }
            return result;
        }
        public static string StripHTML(string stringToStrip)
        {
            stringToStrip = Regex.Replace(stringToStrip, "</p(?:\\s*)>(?:\\s*)<p(?:\\s*)>", "\n\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Utilities.StripHtmlXmlTags(stringToStrip);
            return stringToStrip;
        }
        private static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        public static string GetMidStr(string Source, string StartStr, string EndStr)
        {
            string result;
            try
            {
                int num = Source.IndexOf(StartStr, 0) + StartStr.Length;
                int num2 = Source.IndexOf(EndStr, num);
                result = Source.Substring(num, num2 - num);
            }
            catch
            {
                result = "";
            }
            return result;
        }
        public static string GetMidStr(string source, string rule, int index)
        {
            Match match = new Regex(rule).Match(source);
            string result;
            if (match.Success && match.Groups.Count >= index)
            {
                result = match.Groups[index].Value;
            }
            else
            {
                result = "";
            }
            return result;
        }
        public static string ReverseStr(string text)
        {
            return new string(text.ToCharArray().Reverse<char>().ToArray<char>());
        }
        private static string[] SortArray(string[] inArray)
        {
            return (
                from s in inArray
                orderby Convert.ToInt16(s.Substring(s.Length - 2), 16)
                select s).ToArray<string>();
        }
        public static string GetTime()
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            return ((DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L).ToString();
        }
        public static string GetRnd(int len = 10)
        {
            Random random = new Random();
            int num = random.Next();
            string arg_23_0 = num.ToString();
            num = random.Next();
            string text = arg_23_0 + num.ToString();
            string result;
            if (len > 0 && len <= text.Length)
            {
                result = text.Substring(0, len);
            }
            else
            {
                result = text.Substring(0, 10);
            }
            return result;
        }
        public static string GetNetTime(string NetTime)
        {
            NetTime = NetTime.Replace(" GMT", "");
            DateTime dateTime = Convert.ToDateTime(NetTime);
            dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
            return dateTime.ToString();
        }
        public static string OperationTime(string BaseTime, string AddTime, string AddType)
        {
            DateTime dateTime = Convert.ToDateTime(BaseTime);
            DateTime dateTime2 = DateTime.Now;
            switch (AddType)
            {
                case "年":
                    {
                        dateTime2 = dateTime.AddYears(int.Parse(AddTime));
                        break;
                    }
                case "月":
                    {
                        dateTime2 = dateTime.AddMonths(int.Parse(AddTime));
                        break;
                    }
                case "日":
                    {
                        dateTime2 = dateTime.AddDays(double.Parse(AddTime));
                        break;
                    }
                case "时":
                    {
                        dateTime2 = dateTime.AddHours(double.Parse(AddTime));
                        break;
                    }
                case "分":
                    {
                        dateTime2 = dateTime.AddMinutes(double.Parse(AddTime));
                        break;
                    }
                case "秒":
                    {
                        dateTime2 = dateTime.AddSeconds(double.Parse(AddTime));
                        break;
                    }
                case "毫":
                    {
                        dateTime2 = dateTime.AddMilliseconds(double.Parse(AddTime));
                        break;
                    }
            }
            return dateTime2.ToString();
        }
        public static string strToNum(string Month)
        {
            string result = "";
            string[] array = new string[]
			{
				"Jan", 
				"Feb", 
				"Mar", 
				"Apr", 
				"May", 
				"Jun", 
				"Jul", 
				"Aug", 
				"Sep", 
				"Oct", 
				"Nov", 
				"Dec"
			};
            string[] array2 = new string[]
			{
				"01", 
				"02", 
				"03", 
				"04", 
				"05", 
				"06", 
				"07", 
				"08", 
				"09", 
				"10", 
				"11", 
				"12"
			};
            for (int i = 0; i < array.Length; i++)
            {
                if (Month == array[i])
                {
                    result = array2[i];
                    break;
                }
            }
            return result;
        }
        public static byte[] FileToBytes(string FilePath)
        {
            return File.ReadAllBytes(FilePath);
        }
        public static byte[] MergerArrays(byte[] Array1, byte[] Array2)
        {
            byte[] array = new byte[Array1.Length + Array2.Length];
            Array1.CopyTo(array, 0);
            Array2.CopyTo(array, Array1.Length);
            return array;
        }
    }
}
