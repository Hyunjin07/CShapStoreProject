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
    public partial class Homes : Form
    {
        private SqlConnection Con;

        public Homes()
        {
            InitializeComponent();
            Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ca970\Documents\ShopDb.mdf;Integrated Security=True;Connect Timeout=30");
            CountTshirts();
            CountPants();
            CountSkirts();
            Finance();
        }

        private void CountTshirts()
        {
            try
            {
                Con.Open();
                string Cat = "T-shirts";
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat = @Cat", Con);
                sda.SelectCommand.Parameters.AddWithValue("@Cat", Cat);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TshirtLbl.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void Finance()
        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Amt) from BillTbl", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    FinanceLbl.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    FinanceLbl.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }




        private void CountPants()
        {
            try
            {
                Con.Open();
                string Cat = "Pants";
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat = @Cat", Con);
                sda.SelectCommand.Parameters.AddWithValue("@Cat", Cat);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pantsLbl.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void CountSkirts()
        {
            try
            {
                Con.Open();
                string Cat = "Skirts";
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat = @Cat", Con);
                sda.SelectCommand.Parameters.AddWithValue("@Cat", Cat);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SkirtsLbl.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }


        private void CLosePicture_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Program.IsLoggedIn = false;
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
    }
}
