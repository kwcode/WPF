using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QT
{
    public class MessageProc
    {
        public void msg(object msg)
        {
            if (OnMessagesNoticeEvent != null)
                OnMessagesNoticeEvent.BeginInvoke(msg, null, null);

        }
        public void ReceiveMessages(string qqNumber, int recode, string pollType, MessageValue msg, string content)
        {
            if (OnReceiveMessagesEvent != null)
                OnReceiveMessagesEvent.BeginInvoke(qqNumber, recode, pollType, msg, content, null, null);
        }
        public event MessagesNoticeHandler OnMessagesNoticeEvent;
        public event ReceiveMessagesHandler OnReceiveMessagesEvent;
    }

    /// <summary>
    /// 消息
    /// </summary>
    /// <param name="msg"></param>
    public delegate void MessagesNoticeHandler(object msg);
    /// <summary>
    /// 接受消息
    /// </summary>
    /// <param name="qqNumber"></param>
    /// <param name="recode"></param>
    /// <param name="pollType"></param>
    /// <param name="msg"></param>
    /// <param name="content"></param>
    public delegate void ReceiveMessagesHandler(string qqNumber, int recode, string pollType, MessageValue msg, string content);

}
