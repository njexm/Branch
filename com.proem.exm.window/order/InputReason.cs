using Branch.com.proem.exm.util;
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
    public partial class InputReason : Form
    {
        private string workMode;

        private CustomerDelivery customerDelivery;

        public InputReason()
        {
            InitializeComponent();
        }

        public InputReason(string workMode, CustomerDelivery obj)
        {
            InitializeComponent();
            this.workMode = workMode;
            this.customerDelivery = obj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InputReason_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Y){
                button1_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.N)
            {
                button2_Click(this, EventArgs.Empty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                return;
            }
            customerDelivery.writeReason(textBox1.Text);
            this.Close();
        }

        private void InputReason_Load(object sender, EventArgs e)
        {
            if (workMode.Equals(Constant.PICK_UP_GOODS))
            {
                label1.Text = "请输入拒收原因?";
            }
            else if (workMode.Equals(Constant.REFUND))
            {
                label1.Text = "请输入退货原因";
            }
        }
    }
}
