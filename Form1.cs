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
    public partial class Form1 : Form
    {
        public string sqlCon = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=CampionatFotbal;Integrated Security=True";

        public Form1()
        {
            SqlConnection con = new SqlConnection(sqlCon);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // deschid o conexiune catre baza de date
            SqlConnection con = new SqlConnection(sqlCon);
            con.Open();

            // daca s-a conectat
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = "INSERT INTO Test(nume) VALUES ('" + numebox.Text.ToString() + "')";
                
                // execut comanda
                SqlCommand sqlCom = new SqlCommand(query, con);
                sqlCom.ExecuteNonQuery();
                
                // testez comanda
                MessageBox.Show("Connection was successfull");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
