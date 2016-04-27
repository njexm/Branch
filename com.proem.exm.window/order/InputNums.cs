using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.order
{
    /// <summary>
    /// 数量输入界面
    /// </summary>
    public partial class InputNums : Form
    {
        /// <summary>
        /// 零售界面
        /// </summary>
        private CustomerDelivery customerDelivery;

        /// <summary>
        /// 构造函数
        /// </summary>
        public InputNums()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="customerDelivery"></param>
        public InputNums(CustomerDelivery customerDelivery)
        {
            InitializeComponent();
            this.customerDelivery = customerDelivery;
        }

        private void InputNums_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.Escape)
            {
                button2_Click(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 限定输入只能是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string num = textBox1.Text;
            if (string.IsNullOrEmpty(num.Trim()) || Int32.Parse(num) == 0)
            {
                MessageBox.Show("请输入大于0的数字后再操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                customerDelivery.ResaleChangeNums(num);
                this.Close();
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
