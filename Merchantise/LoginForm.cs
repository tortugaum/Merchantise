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

namespace Merchantise
{
    public partial class LoginForm : Form
    {
        DBConnection dbcon = new DBConnection();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.DarkRed;
        }

        private void label_clear_MouseEnter(object sender, EventArgs e)
        {
            label_clear.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        private void label_clear_MouseLeave(object sender, EventArgs e)
        {
            label_clear.Font = new Font("Segoe UI", 14, FontStyle.Regular);
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_login_Click(object sender, EventArgs e)
        {

        }

        private void label_clear_Click(object sender, EventArgs e)
        {
            TextBox_username.Clear();
            TextBox_password.Clear();
            TextBox_username.Focus();
        }

        private void Button_login_Click(object sender, EventArgs e)
        {
            if(comboBox_role.SelectedItem.ToString() == "Admin")
            {
                if(TextBox_username.Text == "Admin" || TextBox_password.Text == "Admin123")
                {
                    ProductForm product = new ProductForm();
                    product.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong username or password, try again", "Wrong account information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                string selectQuery = "SELECT * FROM Seller WHERE SellerName = '" + TextBox_username.Text + "' AND SellerPassword = '"+ TextBox_password.Text + "'";
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, dbcon.GetCon());
                adapter.Fill(table);
                if(table.Rows.Count > 0)
                {
                    SellingForm selling = new SellingForm();
                    selling.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong username or password, try again", "Wrong account information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
    }
}
