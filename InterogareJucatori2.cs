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
    public partial class InterogareJucatori2 : Form
    {
        public InterogareJucatori2()
        {
            InitializeComponent();      
        }
        
        private void fillComboBox()
        {
            cBDupl.Items.Add("Da");
            cBDupl.Items.Add("Nu");
        }

        private void fillComboBox1()
        {
            cBSort.Items.Add("Da");
            cBSort.Items.Add("Nu");
        }


        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInterogareJucatori mij = new MeniuInterogareJucatori();
            mij.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                var sort = cBSort.Text;
                var dupl = cBDupl.Text;

                // Testare modificarea datelor
                SqlDataAdapter sda = new SqlDataAdapter();
                string query = "";


                if (sort.Equals("Da"))
                {
                    if (dupl.Equals("Da"))
                        query += "SELECT DISTINCT J.Nume, J.Prenume, J.Capitan, G.ID_Meci AS Meci, COUNT(*) AS Goluri_marcate" +
                        " FROM Jucatori J, Goluri G" +
                        " WHERE G.ID_Juc = J.ID_Juc AND J.Capitan = 'da'" +
                        " GROUP BY J.Nume, J.Prenume, J.Capitan, G.ID_Meci " +
                        " ORDER BY J.Nume;";

                    else query += "SELECT J.Nume, J.Prenume, J.Capitan, G.ID_Meci AS Meci, COUNT(*) AS Goluri_marcate" +
                        " FROM Jucatori J, Goluri G" +
                        " WHERE G.ID_Juc = J.ID_Juc AND J.Capitan = 'da'" +
                        " GROUP BY J.Nume, J.Prenume, J.Capitan, G.ID_Meci" +
                        " ORDER BY J.Nume;";
                }

                else
                {
                    if (dupl.Equals("Da"))
                        query += "SELECT DISTINCT J.Nume, J.Prenume, J.Capitan, G.ID_Meci AS Meci, COUNT(*) AS Goluri_marcate" +
                        " FROM Jucatori J, Goluri G" +
                        " WHERE G.ID_Juc = J.ID_Juc AND J.Capitan = 'da'" +
                        " GROUP BY J.Nume, J.Prenume, J.Capitan, G.ID_Meci" +
                        " ORDER BY J.Nume DESC;";

                    else query += "SELECT J.Nume, J.Prenume, J.Capitan, G.ID_Meci AS Meci, COUNT(*) AS Goluri_marcate" +
                        " FROM Jucatori J, Goluri G" +
                        " WHERE G.ID_Juc = J.ID_Juc AND J.Capitan = 'da'" +
                        " GROUP BY J.Nume, J.Prenume, J.Capitan, G.ID_Meci" +
                        " ORDER BY J.Nume DESC";
                }

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

        private void InterogareJucatori2_Load(object sender, EventArgs e)
        {
            fillComboBox();
            fillComboBox1();
        }
    }
}
