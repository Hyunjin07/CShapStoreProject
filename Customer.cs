using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            DisPlayCustomers();
        }

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ca970\Documents\ShopDb.mdf;Integrated Security=True;Connect Timeout=30";

        private void DisPlayCustomers()
        {
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    string Query = "Select * from CustomerTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                    SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    CustomerDGV.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Clear()
        {
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "")
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
                        string query = "INSERT INTO CustomerTbl (CustName, CustAdd, CustPhone) VALUES (@CN, @CA, @CP)";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@CN", CustNameTb.Text); // 데이터베이스에 값을 추가
                            cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                            cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Customer Added");
                            Con.Close();
                            DisPlayCustomers();
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

        int Key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
                CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
                CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
                CustPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
               
            if(CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
            
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select An Customer"); // 삭제할 직원을 선택하지 않았을 때 경고 메시지
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        string query = "DELETE FROM CustomerTbl WHERE CustId=@CKey";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@CKey", Key); // 데이터베이스에서 값을 삭제
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Customer Deleted");
                            Con.Close();
                            DisPlayCustomers();
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
    
    }
}

