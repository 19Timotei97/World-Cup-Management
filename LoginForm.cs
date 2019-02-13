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
    public partial class Form1 : Form
    {
        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        public static string UN;

        private void button1_Click(object sender, EventArgs e)
        {
            UN = txtId.Text.ToString();
            //Apasare pe Conectare

            // Daca se omite userul
            if(txtId.Text.ToString() == "")
            {
                MessageBox.Show("Introduceți username-ul", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Focus();
                return;
            }

            // Daca se omite parola
            if(txtPass.Text.ToString() == "")
            {
                MessageBox.Show("Introduceți parola", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Focus();
                return;
            }

            try
            {
                // Deschid o conexiune catre baza de date
                SqlConnection con = new SqlConnection(sqlCon);

                // Creez comanda
                SqlCommand comm = new SqlCommand("SELECT Username, Password FROM Users WHERE Username = '" + txtId.Text + "' AND Password = '" + txtPass.Text + "';", con);
                               
                //SqlParameter uname = new SqlParameter("@Username", SqlDbType.VarChar);
                //SqlParameter pass = new SqlParameter("@Password", SqlDbType.VarChar);

                //uname.Value = txtId.Text;
                //pass.Value = txtPass.Text;

                // Adaug parametrii comenzii
                //comm.Parameters.Add(uname);
                //comm.Parameters.Add(pass);

                
                comm.Connection.Open();

                SqlDataReader sdr = comm.ExecuteReader(CommandBehavior.CloseConnection);

                if(sdr.Read())
                {
                    MessageBox.Show("Bine ati venit, " + txtId.Text + "!");
                    if (txtId.Text.ToString() == "Timotei" && txtPass.Text.ToString() == "proiectBD")
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

                else
                {
                    MessageBox.Show("Login esuat! Reincercati", "Neautorizat!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Sterg continutul
                    txtId.Clear();
                    txtPass.Clear();

                    txtId.Focus();
                }

                if (con.State == ConnectionState.Open)
                    // Inchid conexiunea
                    con.Dispose();

            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Eroare aparuta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            /* Pentru test
            // deschid o conexiune catre baza de date
            SqlConnection con = new SqlConnection(sqlCon);
            con.Open();

            // daca s-a conectat
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = "INSERT INTO Test(nume) VALUES ('" + txtPass.Text.ToString() + "')";
                
                // execut comanda
                SqlCommand sqlCom = new SqlCommand(query, con);
                sqlCom.ExecuteNonQuery();
                
                // testez comanda
                MessageBox.Show("Connection was successfull");
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // apasare pe Anulare
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreareCont cc = new CreareCont();
            cc.Show();


            this.Hide();
        }
    }
}
