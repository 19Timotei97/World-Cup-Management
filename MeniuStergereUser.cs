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
    public partial class MeniuStergereUser : Form
    {
        public MeniuStergereUser()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button4_Click(object sender, EventArgs e)
        {
            MeniuUser mu = new MeniuUser();
            mu.Show();

            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StergereAntrenori sa = new StergereAntrenori();
            sa.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StergereJucatori sj = new StergereJucatori();
            sj.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StergereStadioane ss = new StergereStadioane();
            ss.Show();

            this.Hide();
        }
    }
}
