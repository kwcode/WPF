using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trip
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        /// <summary>
        /// 角色ID  暂时不用
        /// </summary>
        public int RoleID { get; set; }
    }
}
