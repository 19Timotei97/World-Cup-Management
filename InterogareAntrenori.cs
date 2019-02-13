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
    public partial class InterogareAntrenori : Form
    {
        public InterogareAntrenori()
        {
            InitializeComponent();
        }

        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        private void fillComboBox()
        {
            cBRasp.Items.Add("Da");
            cBRasp.Items.Add("Nu");
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
                    var rasp = cBRasp.Text;
                                       
                    string top = txtAntr.Text.ToString();

                    if (!Regex.IsMatch(top, "^\\d+$"))
                        MessageBox.Show("Introduceți un număr și nu alte caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    else {

                        string query = "";

                        if (rasp.Equals("Da"))
                            query += "SELECT A.Nume + ' ' +  A.Prenume AS 'Nume antrenor', A.[Data nașterii], A.Salariu " +
                                "FROM(SELECT TOP " + top + " A.Salariu AS Salariu, A.Nume AS Nume, A.Prenume AS Prenume, " +
                                "A.Data_N AS 'Data nașterii' " +
                                "FROM Antrenori A " +
                                "ORDER BY A.Salariu) AS A";

                        else
                            query += "SELECT A.Nume + ' ' +  A.Prenume AS 'Nume antrenor', A.[Data nașterii], A.Salariu " +
                                "FROM(SELECT TOP " + top + " A.Salariu AS Salariu, A.Nume AS Nume, A.Prenume AS Prenume, " +
                                "A.Data_N AS 'Data nașterii' " +
                                "FROM Antrenori A " +
                                "ORDER BY A.Salariu DESC) AS A";


                        SqlDataAdapter sqlDA = new SqlDataAdapter();

                        SqlCommand com = new SqlCommand(query, con);

                        sqlDA.SelectCommand = com;
                        DataTable dt = new DataTable();

                        sqlDA.Fill(dt);

                        BindingSource bs = new BindingSource
                        { DataSource = dt };

                        DGW.DataSource = bs;
                        sqlDA.Update(dt);
                        

                        sqlDA.Dispose();
                        com.Dispose();
                        con.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Eroare aparuta in urma interogării!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InterogareAntrenori_Load(object sender, EventArgs e)
        {
            fillComboBox();
        }
    }
}
