using BaseApiCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Trip
{
    public class TripWebService : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["Type"] == "0")
            {
                long len = context.Request.InputStream.Length;
                byte[] buff = new byte[len];
                context.Request.InputStream.Read(buff, 0, (int)len);
                InvokeParam obj = (InvokeParam)BaseApiCommon.SerializationCommon.Deserilize(buff);
                if (obj == null) return;
                string name = SysCore.ServiceConfig.GetServiceComponent(obj.Interface);
                if (name == null) throw new Exception("配置Config错误");
                string bapath = System.AppDomain.CurrentDomain.BaseDirectory;
                string dll = bapath + @"Bin\" + name + ".DLL";
                object objs = BaseApiCommon.AssemblyCommon.InvokeMember(dll, obj.Interface, obj.MethodName, obj.Parameters);
                buff = BaseApiCommon.SerializationCommon.Serilize(objs);
                context.Response.OutputStream.Write(buff, 0, buff.Length);
            }
        }
    }
}
