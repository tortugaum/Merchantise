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
    public partial class ProductForm : Form
    {
        private DBConnection dbcon = new DBConnection();

        public ProductForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Button_category_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            getcategory();
            getTable();
        }

        private void getcategory()
        {
            string selectQuery = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuery, dbcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            comboBox_category.DataSource = table;
            comboBox_category.ValueMember = "CategoryName";
            comboBox_search.DataSource = table;
            comboBox_search.ValueMember = "CategoryName";
        }

        private void getTable()
        {
            string selectQuery = "SELECT * FROM Product";
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
            TextBox_price.Clear();
            TextBox_qty.Clear();
            comboBox_category.SelectedIndex = 0;
        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            try
            {            
                string insertQuery = "INSERT INTO Product VALUES("+TextBox_id.Text +", '" + TextBox_name.Text + "', "+ TextBox_price.Text + ", " + TextBox_qty.Text + ", '"+ comboBox_category.Text +"' )";
                SqlCommand command = new SqlCommand(insertQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbcon.CloseCon();
                getTable();
                clear();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if ((TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_price.Text == "" || TextBox_qty.Text == "" || comboBox_category.Text == ""))
                {
                    MessageBox.Show("Missing information", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string updateQuery = "UPDATE Product SET ProdName= '" + TextBox_name.Text + "' , ProdPrice= " + TextBox_price.Text + " , ProdQty= " + TextBox_qty.Text + " , ProdCat= '" + comboBox_category.Text + "' WHERE ProdId= " + TextBox_id.Text + " ";
                SqlCommand command = new SqlCommand(updateQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Product Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void DataGridView_product_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = DataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_price.Text = DataGridView_product.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_qty.Text = DataGridView_product.SelectedRows[0].Cells[3].Value.ToString();
            comboBox_category.Text = DataGridView_product.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.DarkRed;
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if(TextBox_id.Text == "")
                {
                    MessageBox.Show("ID is invalid", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string deleteQuery = "DELETE FROM Product WHERE ProdId = " + TextBox_id.Text + " ";
                SqlCommand command = new SqlCommand(deleteQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Product Deleted Successfully", "Deleted Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbcon.CloseCon();
                getTable();
                clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_refresh_Click(object sender, EventArgs e)
        {
            getTable();
            comboBox_search.SelectedIndex = 0;
        }

        private void comboBox_search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM Product WHERE ProdCat= '" + comboBox_search.SelectedValue.ToString() + "' ";
            Console.WriteLine(selectQuery);
            SqlCommand command = new SqlCommand(selectQuery, dbcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                DataGridView_product.DataSource = table;
            }catch(Exception ex)
            {
                MessageBox.Show("Data not found");
            }
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

        private void Button_seller_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }
    }
}
