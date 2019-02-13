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
    public partial class MeniuInterogareStadioane : Form
    {
        public MeniuInterogareStadioane()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MeniuInterogare mi = new MeniuInterogare();
            mi.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InterogareStadioane ins = new InterogareStadioane();
            ins.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InterogareStadioane1 ins1 = new InterogareStadioane1();
            ins1.Show();

            this.Hide();
        }
    }
}
