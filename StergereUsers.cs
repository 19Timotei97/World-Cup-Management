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
    public partial class StergereUsers : Form
    {
        public StergereUsers()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuStergere ms = new MeniuStergere();
            ms.Show();

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
                    var un = cBUN.Text;
                    string query = "DELETE FROM Users WHERE Username = '" + un + "';";

                    SqlCommand com = new SqlCommand(query, con);
                    com.ExecuteNonQuery();

                    string query2 = "SELECT * FROM Users WHERE Username = '" + un + "';";
                    SqlCommand comTest = new SqlCommand(query2, con);

                    SqlDataReader dr = comTest.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Nu s-a putut sterge inregistrarea!");
                    }

                    else MessageBox.Show("Inregistrare stearsa!");
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma stergerii!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void fillComboBox()
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

        private void StergereUsers_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
