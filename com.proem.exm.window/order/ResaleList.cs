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
    public partial class ResaleList : Form
    {
        private MemberChoose memberChoose;

        private CustomerDelivery customerDelivery;

        private string memberId;

        public ResaleList()
        {
            InitializeComponent();
        }

        public ResaleList(MemberChoose memberChoose, string memberId, CustomerDelivery customerDelivery)
        {
            InitializeComponent();
            this.memberChoose = memberChoose;
            this.customerDelivery = customerDelivery;
            this.memberId = memberId;
        }

        private void ResaleList_Load(object sender, EventArgs e)
        {

        }
    }
}
