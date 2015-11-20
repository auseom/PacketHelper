using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Packet_Helper
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            textBox_about.Text = @"
This Program is made by DeokGyu Yang for Computer Networking Project


Members

DeokGyu Yang
...

2015. 11. 20
";
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
