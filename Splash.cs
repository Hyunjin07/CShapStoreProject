using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            timer1.Start();
        }
        int startP = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            Progressbar.Value = startP;
            PercentageLbl.Text = startP + "%";

            if (Progressbar.Value >= 100)
            {
                Progressbar.Value = 0;
                timer1.Stop();
                Login Obj = new Login();
                Obj.Show();
                this.Hide();
            }
        }
    }
}
