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
    public partial class InterogareMeciuri : Form
    {
        public InterogareMeciuri()
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
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT ID_Meci FROM Meciuri", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBId.DisplayMember = "ID_Meci";
                    cBId.DataSource = dt;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInterogare mi = new MeniuInterogare();
            mi.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                string s1 = "", s2 = "";

                if (con.State == ConnectionState.Open)
                {
                    var id_meci = cBId.Text;

                    string query = "SELECT M.ID_Meci, E1.Nume, E2.Nume FROM Meciuri M INNER JOIN Echipe E1 ON M.ID_Ech1 = E1.ID_Ech " +
                       "INNER JOIN Echipe E2 ON M.ID_Ech2 = E2.ID_Ech WHERE M.ID_Meci = " + id_meci + ";";                    

                    SqlDataReader sqlDR;
                    SqlCommand com = new SqlCommand(query, con);

                    sqlDR = com.ExecuteReader();

                    while (sqlDR.Read())
                    {
                        s1 = (string)sqlDR.GetValue(1);
                        s2 = (string)sqlDR.GetValue(2);
                    }

                    txtE1.Text = s1;
                    txtE2.Text = s2;

                    
                    sqlDR.Close();
                    com.Dispose();
                    con.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma interogării!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string test = "SELECT * FROM Meciuri;";

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

        private void InterogareMeciuri_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
