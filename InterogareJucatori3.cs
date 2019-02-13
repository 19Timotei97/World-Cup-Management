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
    public partial class InterogareJucatori3 : Form
    {
        public InterogareJucatori3()
        {
            InitializeComponent();
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

                if(con.State == ConnectionState.Open)
                {
                    var meci = txtMeci.Text.ToString();
                    if (!Regex.IsMatch(meci, "^\\d+$"))
                        MessageBox.Show("Introduceti doar cifre si nu alte caractere!");

                    else
                    {
                        string query = "SELECT J.Nume, Prenume, Data_N AS 'Data nașterii', E.Nume AS 'Echipa', J.Pozitie" +
                            " FROM Jucatori J, Echipe E " +
                            "WHERE EXISTS (SELECT J.ID_Ech FROM Meciuri M " +
                            "WHERE J.ID_Ech = M.ID_Ech1 OR J.ID_Ech = M.ID_Ech2 " +
                            "HAVING COUNT(*) > " + meci + ") AND J.ID_Ech = E.ID_Ech" +
                            " GROUP BY J.Nume, Prenume, Data_N, E.Nume, J.Pozitie;";


                        SqlDataAdapter sda = new SqlDataAdapter();
                        SqlCommand com = new SqlCommand(query, con);

                        sda.SelectCommand = com;

                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        BindingSource bs = new BindingSource
                        { DataSource = dt };

                        DGW.DataSource = bs;

                        sda.Update(dt);
                    }
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiilor!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            



        }
    }
}
