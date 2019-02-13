using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CampionatFotbal
{
    public partial class MeniuInserareUser : Form
    {
        public MeniuInserareUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InserareAntrenori ina = new InserareAntrenori();
            ina.Show();

            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MeniuUser mu = new MeniuUser();
            mu.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InserareStadioane inst = new InserareStadioane();
            inst.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InserareJucatori injuc = new InserareJucatori();
            injuc.Show();

            this.Hide();
        }
    }
}
