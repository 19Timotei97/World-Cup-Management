using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CampionatFotbal
{
    public partial class MeniuUser : Form
    {
        public MeniuUser()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MeniuInterogare mi = new MeniuInterogare();
            mi.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MeniuInserareUser miu = new MeniuInserareUser();
                miu.Show();

                this.Hide();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiilor!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuModificareUser mfu = new MeniuModificareUser();
            mfu.Show();

            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MeniuStergereUser msu = new MeniuStergereUser();
            msu.Show();

            this.Hide();
        }
    }
}
