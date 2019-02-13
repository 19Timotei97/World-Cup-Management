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
    public partial class CreareCont : Form
    {
        public CreareCont()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();

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
                    if (!String.IsNullOrWhiteSpace(txtUN.Text.ToString()) && !String.IsNullOrWhiteSpace(txtPass.Text.ToString()) && !String.IsNullOrWhiteSpace(txtRPass.Text.ToString()))
                    {

                        // Conditii pentru un username valid
                        if (txtUN.Text.ToString().Length < 4)
                            MessageBox.Show("Username-ul trebuie să aibe cel puțin 4 caractere!");

                        // Conditie pentru o parola valida
                        if (txtPass.Text.ToString().Length < 4)
                            MessageBox.Show("Parola trebuie să aibe cel puțin 4 caractere!");

                        // Daca parola reintrodusa nu se potriveste cu cea initiala
                        if (!txtRPass.Text.ToString().Equals(txtPass.Text.ToString()))
                            MessageBox.Show("Parola nu se potrivește!");

                        // Verific existenta contului inainte de adaugare
                        string verif = "SELECT Username, Password FROM Users WHERE Username = '" + txtUN.Text.ToString() + "' AND Password = '" + txtPass.Text.ToString() + "';";
                        SqlCommand vrf = new SqlCommand(verif, con);
                        SqlDataReader DR = vrf.ExecuteReader();

                        if (DR.Read())
                        {
                            MessageBox.Show("Contul exista deja!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            vrf.Dispose();
                            DR.Close();
                        }

                        else
                        {
                            DR.Close();
                            string insert = "INSERT INTO Users VALUES('" + txtUN.Text.ToString() + "', '" + txtPass.Text.ToString() + "');";
                            SqlCommand insUser = new SqlCommand(insert, con);
                            insUser.ExecuteNonQuery();
                            insUser.Dispose();

                            string check = "Select Username, Password FROM Users WHERE Username = '" + txtUN.Text.ToString() + "' AND Password = '" + txtPass.Text.ToString() + "';";
                            SqlCommand checkUser = new SqlCommand(check, con);
                            SqlDataReader sqlDR = checkUser.ExecuteReader(CommandBehavior.CloseConnection);

                            if (sqlDR.Read())
                            {
                                string hello = "Bine ați venit, " + txtUN.Text.ToString();
                                MessageBox.Show("Cont creat!\n" + hello);


                                MeniuUser mu = new MeniuUser();
                                mu.Show();

                                this.Hide();
                            }
                            sqlDR.Close();
                            checkUser.Dispose();
                            
                        }                           
                    }
                    else MessageBox.Show("Contul nu a putut fi creat!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare apărută la crearea contului!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
