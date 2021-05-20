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

namespace Merchantise
{
    public partial class CategoryForm : Form
    {
        DBConnection dbcon = new DBConnection();
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void getTable()
        {
            string selectQuery = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuery, dbcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_category.DataSource = table;

        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Category VALUES("+TextBox_id.Text+",'"+TextBox_name.Text+"','"+TextBox_description.Text+"')";
                SqlCommand command = new SqlCommand(insertQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully", "Add Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
                dbcon.CloseCon();
                getTable();
                clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void Button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if((TextBox_id.Text == "" || TextBox_name.Text=="" || TextBox_description.Text == ""))
                {
                    MessageBox.Show("Missing information", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string updateQuery = "UPDATE Category SET CategoryName='" + TextBox_name.Text + "' , CategoryDescription='" + TextBox_description.Text + "' WHERE CategoryId=" + TextBox_id.Text + " ";
                SqlCommand command = new SqlCommand(updateQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category Updated Successfully","Update Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbcon.CloseCon();
                getTable();
                clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView_category_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_category.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = DataGridView_category.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_description.Text = DataGridView_category.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_description.Clear();
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string deleteQuery = "DELETE FROM Category WHERE CategoryId=" + TextBox_id.Text + " ";
                SqlCommand command = new SqlCommand(deleteQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category Deleted Successfully", "Deleted Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbcon.CloseCon();
                getTable();
                clear();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            label_exit.ForeColor = Color.OrangeRed;
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

        private void Button_product_Click(object sender, EventArgs e)
        {
            ProductForm product = new ProductForm();
            product.Show();
            this.Hide();
        }

        private void Button_selling_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }

        private void Button_seller_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }
    }
}
