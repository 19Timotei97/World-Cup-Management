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
    public partial class StergereStadioane : Form
    {
        // internal System.Windows.Forms.ComboBox Stadioane;

        public StergereStadioane()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        /*
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if(con.State == ConnectionState.Open)
                {
                    
                    string queryCount = "SELECT COUNT(ID_Std) FROM Stadioane";

                    SqlCommand com = new SqlCommand(queryCount, con);
                    Int32 count = (Int32) com.ExecuteScalar(); 

                    string[] stadioane = new string[count];

                    string queryNume = "SELECT Denumire FROM Stadioane";
                    SqlCommand com2 = new SqlCommand(queryNume, con);

                    SqlDataReader dr = com2.ExecuteReader();

                    if (dr.Read())
                    {
                        for (int i = 0; i < count; ++i)
                            stadioane[i] = (string)dr.GetValue(i);
                    }

                    this.Stadioane = new System.Windows.Forms.ComboBox();
                    Stadioane.Items.AddRange(stadioane);

                    this.Stadioane.MaxDropDownItems = 5;
                    this.Stadioane.DropDownStyle = ComboBoxStyle.DropDownList;
                    this.Stadioane.Name = "Stadioane";

                    this.Controls.Add(this.Stadioane);

                    //this.Stadioane.SelectedIndexChanged +=
                      //  new System.EventHandler(Stadioane_SelectedIndexChanged);
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiilor desfasurate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.UN == "Timotei")
            {
                MeniuStergere ms = new MeniuStergere();
                ms.Show();

                this.Close();
            }
            else
            {
                MeniuStergereUser msu = new MeniuStergereUser();
                msu.Show();

                this.Close();
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
                    var den = cBDen.Text;
                    string query = "DELETE FROM Stadioane WHERE Denumire = '" + den + "';";
                    SqlCommand sterg = new SqlCommand(query, con);
                    sterg.ExecuteNonQuery();

                    
                    string query2 = "SELECT * FROM Stadioane WHERE Denumire = '" + den + "';";
                    SqlCommand comTest = new SqlCommand(query2, con);

                    SqlDataReader dr = comTest.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Nu s-a putut sterge inregistrarea!");
                    }

                    else MessageBox.Show("Inregistrare stearsa!");                  
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma stergerii!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void StergereStadioane_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
