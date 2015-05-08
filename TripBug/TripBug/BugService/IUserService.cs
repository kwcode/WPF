using SysCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trip
{
    [ServiceContractAttribute]
    public interface IUserService
    {
        void Save(UserInfo user);
        UserInfo GetUserByID(string id);
        UserInfo[] GetAllUser();
        /// <summary>
        /// 登录 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns>0登录失败 1登录成功 </returns>
        UserInfo Login(string name, string pwd);
    }
}
