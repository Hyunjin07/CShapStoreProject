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
using System.Xml.Linq;

namespace Store
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            DisPlayProduct();
        }

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ca970\Documents\ShopDb.mdf;Integrated Security=True;Connect Timeout=30";

        private void DisPlayProduct()
        {
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                try
                {
                    Con.Open();
                    string Query = "Select * from ProductTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                    SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    ProductDGV.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Clear()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            CatCb.SelectedIndex = -1;
            Key = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
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
                        string query = "INSERT INTO ProductTbl (PrName, PrCat, PrQty, PrPrice) VALUES (@PN, @PC, @PQ, @PP)";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@PN", PrNameTb.Text); // 데이터베이스에 값을 추가
                            cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString()); // 카테고리 값을 문자열로 저장
                            cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                            cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Product Added");
                            Con.Close();
                            DisPlayProduct();
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

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProductDGV.SelectedRows.Count > 0) // Ensure the clicked cell is a valid row
            {
                PrNameTb.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
                CatCb.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
                QtyTb.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
                PriceTb.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
                Key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select A Product"); // 삭제할 제품을 선택하지 않았을 때 경고 메시지
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        string query = "DELETE FROM ProductTbl WHERE PrId=@PKey";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@PKey", Key); // 데이터베이스에서 값을 삭제
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Product Deleted");
                            Con.Close();
                            DisPlayProduct();
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

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
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
                        string query = "UPDATE ProductTbl SET PrName=@PN, PrCat=@PC, PrQty=@PQ, PrPrice=@PP WHERE PrId=@PKey";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@PN", PrNameTb.Text); // 데이터베이스에 값을 추가
                            cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString()); // 카테고리 값을 문자열로 저장
                            cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                            cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                            cmd.Parameters.AddWithValue("@PKey", Key);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Product Edited");
                            Con.Close();
                            DisPlayProduct();
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



        private void ClosePicture_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
        
            
                Program.IsLoggedIn = false;
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            
        }
    }
}
