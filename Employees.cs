using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Store
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            DisPlayEmployees(); // 폼이 로드될 때 직원 목록을 표시
        }

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ca970\Documents\ShopDb.mdf;Integrated Security=True;Connect Timeout=30";

        private void DisPlayEmployees()
        {
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    string Query = "Select * from EmployeeTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                    SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    EmployeesDGV.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpPhoneTb.Text = "";
            PasswordTb.Text = "";
            Key = 0;
        }

        int Key = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information"); // 필요한 정보를 모두 입력하지 않았을 때 경고 메시지
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        string query = "INSERT INTO EmployeeTbl (EmpName, EmpAdd, EmpDOB, EmpPhone, EmpPass) VALUES (@EN, @EA, @ED, @EP, @EPa)";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text); // 데이터베이스에 값을 추가
                            cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                            cmd.Parameters.AddWithValue("@ED", EmpDOB.Value); // assuming EmpDOB is a DateTimePicker
                            cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                            cmd.Parameters.AddWithValue("@EPa", PasswordTb.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Employee Added");
                        }
                    }
                    DisPlayEmployees();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmployeesDGV.SelectedRows.Count > 0)
            {
                EmpNameTb.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
                EmpAddTb.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
                EmpDOB.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
                EmpPhoneTb.Text = EmployeesDGV.SelectedRows[0].Cells[4].Value.ToString();
                PasswordTb.Text = EmployeesDGV.SelectedRows[0].Cells[5].Value.ToString();
                Key = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information"); // 필요한 정보를 모두 입력하지 않았을 때 경고 메시지
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        string query = "UPDATE EmployeeTbl SET EmpName=@EN, EmpAdd=@EA, EmpDOB=@ED, EmpPhone=@EP, EmpPass=@EPa WHERE EmpNum=@EKey";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text); // 데이터베이스에 값을 추가
                            cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                            cmd.Parameters.AddWithValue("@ED", EmpDOB.Value); // assuming EmpDOB is a DateTimePicker
                            cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                            cmd.Parameters.AddWithValue("@EPa", PasswordTb.Text);
                            cmd.Parameters.AddWithValue("@EKey", Key);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Employee Updated");
                            Con.Close();
                            DisPlayEmployees();
                            Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select An Employee"); // 삭제할 직원을 선택하지 않았을 때 경고 메시지
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        string query = "DELETE FROM EmployeeTbl WHERE EmpNum=@EmpKey";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@EmpKey", Key); // 데이터베이스에서 값을 삭제
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Employee Deleted");
                            Con.Close();
                            DisPlayEmployees();
                            Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //private void label4_Click(object sender, EventArgs e)
        //{
        //    Customer Obj = new Customer();
        //    Obj.Show();
        //    this.Hide();
        //}

        //private void label1_Click(object sender, EventArgs e)
        //{
        //    Products Obj = new Products();
        //    Obj.Show();
        //    this.Hide();
        //}
        private void HomeLbl_Click(object sender, EventArgs e)
        {
            Homes Obj = new Homes();
            Obj.Show();
            this.Hide();
        }

        private void ProLbl_Click(object sender, EventArgs e)
        {
            Products Obj = new Products();
            Obj.Show();
            this.Hide();
        }
        private void CusLbl_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }
        private void EmpLbl_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }
      

        private void BilLbl_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void CLosePicture_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
    }
}
