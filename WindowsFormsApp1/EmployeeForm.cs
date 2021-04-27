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

namespace WindowsFormsApp1
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }
        

       

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtBookID_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Book book = new Book(txtTitle.Text, cmboCategory.Text, txtDescription.Text, txtBookID.Text , Convert.ToInt32(numQty.Value) , txtName.Text);
            DBConnection.updateBook(book);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Book book = new Book(txtTitle.Text, cmboCategory.Text, txtDescription.Text, txtBookID.Text, Convert.ToInt32(numQty.Value), txtName.Text);
            DBConnection.insertBook(book);
            MessageBox.Show("Book added successfully");

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            List<Book> books = DBConnection.findBook(txtSearchBook.Text);
            foreach (var b in books)
            {
                rchDispaly.Text = b.GetDisplayText(" - ");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
             cmboCategory.Items.AddRange(DBConnection.getCategorie().ToArray());
            cmboCategory.SelectedIndex = 0;
           
            
        }

        private void cmboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnDisplayAll_Click(object sender, EventArgs e)
        {
            rchDispaly.Text = "";
            List<Book> books = DBConnection.DisplayAllBooks();
            foreach (var item in books)
            {
                rchDispaly.Text +=item.GetDisplayText(" , ");
                rchDispaly.Text+="\n";


            }

        }

        private void btnFindBook_Click(object sender, EventArgs e)
        {
        
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
