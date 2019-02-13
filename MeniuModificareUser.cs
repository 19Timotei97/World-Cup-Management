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
    public partial class MeniuModificareUser : Form
    {
        public MeniuModificareUser()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MeniuUser mu = new MeniuUser();
            mu.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModificareAntrenori ma = new ModificareAntrenori();
            ma.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModificareJucatori mj = new ModificareJucatori();
            mj.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ModificareStadioane ms = new ModificareStadioane();
            ms.Show();

            this.Hide();
        }
    }
}
