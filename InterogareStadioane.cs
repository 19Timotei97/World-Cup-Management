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
    public partial class InterogareStadioane : Form
    {
        public InterogareStadioane()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            MeniuInterogareStadioane mi = new MeniuInterogareStadioane();
            mi.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlCon))
                {
                    string cond = txtGol.Text.ToString();


                    if (Regex.IsMatch(cond, "^\\d+$"))
                    {
                        int nr_gol = Int32.Parse(cond);

                        string query = "SELECT S.Denumire, S.Locatie, S.Capacitate, S.Cost, COUNT(ID_Gol) AS Goluri_marcate FROM Meciuri M " +
                            "INNER JOIN Stadioane S ON M.ID_Std = S.ID_Std " +
                            "INNER JOIN Goluri G ON G.ID_Meci = M.ID_Meci " +
                            "GROUP BY S.Denumire, S.Locatie, S.Capacitate, S.Cost " +
                            "HAVING COUNT(ID_Gol) >= " + nr_gol + ";";

                        SqlCommand com = new SqlCommand(query, con);

                        SqlDataAdapter da = new SqlDataAdapter();

                        da.SelectCommand = com;

                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        BindingSource bs = new BindingSource
                        {
                            DataSource = dt
                        };

                        DGW.DataSource = bs;

                        da.Update(dt);

                        com.Dispose();
                        da.Dispose();
                    }
                    else MessageBox.Show("Introduceti doar numere!", "Eroare la citirea conditiei!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare la afisarea rezultatelor!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
