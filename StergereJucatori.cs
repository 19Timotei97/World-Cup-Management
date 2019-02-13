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
    public partial class StergereJucatori : Form
    {
        public StergereJucatori()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void FillComboBox()
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


        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.UN == "Timotei")
            {
                MeniuStergere ms = new MeniuStergere();
                ms.Show();

                this.Hide();
            }

            else
            {
                MeniuStergereUser msu = new MeniuStergereUser();
                msu.Show();

                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if(con.State == ConnectionState.Open)
                {
                    var nume = cBNume.Text;
                    string[] nume_pren = nume.Split(null);

                    string query = "DELETE FROM Jucatori WHERE Nume = '" + nume_pren[0] + "' AND Prenume = '" + nume_pren[1] + "';";

                    SqlCommand com = new SqlCommand(query, con);
                    com.ExecuteNonQuery();

                    string query2 = "SELECT * FROM Jucatori WHERE Nume = '" + nume_pren[0] + "' AND Prenume = '" + nume_pren[1] + "';";
                    SqlCommand comTest = new SqlCommand(query2, con);

                    SqlDataReader dr = comTest.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Nu s-a putut sterge inregistrarea!");
                    }

                    else MessageBox.Show("Inregistrare stearsa!");
                }

                  

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiilor efectuate!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void StergereJucatori_Load(object sender, EventArgs e)
        {
            FillComboBox();
        }
    }
}
