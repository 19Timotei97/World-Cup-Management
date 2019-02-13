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
    public partial class InterogareJucatori : Form
    {
        public InterogareJucatori()
        {
            InitializeComponent();
        }

        private void fillComboBox()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlCon))
                {
                    con.Open();
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT Nume FROM Echipe ORDER BY Nume;", con);

                    DataTable dt = new DataTable();

                    adap.Fill(dt);
                    cBEch.DisplayMember = "Nume";
                    cBEch.DataSource = dt;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea optiunilor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInterogareJucatori mi = new MeniuInterogareJucatori();
            mi.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                int count = 0;

                if (con.State == ConnectionState.Open)
                {
                    var ech = cBEch.Text;

                    string query = "SELECT COUNT(*) FROM Jucatori J INNER JOIN Echipe E ON J.ID_Ech = E.ID_Ech WHERE E.ID_Ech = ";


                    string caut_id_ech = "SELECT ID_Ech FROM Echipe WHERE Nume = '" + ech + "';";
                    SqlCommand id = new SqlCommand(caut_id_ech, con);

                    int id_ech = (int)id.ExecuteScalar();

                    id.Dispose();

                    query += id_ech + ";";

                    SqlCommand com = new SqlCommand(query, con);
                    count = (int)com.ExecuteScalar();

                    txtCount.Text = "" + count;
                    
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
                string test = "SELECT E.Nume AS 'Echipa', E.An_Fond AS 'Anul fondării', S.Denumire AS 'Stadion', " +
                    "A.Nume + ' ' + A.Prenume AS 'Antrenor', E.Pozitie AS 'Poziție în clasament' FROM Echipe E, Stadioane S, Antrenori A " +
                    "WHERE E.ID_Antr = A.ID_Antr AND E.ID_Std = S.ID_Std;";

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

        private void InterogareJucatori_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
