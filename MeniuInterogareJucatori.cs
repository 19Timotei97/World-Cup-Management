using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CampionatFotbal
{
    public partial class MeniuInterogareJucatori : Form
    {
        public MeniuInterogareJucatori()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MeniuInterogare mi = new MeniuInterogare();
            mi.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InterogareJucatori1 ij1 = new InterogareJucatori1();
            ij1.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InterogareJucatori2 ij2 = new InterogareJucatori2();
            ij2.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InterogareJucatori ij = new InterogareJucatori();
            ij.Show();

            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InterogareJucatori3 ij3 = new InterogareJucatori3();
            ij3.Show();

            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InterogareJucatori4 ij4 = new InterogareJucatori4();
            ij4.Show();

            this.Hide();
        }
    }
}
