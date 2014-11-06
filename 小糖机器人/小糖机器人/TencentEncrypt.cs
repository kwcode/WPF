using System;
using System.Security.Cryptography;
using System.Text;
namespace QT
{
	public class TencentEncrypt
	{
		public static string QQPasswordEncrypt(string uin, string password, string verifyCode)
		{
			ByteBuffer byteBuffer = new ByteBuffer();
			byteBuffer.Put(TencentEncrypt.MD5_GetBytes(password));
			byteBuffer.PutInt(0);
			byteBuffer.PutInt(uint.Parse(uin));
			byte[] md5Bytes = byteBuffer.ToByteArray();
			string str = TencentEncrypt.MD5_Encrypt(md5Bytes);
			return TencentEncrypt.MD5_Encrypt(str + verifyCode.ToUpper());
		}
		public static string MD5_Encrypt(string md5_str)
		{
			MD5 mD = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(md5_str);
			byte[] array = mD.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				stringBuilder.Append(b.ToString("x").PadLeft(2, '0'));
			}
			return stringBuilder.ToString().ToUpper();
		}
		private static string MD5_Encrypt(byte[] md5Bytes)
		{
			MD5 mD = MD5.Create();
			byte[] array = mD.ComputeHash(md5Bytes);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				stringBuilder.Append(b.ToString("x").PadLeft(2, '0'));
			}
			return stringBuilder.ToString().ToUpper();
		}
		private static byte[] MD5_GetBytes(string md5Str)
		{
			MD5 mD = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(md5Str);
			return mD.ComputeHash(bytes);
		}
		private static string Encrypt_1(string md5Str)
		{
			MD5 mD = MD5.Create();
			byte[] array = Encoding.ASCII.GetBytes(md5Str);
			array = mD.ComputeHash(array);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				stringBuilder.Append("\\x");
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}
	}
}
