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
    public partial class InserareUsers : Form
    {
        public InserareUsers()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInserare mp = new MeniuInserare();
            mp.Show();

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
                    // verific ca datele importante sa fie introduce in interogare
                   if (String.IsNullOrWhiteSpace(txtUN.Text.ToString()) || String.IsNullOrWhiteSpace(txtPass.Text.ToString()))
                        MessageBox.Show("Introduceti datele marcate cu \'*\'!");

                    string query = "INSERT INTO Users VALUES ('" + txtUN.Text.ToString() + "', '" + txtPass.Text.ToString() + "');";

                    SqlCommand com = new SqlCommand(query, con);
                    com.ExecuteNonQuery();

                    string queryTest = "SELECT * FROM Users WHERE Username = '" + txtUN.Text.ToString() + "';";
                    SqlCommand sqlCom = new SqlCommand(queryTest, con);
                    SqlDataReader dr = sqlCom.ExecuteReader();

                    if (dr.Read())
                        MessageBox.Show("Succes!");

                    else MessageBox.Show("Inserarea nu a putut avea loc!");
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiilor efectuate!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
