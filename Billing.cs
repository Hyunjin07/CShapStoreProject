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
using System.Drawing.Printing;

namespace Store
{
    public partial class Billing : Form
    {
        private SqlConnection Con;

        public Billing()
        {
            InitializeComponent();
            Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ca970\Documents\ShopDb.mdf;Integrated Security=True;Connect Timeout=30");
            GetCustomers();
            DisplayProduct();
            CustIdCb.SelectedIndexChanged += CustIdCb_SelectedIndexChanged; // 이벤트 핸들러 연결
            InitializeBillDGV(); // BillDGV 초기화
        }

        private void InitializeBillDGV()
        {
            BillDGV.ColumnCount = 5;
            BillDGV.Columns[0].Name = "ID";
            BillDGV.Columns[1].Name = "Product Name";
            BillDGV.Columns[2].Name = "Price";
            BillDGV.Columns[3].Name = "Quantity";
            BillDGV.Columns[4].Name = "Total";
        }

        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("SELECT CustId FROM CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(Rdr);
            CustIdCb.ValueMember = "CustId";
            CustIdCb.DataSource = dt;
            Con.Close();
        }

        private void DisplayProduct()
        {
            Con.Open();
            string Query = "SELECT * FROM ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GeCusName()
        {
            if (CustIdCb.SelectedValue != null)
            {
                Con.Open();
                string Query = "SELECT * FROM CustomerTbl WHERE CustId = @CustId";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@CustId", CustIdCb.SelectedValue.ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CustNameTb.Text = dr["CustName"].ToString();
                }
                Con.Close();
            }
        }

        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeCusName(); // 고객 이름을 가져오는 메서드 호출
        }

        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(QtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProductTbl SET PrQty = @PQ WHERE PrId = @PKey", Con);
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", Key);

                cmd.ExecuteNonQuery();
                Con.Close();
                DisplayProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }

        int n = 0, GrdTotal = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > Stock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PrPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;  // ID
                newRow.Cells[1].Value = PrNameTb.Text;  // Product Name
                newRow.Cells[2].Value = PrPriceTb.Text; // Price
                newRow.Cells[3].Value = QtyTb.Text;  // Quantity
                newRow.Cells[4].Value = total;  // Total
                BillDGV.Rows.Add(newRow);
                GrdTotal += total;
                n++;
                TotalLbl.Text = "Rs " + GrdTotal;
                UpdateStock();
                Reset();
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        int Key = 0;
        int Stock = 0;
        private void Reset()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            PrPriceTb.Text = "";
            Stock = 0;
            Key = 0;
        }

        private void InsertBill()
        {
            try
            {
                Con.Open();
                string query = "INSERT INTO BillTbl (BDate, EmpAdd, CustId, CustName, EmpName, Amt) VALUES (@BD, @CI, @CN, @EN, @Am)";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@EN", "EmpName"); // 현재 로그인된 직원 이름을 사용하십시오.
                cmd.Parameters.AddWithValue("@Am", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved");
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }

        int prodid, prodqty, prodprice, tottal, pos;
        string prodname;

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("MyCodeSpace Clothes Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            pos = 60;

            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null && row.Cells[4].Value != null)
                {
                    prodid = Convert.ToInt32(row.Cells[0].Value);
                    prodname = row.Cells[1].Value.ToString();
                    prodprice = Convert.ToInt32(row.Cells[2].Value);
                    prodqty = Convert.ToInt32(row.Cells[3].Value);
                    tottal = Convert.ToInt32(row.Cells[4].Value);
                    e.Graphics.DrawString(prodid.ToString(), new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                    e.Graphics.DrawString(prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                    e.Graphics.DrawString(prodprice.ToString(), new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                    e.Graphics.DrawString(prodqty.ToString(), new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                    e.Graphics.DrawString(tottal.ToString(), new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                    pos += 20;
                }
            }

            e.Graphics.DrawString("Grand Total: Rs" + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("********** Clothes shop **********", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill(); // BillTbl에 데이터 삽입
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < ProductsDGV.Rows.Count)
            {
                DataGridViewRow row = ProductsDGV.Rows[e.RowIndex];
                PrNameTb.Text = row.Cells[1].Value.ToString();
                Stock = Convert.ToInt32(row.Cells[3].Value);
                PrPriceTb.Text = row.Cells[4].Value.ToString();
                Key = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void ClosePicture_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();

            this.Hide();
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
    }
}
