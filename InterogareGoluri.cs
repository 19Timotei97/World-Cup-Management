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
    public partial class InterogareGoluri : Form
    {
        public InterogareGoluri()
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

                if (con.State == ConnectionState.Open)
                {

                    var id_meci = cBId.Text;

                    string query = "SELECT J.Nume + ' ' + J.Prenume AS Jucătorul, M.ID_Meci, COUNT(*) AS 'Număr de goluri' FROM Goluri G " +
                        "INNER JOIN Jucatori J ON G.ID_Juc = J.ID_Juc " +
                        "INNER JOIN Meciuri M ON G.ID_Meci = M.ID_Meci " +
                        "WHERE M.ID_Meci = " + id_meci  + " " +
                        "GROUP BY J.Nume, J.Prenume, M.ID_Meci; ";

                    SqlCommand comm = new SqlCommand(query, con);
                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = comm;

                    DataTable db = new DataTable();

                    sqlDA.Fill(db);
                    // Initializare simplificata
                    BindingSource bSource = new BindingSource
                    { DataSource = db };
                    // inlocuieste bSource.DataSource = db

                    DGW1.DataSource = bSource;

                    sqlDA.Update(db);

                    comm.Dispose();
                    sqlDA.Dispose();
                    
                    con.Close();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare apărută în urma interogării", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string test = "SELECT J.Nume + ' ' + J.Prenume AS 'Nume marcator', G.ID_Meci AS 'ID-ul meciului', " +
                    "G.Descriere AS 'Detalii gol' FROM Jucatori J, Goluri G " +
                    "WHERE G.ID_Juc = J.ID_Juc;";

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

        private void InterogareGoluri_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
