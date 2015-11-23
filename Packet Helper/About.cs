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


This Program is made by DeokGyu Yang for Computer Network Project

-- Members --
DeokGyu Yang
JunHee Lee
JeongHyeon Sim
JiSu Oh
GyuBeom Kim

2015. 11. 20
";

            label_visitGibhub.Text = "Click here : Packet Helper GitHub";
        }

        private void label_visitGibhub_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/auseom/PacketHelper/");
        }

        private void label_visitGibhub_MouseHover(object sender, EventArgs e)
        {
            label_visitGibhub.ForeColor = System.Drawing.Color.Cyan;
            label_visitGibhub.Cursor = Cursors.Hand;
        }

        private void label_visitGibhub_MouseLeave(object sender, EventArgs e)
        {
            label_visitGibhub.ForeColor = System.Drawing.Color.Black;
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
