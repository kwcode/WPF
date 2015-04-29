using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace QT
{
    /// <summary>
    /// QQMain.xaml 的交互逻辑
    /// </summary>
    public partial class QQMain : Window
    {
        QQHelper QQ = new QQHelper();
        private string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";

        public QQMain()
        {
            InitializeComponent();
            this.Loaded += QQMain_Loaded;
            QQ.OnMessagesNoticeEvent += new MessagesNoticeHandler(QQ_OnMessagesNoticeEvent);
            QQ.OnReceiveMessagesEvent += QQ_OnReceiveMessagesEvent;
        }

        void QQMain_Loaded(object sender, RoutedEventArgs e)
        {
            myMsg.Msg("Cookie:" + Global.Cookie);
            myMsg.Msg("ClientID:" + Global.ClientID);
            myMsg.Msg("PtWebQQ:" + Global.PtWebQQ);
            myMsg.Msg("VfWebQQ:" + Global.VfWebQQ);
            myMsg.Msg("PsessionID:" + Global.PsessionID);
            myMsg.Msg("VerifySession:" + Global.VerifySession);
            QQ.StartPoll();//开始监听

            myMsg.Msg(Global.QQNickName);
        }

        private void QQ_OnMessagesNoticeEvent(object msg)
        {
            Global.SysContext.Send(o =>
            {
                myMsg.Msg(msg.ToString());
            }, null);
        }

        void QQ_OnReceiveMessagesEvent(string qqNumber, int recode, string pollType, MessageValue msg, string content)
        {
            QQ.msg("收到" + qqNumber + "的消息:" + content);
            try
            {
                this.Write(string.Concat(new object[]
				{
					"===========================\r\n", 
					pollType, 
					"\r\n", 
					msg.SendUin, 
					"\r\n", 
					content, 
					"\r\n"
				}));
                string pollType2 = pollType;
                bool b = true;
                switch (pollType2)
                {
                    case "kick_message":
                        {
                            MessageBox.Show("您的账号已经掉线，错误代码（" + recode + ")");
                            break;
                        }
                    case "message":
                        {
                            string uid = msg.FromUin.ToString();
                            string ask = "你的信息已经收到！";//QQ.Ask(content)
                            b = QQ.SendMsgToFriend(uid, ask);
                            break;
                        }
                    case "sess_message":
                        {
                            string uid = msg.FromUin.ToString();
                            b = QQ.SendMsgToSess(uid, QQ.Ask(content));
                            break;
                        }
                    case "group_message":
                        {
                            string uid = msg.FromUin.ToString();
                            b = QQ.SendMsgToGroup(uid, QQ.Ask(content));
                            break;
                        }
                    case "discu_message":
                        {
                            string uid = msg.DiscuID.ToString();
                            b = QQ.SendMsgToDiscu(uid, QQ.Ask(content));
                            break;
                        }
                }
                if (!b)
                {
                    QQ.msg("机器人发送失败了！");
                }
            }
            catch (Exception ex)
            {
                Global.SysContext.Send(delegate(object o)
                {
                    FileStream fileStream = new FileStream(this.path, FileMode.Append, FileAccess.Write);
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(string.Concat(new string[]
					{
						"===========================\r\n", 
						pollType, 
						"\r\n", 
						ex.Message, 
						"\r\n", 
						ex.StackTrace, 
						"时间:", 
						DateTime.Now.ToString(), 
						"\r\n===========================\r\n"
					}));
                    streamWriter.Close();
                    fileStream.Close();
                }
                 , null);
            }
        }
        private void Write(string msg)
        {
            Global.SysContext.Send(delegate(object o)
            {
                FileStream fileStream = new FileStream(this.path, FileMode.Append, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(msg + "时间:" + DateTime.Now.ToString());
                streamWriter.Close();
                fileStream.Close();
            }
            , null);
        }


        private void btn_Seting_Click(object sender, RoutedEventArgs e)
        {
            QQ.GetFriendResults();
        }
    }
}
