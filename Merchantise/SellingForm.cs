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
using DGVPrinterHelper;

namespace Merchantise
{
    public partial class SellingForm : Form
    {

        DBConnection dbcon = new DBConnection();
        DGVPrinter printer = new DGVPrinter();
        public SellingForm()
        {
            InitializeComponent();
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
        }

        private void getProduct()
        {
            string selectQuery = "SELECT ProdName, ProdPrice FROM Product";
            SqlCommand command = new SqlCommand(selectQuery, dbcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_product.DataSource = table;

        }

        private void getSelllist()
        {
            string selectQuery = "SELECT * FROM Bill";
            SqlCommand command = new SqlCommand(selectQuery, dbcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_sell_list.DataSource = table;

        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            label_date.Text = DateTime.Today.ToShortDateString();
            getcategory();
            getProduct();
            getSelllist();
        }

        private void DataGridView_product_Click(object sender, EventArgs e)
        {
            TextBox_name.Text = DataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_price.Text = DataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
        }

        int grandTotal = 0, n = 0;

        private void Button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Bill VALUES(" + TextBox_id.Text + ", '" + label_seller.Text + "', '" + label_date.Text + "', " + grandTotal.ToString() + " )";
                SqlCommand command = new SqlCommand(insertQuery, dbcon.GetCon());
                dbcon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Order Added Successfully", "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbcon.CloseCon();
                getSelllist();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_print_Click(object sender, EventArgs e)
        {
            //We need DGVPrinter helper
            printer.Title = "Merchantise Sell List";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Merchantise Tortuga";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_sell_list);
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

        private void Button_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            this.Hide();
            login.Show();
        }

        private void Button_logout_MouseEnter(object sender, EventArgs e)
        {
            Button_logout.ForeColor = Color.Gold;
        }

        private void Button_logout_MouseLeave(object sender, EventArgs e)
        {
            Button_logout.ForeColor = Color.Goldenrod;
        }

        private void Button_addorder_Click(object sender, EventArgs e)
        {
            if(TextBox_name.Text == "" || TextBox_qty.Text == "")
            {
                MessageBox.Show("Missing information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int Total = Convert.ToInt32(TextBox_price.Text) * Convert.ToInt32(TextBox_qty.Text);
                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(DataGridView_order);
                addRow.Cells[0].Value =++n;
                addRow.Cells[1].Value =TextBox_name.Text;
                addRow.Cells[2].Value =TextBox_price.Text;
                addRow.Cells[3].Value =TextBox_qty.Text;
                addRow.Cells[4].Value = Total;
                DataGridView_order.Rows.Add(addRow);
                grandTotal += Total;
                label_amount.Text = grandTotal + "$";
            }
        }
    }
}
