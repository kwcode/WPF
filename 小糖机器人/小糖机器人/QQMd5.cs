using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;


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
        public static string getNewP(string password, long uin, string vcode)
        {
            string hexString = byte2HexString(long2bytes(uin)).ToLower();
            string fun = string.Format(@"getPassword('{0}','{1}','{2}')", password, hexString, vcode);
            return ExecuteScript(fun);
        }
        public static String byte2HexString(byte[] b)
        {

            string ss = "";
            char[] hex = new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8',
                '9', 'A', 'B', 'C', 'D', 'E', 'F'};
            if (b == null)
                return "null";

            int offset = 0;
            int len = b.Length;

            // 检查索引范围
            int end = offset + len;
            if (end > b.Length)
                end = b.Length;


            for (int i = offset; i < end; i++)
            {
                ss += "" + hex[(b[i] & 0xF0) >> 4] + "" + hex[b[i] & 0xF];
            }
            return ss;
        }
        public static byte[] long2bytes(long i)
        {
            byte[] b = new byte[8];
            for (int m = 0; m < 8; m++, i >>= 8)
            {
                b[7 - m] = (byte)(i & 0x000000FF);

            }
            return b;
        }
        /// <summary>
        /// 执行JS
        /// </summary>
        /// <param name="sExpression">参数体</param>
        /// <param name="sCode">JavaScript代码的字符串</param>
        /// <returns></returns>
        public static string ExecuteScript(string sExpression)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "mq_comm.js";
            string sCode = File.ReadAllText(path);
            MSScriptControl.ScriptControl scriptControl = new MSScriptControl.ScriptControl();
            scriptControl.UseSafeSubset = false;
            scriptControl.Language = "JavaScript";
            scriptControl.AddCode(sCode);
            try
            {
                string str = scriptControl.Eval(sExpression).ToString();
                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static string getNewHash(string uin, string ptwebqq)
        {
            string fun = string.Format(@"hash('{0}','{1}')", uin, ptwebqq);
            return ExecuteScript(fun);
        }
        #endregion
    }


}

