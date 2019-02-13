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
    public partial class InserareJucatori : Form
    {
        public InserareJucatori()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

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
                MeniuInserareUser mu = new MeniuInserareUser();
                mu.Show();

                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    string query = "INSERT INTO Jucatori";

                    // verific ca datele importante sa fie introduce in interogare
                    if (String.IsNullOrWhiteSpace(txtNume.Text.ToString()) || String.IsNullOrWhiteSpace(txtPren.Text.ToString()) || String.IsNullOrWhiteSpace(txtAn.Text.ToString()) || String.IsNullOrWhiteSpace(txtLuna.Text.ToString()) || String.IsNullOrWhiteSpace(txtZi.Text.ToString()))
                        MessageBox.Show("Introduceti datele marcate cu \'*\'!");

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
                                else
                                    MessageBox.Show("Conversie eșuată!");
                            }
                        }
                        else
                            MessageBox.Show("Conversie eșuată!");


                        var ech = cBEch.Text;
                        string search = "SELECT ID_Ech FROM Echipe WHERE Nume = '" + ech + "';";

                        SqlCommand srch = new SqlCommand(search, con);
                        string ID = "" + srch.ExecuteScalar();

                        //if (string.IsNullOrWhiteSpace(ID))
                        //     MessageBox.Show("Echipa nu există! Introduceți mai întâi echipa pentru a adăuga jucători!");



                        var poz = cBPoz.Text;
                        var cap = cBCap.Text.ToLower();
                        query += " VALUES ('" + txtNume.Text.ToString() + "', '" + txtPren.Text.ToString() + "', '";
                        query += data_n + "', '" + cap + "', '" + ID + "', '" + poz + "');";

                        SqlCommand com = new SqlCommand(query, con);
                        com.ExecuteNonQuery();

                        com.Dispose();
                        

                        string queryTest = "SELECT * FROM Jucatori WHERE Nume = '" + txtNume.Text.ToString() + "' AND Prenume = '" + txtPren.Text.ToString() + "';";
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                // Testare modificarea datelor
                SqlDataAdapter sda = new SqlDataAdapter();
                string test = "SELECT J.Nume + ' ' + Prenume, Data_N AS 'Data nașterii', E.Nume AS Echipa, J.Pozitie, Capitan FROM Jucatori J, Echipe E " +
                    "WHERE J.ID_Ech = E.ID_Ech;";

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
                    if (con.State == ConnectionState.Open)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT Nume FROM Echipe;", con);
                        DataTable dt = new DataTable();

                        da.Fill(dt);
                        cBEch.DisplayMember = "Nume";
                        cBEch.DataSource = dt;

                    }

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisare echipelor!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboBox1()
        {
            cBPoz.Text = "GK";
            string[] pozitii = new string[] { "GK", "CB", "LB", "RB", "LWB", "RWB", "CDM", "CM", "LM", "RM", "CAM", "ST", "CF", "LW", "RW", "LF", "RF" };
            cBPoz.Items.AddRange(pozitii);
            this.Controls.Add(cBPoz);
        }

        private void FillComboBox2()
        {
            cBCap.Items.Add("Da");
            cBCap.Items.Add("Nu");
        }
        private void InserareJucatori_Load(object sender, EventArgs e)
        {
            FillComboBox();
            FillComboBox1();
            FillComboBox2();
        }
    }
}
