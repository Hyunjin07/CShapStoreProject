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
            string username = UserTb.Text;  // UserTb는 사용자 이름 입력 텍스트 박스
            string password = PasswordTb.Text;  // PasswordTb는 비밀번호 입력 텍스트 박스

         
            if (username == "Hyunjin" && password == "123456")
            {
                // 조건에 맞는 사용자인 경우 Home 폼으로 이동
                Homes homeForm = new Homes();
                homeForm.Show();
                this.Hide();
            }
            else
            {
                // 사용자 이름이나 비밀번호가 틀린 경우 경고 메시지 표시
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
