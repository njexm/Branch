using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using System.Configuration;

namespace Branch.com.proem.exm.util
{
    public static class PingTask
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(PingTask));

        /// <summary>
        /// 超时时间
        /// </summary>
        public const int TIMEOUT = 1000;

        public const int SLEEPINGTIME = 5000;

        public static bool IsConnected { get; set; }

        public static bool flag = true;

        public static void Ping()
        {
            Ping ping = new Ping();
            PingReply reply = null;
            try
            {
                while (flag)
                {
                    reply = ping.Send(ConfigurationManager.ConnectionStrings["PingIP"].ConnectionString, TIMEOUT);
                    if (reply.Status == IPStatus.Success)
                    {
                        IsConnected = true;
                    }
                    else
                    {
                        IsConnected = false;
                    }
                    Thread.Sleep(SLEEPINGTIME);
                }
            }
            catch (Exception ex)
            {
                IsConnected = false;
                //Thread t = new Thread(new ThreadStart(PingTask.Ping));
                //t.Start();
                log.Error("ping网络发生异常", ex);
                Thread.Sleep(SLEEPINGTIME);
                Ping();
            }
        }

    }
}
