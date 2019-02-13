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
    public partial class MeniuInserare : Form
    {
        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        public MeniuInserare()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MeniuPrincipal mp = new MeniuPrincipal();
            mp.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    InserareAntrenori ia = new InserareAntrenori();
                    ia.Show();

                    this.Close();
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiunilor desfasuarate!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    InserareJucatori ij = new InserareJucatori();
                    ij.Show();

                    this.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiunilor desfasuarate!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    InserareStadioane ist = new InserareStadioane();
                    ist.Show();

                    this.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiunilor desfasuarate!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InserareUsers iu = new InserareUsers();
            iu.Show();

            this.Hide();
        }
    }
}
