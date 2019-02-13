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
    public partial class ModificareJucatori : Form
    {
        public ModificareJucatori()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void fillComboBox()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlCon))
                {
                    con.Open();
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT Nume + ' ' + Prenume AS Nume_jucator FROM Jucatori", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBNume.DisplayMember = "Nume_jucator";
                    cBNume.DataSource = dt;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillComboBox1()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlCon))
                {
                    con.Open();
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT Nume FROM Echipe", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBEch.Text = "";
                    cBEch.DisplayMember = "Nume";
                    cBEch.DataSource = dt;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.UN == "Timotei")
            {
                MeniuModificare mm = new MeniuModificare();
                mm.Show();

                this.Hide();
            }

            else
            {
                MeniuModificareUser mmu = new MeniuModificareUser();
                mmu.Show();

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
                    string query = "UPDATE Jucatori SET ";

                    var nume = cBNume.Text;
                    var nume_pren = nume.Split(null);

                    string data_n = "SELECT Data_N FROM Jucatori WHERE Nume = '" + nume_pren[0]
                        + "' AND Prenume = '" + nume_pren[1] + "';";
                    SqlCommand get_Data_N = new SqlCommand(data_n, con);
                    var Data_N = (DateTime)get_Data_N.ExecuteScalar();


                    // verific pe rand fiecare camp
                    // si daca nu este Null sau blank, le folosesc in query

                    if (!String.IsNullOrWhiteSpace(txtNNume.Text.ToString()))
                        if (Regex.IsMatch(txtNNume.Text.ToString(), "^[A-Za-z]*[0-9]+$") || Regex.IsMatch(txtNNume.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Numele nu pot conține cifre!");
                        else query += "Nume = '" + txtNNume.Text.ToString() + "',";

                    if (!String.IsNullOrWhiteSpace(txtNPren.Text.ToString()))
                        if (Regex.IsMatch(txtNPren.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtNPren.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Prenumele nu pot conține cifre!");
                        else query += "Prenume = '" + txtNPren.Text.ToString() + "',";


                    // Verifica daca se modifica un element din data de nastere:
                    // Ziua, Luna sau Anul
                    if (!string.IsNullOrWhiteSpace(txtZi.Text.ToString()))
                    {
                        // Am toate cele 3 componente
                        if (!string.IsNullOrWhiteSpace(txtLuna.Text.ToString()) && !string.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                        {
                            if(Int32.TryParse(txtZi.Text, out int zi) && Int32.TryParse(txtLuna.Text, out int luna) && Int32.TryParse(txtAn.Text, out int an))
                            {
                                if (zi > 31 || zi < 1)
                                    MessageBox.Show("Zi incorecta!");
                                else if (luna > 12 || luna < 1)
                                    MessageBox.Show("Luna incorecta!");
                                else if (an > 2018 && an < 1950)
                                    MessageBox.Show("An incorect!");
                                else
                                {
                                    string ndn = txtAn.Text.ToString() + "-" + txtLuna.Text.ToString() + "-" + txtZi.Text.ToString();
                                    query += "Data_N = '" + ndn + "',";
                                }
                            }
                            
                        }

                        // Am Zi si An dar nu am Luna
                        else if (string.IsNullOrWhiteSpace(txtLuna.Text.ToString()) && !string.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                        {
                            string ndn = txtAn.Text.ToString() + "-" + Data_N.Month + "-" + txtZi.Text.ToString();
                            query += "Data_N = '" + ndn + "',";
                        }

                        // Am Zi si Luna, dar nu am An
                        else if (string.IsNullOrWhiteSpace(txtAn.Text.ToString()) && !string.IsNullOrWhiteSpace(txtLuna.Text.ToString()))
                        {
                            string ndn = Data_N.Year + "-" + txtLuna.Text.ToString() + "-" + txtZi.Text.ToString();
                            query += "Data_N = '" + ndn + "',";
                        }

                        else if (String.IsNullOrWhiteSpace(txtLuna.Text.ToString()) && String.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                        {
                            string ndn = Data_N.Year + "-" + Data_N.Month + "-" + txtZi.Text.ToString();
                            query += "Data_N = '" + ndn + "',";
                        }
                    }

                    // Am Luna
                    else if (string.IsNullOrWhiteSpace(txtZi.Text.ToString()) && !string.IsNullOrWhiteSpace(txtLuna.Text.ToString()))
                    {
                        //si An, fara Zi
                        if (!string.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                        {
                            string ndn = txtAn.Text.ToString() + "-" + txtLuna.Text.ToString() + "-" + Data_N.Day;
                            query += "Data_N = '" + ndn + "',";
                        }

                        // fara Zi si An
                        else
                        {
                            string ndn = Data_N.Year + "-" + txtLuna.Text.ToString() + "-" + Data_N.Day;
                            query += "Data_N = '" + ndn + "',";
                        }

                    }

                    // fara Zi si Luna
                    else if (string.IsNullOrWhiteSpace(txtZi.Text.ToString()) && string.IsNullOrWhiteSpace(txtLuna.Text.ToString()))
                    {
                        // dar am An
                        if (!string.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                        {
                            string ndn = txtAn.Text.ToString() + "-" + Data_N.Month + "-" + Data_N.Day;
                            query += "Data_N = '" + ndn + "',";
                        }
                    }

                    //if (!String.IsNullOrWhiteSpace(txtNDN.Text.ToString()))
                    //  query += "Data_N = '" + txtNDN.Text.ToString() + "',";

                    if (!string.IsNullOrWhiteSpace(cBCap.Text))
                        query += "Capitan = '" + cBCap.Text + "',";

                    if (!string.IsNullOrWhiteSpace(cBEch.Text))
                    {
                        string ech = "SELECT ID_Ech FROM Echipe WHERE Nume = '" + cBEch.Text + "';";
                        SqlCommand getEch = new SqlCommand(ech, con);
                        int id_ech = (int)getEch.ExecuteScalar();

                        query += "ID_Ech = " + id_ech + ",";
                        getEch.Dispose();
                    }

                    if (!string.IsNullOrWhiteSpace(cBPoz.Text))
                        query += "Pozitie = '" + cBPoz.Text + "',";

                    query = query.Remove(query.Length - 1);
                    query += " WHERE Nume = '" + nume_pren[0] + "' AND Prenume = '" + nume_pren[1] + "';";
                    SqlCommand modif = new SqlCommand(query, con);

                    modif.ExecuteNonQuery();
                    modif.Dispose();
                    con.Close();
                }
                else MessageBox.Show("Introduceți numele și prenumele Jucătorului!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);              
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare apărută în urma modificării", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string test = "SELECT J.Nume + ' ' + J.Prenume AS 'Nume jucător', J.Data_N AS 'Data nașterii'," +
                    " E.Nume AS 'Nume echipă', " +
                    "J.Pozitie, Capitan FROM Jucatori J, Echipe E " +
                    "WHERE J.ID_Ech = E.ID_Ech";

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

        private void FillComboBox2()
        {
            try
            {
                cBPoz.Text = "";
                string[] pozitii = new string[] { "GK", "CB", "LB", "RB", "LWB", "RWB", "CDM", "CM", "LM", "RM", "CAM", "ST", "CF", "LW", "RW", "LF", "RF" };
                cBPoz.Items.AddRange(pozitii);
                this.Controls.Add(cBPoz);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea pozitiilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboBox3()
        {
            try
            {
                cBCap.Text = "";
                cBCap.Items.Add("Da");
                cBCap.Items.Add("Nu");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificareJucatori_Load(object sender, EventArgs e)
        {
            fillComboBox();
            fillComboBox1();
            FillComboBox2();
            FillComboBox3();
        }
    }
}
