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
    public partial class InterogareStadioane1 : Form
    {
        public InterogareStadioane1()
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
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT Data FROM Meciuri", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBData.DisplayMember = "Data";
                    cBData.DataSource = dt;
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
                cBRasp.Items.Add("Da");
                cBRasp.Items.Add("Nu");
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InterogareStadioane1_Load(object sender, EventArgs e)
        {
            fillComboBox();
            fillComboBox1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInterogareStadioane mis = new MeniuInterogareStadioane();
            mis.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                var rasp = cBRasp.Text;
                var data = cBData.Text;
                
                // Testare modificarea datelor
                SqlDataAdapter sda = new SqlDataAdapter();
                string query = "";

                if (rasp.Equals("Da"))
                    query += "SELECT M.Data, S.Denumire AS 'Stadionul', E1.Nume AS 'Echipa 1', E2.Nume AS 'Echipa 2', M.Scor_Ech1, M.Scor_Ech2" +
                        " FROM Meciuri M, Stadioane S, Echipe E1, Echipe E2 " +
                        "WHERE M.ID_Ech1 = E1.ID_Ech AND M.ID_Ech2 = E2.ID_Ech AND M.ID_Std = S.ID_Std AND M.ID_Meci IN " +
                        "(SELECT ID_Meci FROM Meciuri " +
                        "WHERE Data = '" + data + "') " +
                        "GROUP BY M.Data, S.Denumire, E1.Nume, E2.Nume, M.Scor_Ech1, M.Scor_Ech2; ";

                else query += "SELECT M.Data, S.Denumire AS 'Stadionul', E1.Nume AS 'Echipa 1', E2.Nume AS 'Echipa 2', M.Scor_Ech1, M.Scor_Ech2" +
                        " FROM Meciuri M, Stadioane S, Echipe E1, Echipe E2 " +
                        "WHERE M.ID_Ech1 = E1.ID_Ech AND M.ID_Ech2 = E2.ID_Ech AND M.ID_Std = S.ID_Std AND M.ID_Meci NOT IN " +
                        "(SELECT ID_Meci FROM Meciuri " +
                        "WHERE Data = '" + data + "') " +
                        "GROUP BY M.Data, S.Denumire, E1.Nume, E2.Nume, M.Scor_Ech1, M.Scor_Ech2; ";

                SqlCommand comTest = new SqlCommand(query, con);
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
