using System;
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
The Packet Helper is capturing the packet which has user registered sensitive data.
If the outgoing packet has the data with plain text in payload of the packet, The Packet Helper will alert you of it with simple MessageBox.
So you could know what data will be outgoing and you probably checking your system for prevention of leakage of the data.


This Program is made by DeokGyu Yang for Computer Networking Project

-- Members --
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
