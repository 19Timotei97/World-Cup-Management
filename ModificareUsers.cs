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
    public partial class ModificareUsers : Form
    {
        public ModificareUsers()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuModificare mm = new MeniuModificare();
            mm.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if(con.State == ConnectionState.Open)
                {
                    string query = "UPDATE Users SET ";

                    var un = cBUN.Text;

                    if (!String.IsNullOrWhiteSpace(txtNUN.Text.ToString()))
                        query += "Username = '" + txtNUN.Text.ToString() + "',";

                    if (!String.IsNullOrWhiteSpace(txtNPass.Text.ToString()))
                        query += "Password = '" + txtNPass.Text.ToString() + "',";

                    query = query.Remove(query.Length - 1);

                    
                    query += " WHERE Username = '" + un + "';";
                    
                    SqlCommand com = new SqlCommand(query, con);
                    com.ExecuteNonQuery();
                    com.Dispose();
                }

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma modificarii!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                // Testare modificarea datelor
                SqlDataAdapter sda = new SqlDataAdapter();
                string test = "SELECT Username, Password AS Parola FROM Users;";

                SqlCommand comTest = new SqlCommand(test, con);
                sda.SelectCommand = comTest;

                DataTable db = new DataTable();

                sda.Fill(db);
                // Initializare simplificata
                BindingSource bSource = new BindingSource
                { DataSource = db };
                // inlocuieste bSource.DataSource = db

                DGW.DataSource = bSource;

                sda.Update(db);

                con.Close();
                comTest.Dispose();
                sda.Dispose();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea tabelei!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboBox()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlCon))
                {
                    con.Open();
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT Username FROM Users", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBUN.DisplayMember = "Username";
                    cBUN.DataSource = dt;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificareUsers_Load(object sender, EventArgs e)
        {
            FillComboBox();
        }
    }
}
