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
    public partial class ModificareStadioane : Form
    {
        public ModificareStadioane()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

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

        private void fillComboBox()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlCon))
                {
                    con.Open();
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT Denumire FROM Stadioane", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBDen.DisplayMember = "Denumire";
                    cBDen.DataSource = dt;
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
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    string query = "UPDATE Stadioane SET ";

                    var denumire = cBDen.Text;

                    if (!String.IsNullOrWhiteSpace(txtNDen.Text.ToString()))
                    {
                        if (Regex.IsMatch(txtNDen.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtNDen.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Denumirea nu poate contine cifre!");
                        else query += "Denumire = '" + txtNDen.Text.ToString() + "',";
                    }

                    if (!String.IsNullOrWhiteSpace(txtNLoc.Text.ToString()))
                    {
                        if (Regex.IsMatch(txtNLoc.Text.ToString(), "^[A-Za-z]+[0-9]+$") || Regex.IsMatch(txtNLoc.Text.ToString(), "^\\[0-9]+[A-Za-z]*"))
                            MessageBox.Show("Locatia nu poate avea cifre!");
                        else query += "Locatie = '" + txtNLoc.Text.ToString() + "',";
                    }

                    if (!String.IsNullOrWhiteSpace(txtNCap.Text.ToString()))
                        query += "Capacitate = " + txtNCap.Text.ToString() + ",";

                    if (!String.IsNullOrWhiteSpace(txtNAn.Text.ToString()))
                    {
                        if (!Regex.IsMatch(txtNAn.Text.ToString(), "^\\d+$"))
                            MessageBox.Show("Introduceți un număr și nu alte caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        else
                        {
                            query += "An_Constr = " + txtNAn.Text.ToString() + ",";
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(txtNCost.Text.ToString()))
                    {
                        if (!Regex.IsMatch(txtNCost.Text.ToString(), "^\\d+$"))
                            MessageBox.Show("Introduceți un număr și nu alte caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            query += "Cost = " + txtNCost.Text.ToString() + ",";
                        }
                    }
                    query = query.Remove(query.Length - 1);
                    query += " WHERE Denumire = '" + denumire  + "';";

                    SqlCommand command = new SqlCommand(query, con);
                    command.ExecuteNonQuery();

                    command.Dispose();

                }
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
                string test = "SELECT Denumire, Locatie, Capacitate, An_Constr AS 'Anul construcției', Cost FROM Stadioane;";

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

        private void ModificareStadioane_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}