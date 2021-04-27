﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class DBConnection
    {
        private static SqlConnection conn = null;

        public static SqlConnection getConnection()
        {
            try
            {
                if (conn == null)
                {
                    string con = @"Server = LAPTOP-06TGPAMU\SQLEXPRESS; Database = Final-Project-c#; Integrated Security=True";
                    conn = new SqlConnection(con);
                    conn.Open();                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
            return conn;
        }

        public static void insertCustomer(Customer customer)
        {
           
            try
            {
                conn = getConnection();
                string query = "INSERT INTO Customers (LastName,FirstName,Address,City,State,Zip,Email,UserName,Passward) VALUES (@LastName ,@FirstName ,@Address , @City , @State , @zip , @Email , @UserName, @passward)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@City", customer.City);
                cmd.Parameters.AddWithValue("@State", customer.State);
                cmd.Parameters.AddWithValue("@zip", customer.Zip);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@UserName", customer.UserName);
                cmd.Parameters.AddWithValue("@passward", customer.Passward);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message);
            }
        }

        public static void updateBookQty(int qty , int BookID)
        {
            try
            {   
                getConnection();
                string query = " UPDATE Books SET qty = qty - @qty Where BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@BookID", BookID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e.Message);
            }
        }

        public static void bookingBook(int customerID , int bookID , int qty , DateTime date)
        {
            try
            {
                getConnection();
                string query = "Insert into Orders (customer#, BookID , qty , DateOfOrder) VALUES (@customerID , @BookID, @Qty,@DofOrder)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerID", customerID);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                cmd.Parameters.AddWithValue("@Qty", qty);
                cmd.Parameters.AddWithValue("@DofOrder", date);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e.Message);
            }
        }

        public static List<Customer> getCustomer(string username , string passward)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                conn = getConnection();
                string query = "Select * from Customers where UserName = @username and Passward = @passward";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@passward", passward);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Customer customer = new Customer(sdr.GetInt32(sdr.GetOrdinal("Customer#")), sdr["LastName"].ToString(), sdr["FirstName"].ToString(), sdr["Address"].ToString(),
                            sdr["City"].ToString(), sdr["State"].ToString(), sdr["zip"].ToString(), sdr["Email"].ToString(), sdr["UserName"].ToString(),
                            sdr["Passward"].ToString());
                        customers.Add(customer);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return customers;
        }

        public static List<string> getCategorie()
        {
            List<string> categories = new List<string>();
            try
            {
                conn = getConnection();
                string query = "select Category from Category";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        categories.Add(sdr["Category"].ToString());
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error", e1.Message);
            }

            return categories;
        }

        public static void insertBook(Book book)
        {
            try
            {
                getConnection();
                string query = "INSERT INTO dbo.Books VALUES (@ISBN,@Book_Title, @Category,@Descreption , @Book_Name , @qty)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmd.Parameters.AddWithValue("@Book_Title", book.Title);
                cmd.Parameters.AddWithValue("@Category", book.Category);
                cmd.Parameters.AddWithValue("@Descreption", book.Descreption);
                cmd.Parameters.AddWithValue("@Book_Name", book.BookName);
                cmd.Parameters.AddWithValue("@qty", book.Qty);
                cmd.ExecuteNonQuery();
            }catch(Exception e)
            {
                MessageBox.Show("Error" + e.Message);
            }
        }
        public static void updateBook(Book book)
        {
            getConnection();
            string query = "Update dbo.Books set Title = @Book_Title , Category = @Category, Descreption = @Descreption , Book_Name = @Book_Name , qty = @qty WHERE ISBN = @ISBN";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Book_Title", book.Title);
            cmd.Parameters.AddWithValue("@Descreption", book.Descreption);
            cmd.Parameters.AddWithValue("@Category", book.Category);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@Book_Name", book.BookName);
            cmd.Parameters.AddWithValue("@qty", book.Qty);
            cmd.ExecuteNonQuery();
        }
        public static List<Book> findBook(string isbn)
        {
               List<Book> books = new List<Book>();
            try
            {
                conn = getConnection();
                string query = "select * from Books where ISBN = @ISBN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    Book book = new Book(sdr.GetInt32(sdr.GetOrdinal("BookID")),sdr["Title"].ToString(), sdr["category"].ToString() , sdr["Descreption"].ToString(), sdr["ISBN"].ToString() , Convert.ToInt32(sdr["qty"]), sdr["Book_Name"].ToString());
                    books.Add(book);
                }
            }
            catch (Exception e1)
            {
                 MessageBox.Show("Error" , e1.Message);
            }

            return books;
        }

        public static List<Book> DisplayAllBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                conn = getConnection();
                string query = "select * from Books";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Book book = new Book(sdr.GetInt32(sdr.GetOrdinal("BookID")),sdr["Title"].ToString(), sdr["category"].ToString(), sdr["Descreption"].ToString(), sdr["ISBN"].ToString(), Convert.ToInt32(sdr["qty"]), sdr["Book_Name"].ToString());
                        books.Add(book);
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error", e1.Message);
            }

            return books;
        }

        public static int orderQty(int customerID)
        {
            int qty = 0;
            try
            {
                conn = getConnection();
                string query = "select count(OrderID) as 'orders' from Orders where Customer# = @customerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerID", customerID);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        qty = sdr.GetInt32(sdr.GetOrdinal("orders"));
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error", e1.Message);
            }
            return qty;
        }
    }
  
}
