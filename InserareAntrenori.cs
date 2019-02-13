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
    public partial class InserareAntrenori : Form
    {
        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        public InserareAntrenori()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {

                    string query = "INSERT INTO ANTRENORI";

                    // verific ca datele importante sa fie introduce in interogare
                    if (String.IsNullOrWhiteSpace(txtNume.Text.ToString()) || String.IsNullOrWhiteSpace(txtPren.Text.ToString()) || String.IsNullOrWhiteSpace(txtZi.Text.ToString()) || String.IsNullOrWhiteSpace(txtLuna.Text.ToString()) || String.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                        MessageBox.Show("Introduceți datele marcate cu \'*\'!");

                    else if (Regex.IsMatch(txtNume.Text.ToString(), "^[A-Za-z]*[0-9]+$") || Regex.IsMatch(txtNume.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                    {
                        if (Regex.IsMatch(txtPren.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtPren.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Numele și prenumele nu pot conține cifre!");
                        else
                            MessageBox.Show("Numele nu pot conține cifre!");

                    }
                    else if (Regex.IsMatch(txtPren.Text.ToString(), "^[A-Za-z]*[0-9]+$") || Regex.IsMatch(txtPren.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                        MessageBox.Show("Prenumele nu pot conține cifre!");

                    else
                    {

                        bool bisect = false;

                        int[] zile_31 = { 1, 3, 5, 7, 8, 10, 12 };
                        int[] zile_30 = { 4, 6, 9, 11 };
                        string data_n = "";


                        if (Int32.TryParse(txtAn.Text, out int an))
                        {
                            if (an < 1900 || an > 2018)
                                MessageBox.Show("Anul nu este valid! Reîncercați!");
                            else
                            {
                                // pas 1
                                if (an % 4 == 0)
                                {
                                    // pas 2
                                    if (an % 100 == 0)
                                    {
                                        // pas 3
                                        if (an % 400 == 0)
                                            bisect = true;
                                        else bisect = false;
                                    }
                                    // pas 4
                                    else bisect = true;
                                }
                                // pas 5
                                else bisect = false;
                            }
                        }
                        else MessageBox.Show("Conversie eșuată!");




                        if (Int32.TryParse(txtLuna.Text, out int luna))
                        {
                            if (luna < 1 || luna > 12)
                                MessageBox.Show("Luna nu este validă! Reîncercați!");

                            else
                            {
                               if (Int32.TryParse(txtZi.Text, out int zi))
                                    {
                                        for (int i = 0; i < zile_31.Length; ++i)
                                            if (luna == zile_31[i] && (zi > 31 || zi < 1))
                                                MessageBox.Show("Ziua nu este validă pentru luna curentă! Reîncercați!");
                                        for (int i = 0; i < zile_30.Length; ++i)
                                            if (luna == zile_30[i] && (zi > 30 || zi < 1))
                                                MessageBox.Show("Ziua nu este validă pentru luna curentă! Reîncercați!");


                                        if (bisect && luna == 2 && (zi > 29 || zi < 1))
                                            MessageBox.Show("Ziua nu este validă deoarece anul este bisect!");
                                        else if (bisect != true && luna == 2 && (zi > 28 || zi < 1))
                                            MessageBox.Show("Ziua nu este validă pentru luna curentă!");
                                        else data_n += txtAn.Text.ToString() + "-" + txtLuna.Text.ToString() + "-" + txtZi.Text.ToString();
                                    }
                                    else MessageBox.Show("Conversie eșuată!");                               
                            }
                        }
                        else MessageBox.Show("Conversie eșuată!");


                        if (String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && String.IsNullOrWhiteSpace(txtGreut.Text.ToString()) && String.IsNullOrWhiteSpace(txtSal.Text.ToString()))
                        {
                            query += "(Nume, Prenume, Data_N) VALUES('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "');";
                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }

                        else if (!String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && String.IsNullOrWhiteSpace(txtGreut.Text.ToString()) && String.IsNullOrWhiteSpace(txtSal.Text.ToString()))
                        {
                            if (Double.TryParse(txtInal.Text, out double inal))
                            {
                                if (inal > 2.50 || inal < 1.50)
                                    MessageBox.Show("Inaltime introdusa gresit! Reincercati");

                                else
                                {
                                    query += "(Nume, Prenume, Data_N, Inaltime) VALUES('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "', ";
                                    query += txtInal.Text.ToString() + ");";
                                }
                            }
                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }

                        else if (!String.IsNullOrWhiteSpace(txtGreut.Text.ToString()) && String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && String.IsNullOrWhiteSpace(txtSal.Text.ToString()))
                        {
                            if (Int32.TryParse(txtGreut.Text, out int gre))
                            {
                                if (gre < 50 || gre > 110)
                                {
                                    MessageBox.Show("Greutate incorecta! Reincercati");
                                }
                                else
                                {
                                    query += "(Nume, Prenume, Data_N, Greutate) VALUES('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "', ";
                                    query += txtGreut.Text.ToString() + ");";
                                }

                            }
                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }

                        else if (!String.IsNullOrWhiteSpace(txtSal.Text.ToString()) && String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && String.IsNullOrWhiteSpace(txtGreut.Text.ToString()))
                        {
                            if (Double.TryParse(txtSal.Text, out double sal))
                            {
                                if (sal < 1000 || sal > 1000000000)
                                {
                                    MessageBox.Show("Salariu incorect! Reincercati");
                                }
                                else
                                {
                                    query += "(Nume, Prenume, Data_N, Salariu) VALUES('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "', ";
                                    query += txtSal.Text.ToString() + ");";
                                }

                            }
                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }

                        else if (!String.IsNullOrWhiteSpace(txtSal.Text.ToString()) && !String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && String.IsNullOrWhiteSpace(txtGreut.Text.ToString()))
                        {
                            if (Double.TryParse(txtSal.Text, out double sal))
                            {
                                if (Double.TryParse(txtInal.Text, out double inal))
                                {
                                    if (sal < 1000 || sal > 1000000000 || inal < 1.50 || inal > 2.20)
                                    {
                                        MessageBox.Show("Salariu sau inaltime incorecte! Reincercati");
                                    }
                                    else
                                    {
                                        query += "(Nume, Prenume, Data_N, Inaltime, Salariu) VALUES('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "', ";
                                        query += txtInal.Text.ToString() + ", " + txtSal.Text.ToString() + ");";
                                    }
                                }
                            }

                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }

                        else if (String.IsNullOrWhiteSpace(txtSal.Text.ToString()) && !String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && !String.IsNullOrWhiteSpace(txtGreut.Text.ToString()))
                        {
                            if (Int32.TryParse(txtInal.Text, out int greut))
                            {
                                if (Double.TryParse(txtInal.Text, out double inal))
                                {
                                    if (greut < 50 || greut > 130 || inal < 1.50 || inal > 2.20)
                                    {
                                        MessageBox.Show("Greutatea sau înălțimea introduse incorect! Reîncercați");
                                    }
                                    else
                                    {
                                        query += "(Nume, Prenume, Data_N, Inaltime, Greutate) VALUES('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "', ";
                                        query += txtInal.Text.ToString() + ", " + txtGreut.Text.ToString() + ");";
                                    }
                                }
                            }

                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }

                        else if (!String.IsNullOrWhiteSpace(txtSal.Text.ToString()) && String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && !String.IsNullOrWhiteSpace(txtGreut.Text.ToString()))
                        {
                            if (Int32.TryParse(txtInal.Text, out int greut))
                            {
                                if (Double.TryParse(txtSal.Text, out double sal))
                                {
                                    if (greut < 50 || greut > 130 || sal < 1000 || sal > 1000000000)
                                    {
                                        MessageBox.Show("Salariul sau greutatea introduse incorect! Reîncercați");
                                    }
                                    else
                                    {
                                        query += "(Nume, Prenume, Data_N, Inaltime, Greutate) VALUES('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "', ";
                                        query += txtInal.Text.ToString() + ", " + txtGreut.Text.ToString() + ");";
                                    }
                                }
                            }

                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }

                        else if (!String.IsNullOrWhiteSpace(txtInal.Text.ToString()) && !String.IsNullOrWhiteSpace(txtGreut.Text.ToString()) && !String.IsNullOrWhiteSpace(txtSal.Text.ToString()))
                        {
                            query += " VALUES ('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '" + data_n + "', ";
                            query += txtInal.Text.ToString() + ", " + txtGreut.Text.ToString() + ", " + txtSal.Text.ToString() + ");";

                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();

                            com.Dispose();
                        }


                        string queryTest = "SELECT * FROM Antrenori WHERE Nume = '" + txtNume.Text.ToString() + "' AND Prenume = '" + txtPren.Text.ToString() + "';";
                        SqlCommand sqlCom2 = new SqlCommand(queryTest, con);
                        SqlDataReader dr = sqlCom2.ExecuteReader();

                        if (dr.Read())
                            MessageBox.Show("Success!");

                        else MessageBox.Show("Inregistrarea nu a putut fi inserata!");
                    }                      
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
                MeniuInserare mi = new MeniuInserare();
                mi.Show();

                this.Close();
            }
            else
            {
                MeniuInserareUser miu = new MeniuInserareUser();
                miu.Show();

                this.Close();
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
                string test = "SELECT Nume, Prenume, Data_N AS 'Data nașterii', Inaltime, Greutate, Salariu FROM Antrenori;";

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

        private void InserareAntrenori_Load(object sender, EventArgs e)
        {

        }
    }
}
