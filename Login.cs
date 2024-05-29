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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UserTb.Text;  
            string password = PasswordTb.Text; 

            if (username == "Hyunjin" && password == "123456")
            {

                Program.IsLoggedIn = true;
                Homes homesForm = new Homes();
                homesForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetLbl_Click(object sender, EventArgs e)
        {
            UserTb.Text = "";
            PasswordTb.Text = "";
        }

        private void ClosePicture_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
