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
    public partial class MeniuPrincipal : Form
    {
        public MeniuPrincipal()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if(con.State == ConnectionState.Open)
                {
                    Interogare itg = new Interogare();
                    this.Hide();
                    itg.Show();
                }

            }

            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {

                    MeniuAlterare ma = new MeniuAlterare();
                    ma.Show();

                    this.Close();
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiunilor!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Inserare ins = new Inserare();
                    ins.Show();
                    this.Close();
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Close();
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error caught!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if(con.State == ConnectionState.Open)
                {
                    Modificare md = new Modificare();
                    md.Show();

                    this.Close();
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error ocurred while processing your query!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);

                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    Stergere st = new Stergere();
                    st.Show();

                    this.Close();
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error ocurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }
    }
}
