using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace QT
{
    public class QQMd5
    {
        #region 旧版的QQ登录方式======================

        public static string EncryptOld(string password, string verifyCode)
        {
            return QQMd5.smethod_0(QQMd5.EncyptMD5_3_16(password) + verifyCode.ToUpper());
        }
        /// <summary>
        /// 根据qq和密码和验证码得到加密的密码
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="password"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public static string Encrypt(string qq, string password, string verifyCode)
        {
            return QQMd5.Encrypt((long)Convert.ToInt32(qq), password, verifyCode);
        }
        private static string EncyptMD5_3_16(string s)
        {
            MD5 mD = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            byte[] buffer = mD.ComputeHash(bytes);
            byte[] buffer2 = mD.ComputeHash(buffer);
            byte[] array = mD.ComputeHash(buffer2);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                stringBuilder.Append(b.ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString().ToUpper();
        }
        private static string smethod_0(string s)
        {
            MD5 mD = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
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
        private static byte[] EncyptMD5Bytes(string s)
        {
            MD5 mD = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            return mD.ComputeHash(bytes);
        }
        private static string smethod_1(byte[] s)
        {
            MD5 mD = MD5.Create();
            byte[] array = mD.ComputeHash(s);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                stringBuilder.Append(b.ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString().ToUpper();
        }
        private static string EncryptQQWebMd5(string s)
        {
            MD5 mD = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            byte[] array = mD.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                stringBuilder.Append("");
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        private static string Encrypt(long qq, string password, string verifyCode)
        {
            ByteBuffer byteBuffer = new ByteBuffer();
            byteBuffer.PushByteArray(QQMd5.EncyptMD5Bytes(password));
            byteBuffer.PushInt(0);
            byteBuffer.PushInt((uint)qq);
            QQMd5.EncryptQQWebMd5(password);
            byte[] s = byteBuffer.ToByteArray();
            string str = QQMd5.smethod_1(s);
            return QQMd5.smethod_0(str + verifyCode.ToUpper());
        }

        #endregion

        #region 新版======================

        #endregion
    }


}

