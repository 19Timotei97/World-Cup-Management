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
    public partial class MeniuInterogare : Form
    {
        public MeniuInterogare()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button5_Click(object sender, EventArgs e)
        {
            if(Form1.UN == "Timotei")
            {
                MeniuPrincipal mp = new MeniuPrincipal();
                mp.Show();

                this.Hide();
            }

            else
            {
                MeniuUser mu = new MeniuUser();
                mu.Show();

                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InterogareAntrenori ia = new InterogareAntrenori();
            ia.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInterogareJucatori mij = new MeniuInterogareJucatori();
            mij.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InterogareGoluri ig = new InterogareGoluri();
            ig.Show();

            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InterogareMeciuri im = new InterogareMeciuri();
            im.Show();

            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MeniuInterogareStadioane mins = new MeniuInterogareStadioane();
            mins.Show();

            this.Hide();
        }
    }
}
