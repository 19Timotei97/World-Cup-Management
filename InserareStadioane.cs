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
using System.Text.RegularExpressions;

namespace CampionatFotbal
{
    public partial class InserareStadioane : Form
    {
        public InserareStadioane()
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

                if (con.State == ConnectionState.Open)
                {
                    string query = "INSERT INTO Stadioane";

                    // verific ca datele importante sa fie introduce in comanda de inserare
                    if (!String.IsNullOrWhiteSpace(txtDen.Text.ToString()) && !String.IsNullOrWhiteSpace(txtLoc.Text.ToString()))
                    {
                        if (Regex.IsMatch(txtDen.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtDen.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Denumirea nu poate avea cifre!");
                        else if (Regex.IsMatch(txtLoc.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtLoc.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Locatia nu poate avea cifre!");
                        else
                        {
                            if (!String.IsNullOrWhiteSpace(txtCap.Text.ToString()))
                            {
                                query += "(Denumire, Locatie, Capacitate) VALUES('" + txtDen.Text.ToString() + "', '" + txtLoc.Text.ToString() + "', ";
                                query += txtCap.Text.ToString() + ");";

                                SqlCommand com = new SqlCommand(query, con);
                                com.ExecuteNonQuery();
                                com.Dispose();
                            }

                            if (!String.IsNullOrWhiteSpace(txtCap.Text.ToString()) && !String.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                            {
                                if (!Regex.IsMatch(txtAn.Text.ToString(), "^\\d+$"))
                                    MessageBox.Show("Introduceți un număr și nu alte caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                else
                                {
                                    query += "(Denumire, Locatie, Capacitate, An_Constr) VALUES('" + txtDen.Text.ToString() + "', '" + txtLoc.Text.ToString() + "', " + txtCap.Text.ToString() + ", ";
                                    query += txtAn.Text.ToString() + ");";

                                    SqlCommand com = new SqlCommand(query, con);
                                    com.ExecuteNonQuery();
                                    com.Dispose();
                                }
                            }

                            if (!String.IsNullOrWhiteSpace(txtCap.Text.ToString()) && !String.IsNullOrWhiteSpace(txtAn.Text.ToString()) && !String.IsNullOrWhiteSpace(txtCost.Text.ToString()))
                            {
                                if (!Regex.IsMatch(txtAn.Text.ToString(), "^\\d+$"))
                                    MessageBox.Show("Introduceți un număr și nu alte caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                else
                                {
                                    query += " VALUES('" + txtDen.Text.ToString() + "', '" + txtLoc.Text.ToString() + "', " + txtCap.Text.ToString() + ", ";
                                    query += txtAn.Text.ToString() + ", " + txtCost.Text.ToString() + ");";

                                    SqlCommand com = new SqlCommand(query, con);
                                    com.ExecuteNonQuery();
                                    com.Dispose();
                                }
                            }

                            else
                            {
                                query += "(Denumire, Locatie) VALUES('" + txtDen.Text.ToString() + "', '" + txtLoc.Text.ToString() + "');";
                                SqlCommand com = new SqlCommand(query, con);
                                com.ExecuteNonQuery();
                                com.Dispose();
                            }

                            string queryTest = "SELECT * FROM Stadioane WHERE Denumire = '" + txtDen.Text.ToString() + "' AND Locatie = '" + txtLoc.Text.ToString() + "';";
                            SqlCommand sqlCom2 = new SqlCommand(queryTest, con);
                            SqlDataReader dr = sqlCom2.ExecuteReader();

                            if (dr.Read())
                                MessageBox.Show("Success!");

                            else MessageBox.Show("Inregistrarea nu a putut fi inserata!");

                        }
                    }
                    else MessageBox.Show("Introduceti datele marcate cu \'*\'!");                    
                }
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Eroare aparuta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Form1.UN == "Timotei")
            {
                MeniuInserare mp = new MeniuInserare();
                mp.Show();

                this.Hide();
            }

            else
            {
                MeniuInserareUser miu = new MeniuInserareUser();
                miu.Show();

                this.Hide();
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
                string test = "SELECT * FROM Stadioane;";

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
