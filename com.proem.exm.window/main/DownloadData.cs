using Branch.com.proem.exm.service;
using Branch.com.proem.exm.service.main;
using Branch.com.proem.exm.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.main
{
    public partial class DownloadData : Form
    {
        /// <summary>
        /// 标识数据下载的模式
        /// 1，登陆数据下载
        /// 2，进入零售数据下载
        /// </summary>
        private string flag;

        public DownloadData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="flag"></param>
        public DownloadData(string flag)
        {
            InitializeComponent();
            this.flag = flag;
        }


        public void Step()
        {
            this.progressBar1.PerformStep();
        }

        /// <summary>
        /// 显示下载信息
        /// </summary>
        /// <param name="message"></param>
        public void AppendMessage(string message)
        {
            messagebox.SelectionStart = 0;
            messagebox.AppendText(message);
        }

        private void DownloadData_Load(object sender, EventArgs e)
        {
            messagebox.SelectionStart = 0;
            messagebox.AppendText("");
        }

        private void loadData()
        {
            messagebox.SelectionStart = 0;
            messagebox.AppendText("开始下载数据,请稍等.......\n");
            DownloadDataService download = new DownloadDataService();
            download.DownloadData(this);
            DownloadIdentifyService service = new DownloadIdentifyService();
            if(flag.Equals(Constant.DOWNLOAD_DAILY))
            {
                ///更新亭点收货标识
                service.UpdateHarvestFlag(Constant.HARVEST_NO);
            }
            ///更新最后一次下载时间
            service.UpdateIdentify(DateTime.Now);
            
        }

        private void DownloadData_Shown(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
