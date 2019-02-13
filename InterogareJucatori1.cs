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
    public partial class InterogareJucatori1 : Form
    {
        public InterogareJucatori1()
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
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT DISTINCT Pozitie FROM Jucatori;", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBPoz.DisplayMember = "Pozitie";
                    cBPoz.DataSource = dt;
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

                // Testare modificarea datelor
                SqlDataAdapter sda = new SqlDataAdapter();

                var poz = cBPoz.Text;

                string test = "SELECT J.Nume + ' ' + Prenume AS Jucătorul, Data_N AS 'Data nașterii', E.Nume AS 'Echipa', J.Pozitie, Capitan " +
                    "FROM Jucatori J, Echipe E WHERE J.ID_Ech = E.ID_Ech AND J.Pozitie = '" + poz + "';";

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

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInterogareJucatori mi = new MeniuInterogareJucatori();
            mi.Show();

            this.Close();
        }

        private void InterogareJucatori1_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
