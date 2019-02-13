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
    public partial class MeniuStergere : Form
    {
        public MeniuStergere()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if(con.State == ConnectionState.Open)
                {
                   
                    MeniuPrincipal mp = new MeniuPrincipal();

                    mp.Show();

                    this.Hide();
                }

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiunilor desfasurate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private void button5_Click(object sender, EventArgs e)
        {
            StergereUsers su = new StergereUsers();
            su.Show();

            this.Hide();

        }
    }
}
