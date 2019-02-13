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
    public partial class ModificareAntrenori : Form
    {
        public ModificareAntrenori()
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
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT Nume + ' ' + Prenume AS Nume_antr FROM Antrenori", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBNume.DisplayMember = "Nume_antr";
                    cBNume.DataSource = dt;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    // string-ul folosit pentru comanda UPDATE
                    string query = "UPDATE Antrenori SET ";

                    var nume = cBNume.Text;
                    string[] nume_pre = nume.Split(null);

                    string data_n = "SELECT Data_N FROM Antrenori WHERE Nume = '" + nume_pre[0]
                        + "' AND Prenume = '" + nume_pre[1] + "';";
                    SqlCommand get_Data_N = new SqlCommand(data_n, con);
                    var Data_N = (DateTime)get_Data_N.ExecuteScalar();

                    // verific pe rand fiecare camp
                    // si daca nu este Null sau blank, le folosesc in query

                    if (!String.IsNullOrWhiteSpace(txtNNume.Text.ToString()))
                    {
                        if (Regex.IsMatch(txtNNume.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtNNume.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Numele nu pot conține cifre!");
                        else query += "Nume = '" + txtNNume.Text.ToString() + "',";
                    }

                    if (!String.IsNullOrWhiteSpace(txtNPren.Text.ToString()))
                    {
                        if (Regex.IsMatch(txtNPren.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtNPren.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Prenumele nu pot conține cifre!");
                        else query += "Prenume = '" + txtNPren.Text.ToString() + "',";
                    }

                    // Verifica daca se modifica un element din data de nastere:
                    // Ziua, Luna sau Anul
                    if (!string.IsNullOrWhiteSpace(txtZi.Text.ToString()))
                    {
                        // Am toate cele 3 componente
                        if (!string.IsNullOrWhiteSpace(txtLuna.Text.ToString()) && !string.IsNullOrWhiteSpace(txtAn.Text.ToString()))
                        {
                            string ndn = txtAn.Text.ToString() + "-" + txtLuna.Text.ToString() + "-" + txtZi.Text.ToString();
                            query += "Data_N = '" + ndn + "',";
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

                    if (!String.IsNullOrWhiteSpace(txtNInal.Text.ToString()))
                        query += "Inaltime = " + txtNInal.Text.ToString() + ",";

                    if (!String.IsNullOrWhiteSpace(txtNGr.Text.ToString()))
                        query += "Greutate = " + txtNGr.Text.ToString() + ",";

                    if (!String.IsNullOrWhiteSpace(txtSal.Text.ToString()))
                        query += "Salariu = " + txtSal.Text.ToString() + ",";

                    // Se elimina ultima din virgula
                    query = query.Remove(query.Length - 1);
                    query += " WHERE Nume = '" + nume_pre[0] + "' AND Prenume = '" + nume_pre[1] + "';";

                    SqlCommand com = new SqlCommand(query, con);

                    com.ExecuteNonQuery();

                    com.Dispose();
                }
                else MessageBox.Show("Selectati numele antrenorului", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.StackTrace, "Eroare aparuta in urma modificarii!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                // Testare modificarea datelor
                SqlDataAdapter sda = new SqlDataAdapter();
                string test = "SELECT Nume, Prenume, Data_N AS 'Data nașterii', Salariu, Inaltime, Greutate FROM Antrenori;";

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

        private void ModificareAntrenori_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}