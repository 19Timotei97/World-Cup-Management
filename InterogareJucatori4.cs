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
    public partial class InterogareJucatori4 : Form
    {
        public InterogareJucatori4()
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

        private void fillComboBox()
        {
            cBRasp.Items.Add("Da");
            cBRasp.Items.Add("Nu");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlCon);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    string query = "";

                    var rasp = cBRasp.Text;

                    if(rasp.Equals("Da"))
                        query += "SELECT J.Nume + ' ' + Prenume AS 'Nume jucator', " +
                        "(SELECT COUNT(*) FROM Goluri WHERE Goluri.ID_Juc = J.ID_Juc) AS 'Goluri marcate'," +
                        " J.Pozitie, Data_N AS 'Data nașterii', E.Nume AS 'Echipa' " +
                        "FROM Jucatori J, Echipe E WHERE J.ID_Ech = E.ID_Ech " +
                        "ORDER BY(SELECT COUNT(*) FROM Meciuri WHERE J.ID_Ech = ID_Ech1" +
                        " OR J.ID_Ech = ID_Ech2)";
                    else
                        query += "SELECT J.Nume + ' ' + Prenume AS 'Nume jucator', " +
                        "(SELECT COUNT(*) FROM Goluri WHERE Goluri.ID_Juc = J.ID_Juc) AS 'Goluri marcate'," +
                        " J.Pozitie, Data_N AS 'Data nașterii', E.Nume AS Echipa " +
                        "FROM Jucatori J, Echipe E WHERE J.ID_Ech = E.ID_Ech " +
                        "ORDER BY(SELECT COUNT(*) FROM Meciuri WHERE J.ID_Ech = ID_Ech1" +
                        " OR J.ID_Ech = ID_Ech2) DESC";

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
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma operatiilor!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InterogareJucatori4_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
