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
    public partial class SellerForm : Form
    {
        DBConnection dbcon = new DBConnection();
        public SellerForm()
        {
            InitializeComponent();
        }

        private void getTable()
        {
            string selectQuery = "SELECT * FROM Seller";
            SqlCommand command = new SqlCommand(selectQuery, dbcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_product.DataSource = table;

        }

        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_age.Clear();
            TextBox_password.Clear();
        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Seller VALUES(" + TextBox_id.Text + ", '" + TextBox_name.Text + "', " + TextBox_age.Text + ", '" + TextBox_password.Text + "' )";
                Console.WriteLine(insertQuery);
                SqlCommand command = new SqlCommand(insertQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbcon.CloseCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if ((TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_age.Text == "" || TextBox_password.Text == ""))
                {
                    MessageBox.Show("Missing information", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string updateQuery = "UPDATE Seller SET SellerName= '" + TextBox_name.Text + "' , SellerAge= " + TextBox_age.Text + " , SellerPassword= '" + TextBox_password.Text + "' WHERE SellerId= " + TextBox_id.Text + " ";
                SqlCommand command = new SqlCommand(updateQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Seller Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbcon.CloseCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "")
                {
                    MessageBox.Show("ID is invalid", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if((MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    string deleteQuery = "DELETE FROM Seller WHERE SellerId = " + TextBox_id.Text + " ";
                    SqlCommand command = new SqlCommand(deleteQuery, dbcon.GetCon());
                    dbcon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfully", "Deleted Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dbcon.CloseCon();
                    getTable();
                    clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.DarkRed;
        }

        private void DataGridView_product_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = DataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_age.Text = DataGridView_product.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_password.Text = DataGridView_product.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void Button_product_Click(object sender, EventArgs e)
        {
            ProductForm product = new ProductForm();
            product.Show();
            this.Hide();
        }

        private void Button_category_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        private void Button_logout_MouseEnter(object sender, EventArgs e)
        {
            Button_logout.ForeColor = Color.Gold;
        }

        private void Button_logout_MouseLeave(object sender, EventArgs e)
        {
            Button_logout.ForeColor = Color.Goldenrod;
        }

        private void Button_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void Button_selling_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }
    }
}
