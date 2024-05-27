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
                        string query = "INSERT INTO ProductTbl (PrName, PrCat, PrQty, PrPrice) VALUES (@PA, @PC, @PQ, @PP)";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            cmd.Parameters.AddWithValue("@PA", PrNameTb.Text); // 데이터베이스에 값을 추가
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
    }
}
