using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;



namespace VT_STYLES
{
    public partial class app : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        private string connectionString = @"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True";
        
  
        public app()
        {
            InitializeComponent();
            loadappointments();
            loadacceptappointments();
        }

        private void loadacceptappointments()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT name, phoneno, gender, date, email, style, totalprice FROM ACCEPT";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind DataTable to DataGridView2
                    dataGridView2.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accepted appointments: " + ex.Message);
            }
        }

        private void loadappointments()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT name, phoneno, gender, date, email, style, totalprice FROM APPOINTMENT";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind DataTable to DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string query = "select * from appointment";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter dataadapter = new SqlDataAdapter(query, connection);
                DataTable datatable = new DataTable();
                dataadapter.Fill(datatable);
                dataGridView1.DataSource = datatable;
                
            }
        }
        private bool ValidateUserLogin(string phoneno, string password)
{
    string connectionString = @"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        try
        {
            connection.Open();

            string query = "SELECT COUNT(*) FROM login WHERE phoneno = @phoneno AND password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            // Ensure both parameters are provided and match the expected types
            command.Parameters.AddWithValue("@phoneno", phoneno);
            command.Parameters.AddWithValue("@Password", password);

            int userCount = (int)command.ExecuteScalar();

            return userCount > 0;
        }
        catch (SqlException ex)
        {
            MessageBox.Show("db error"+ex.Message);
            return false;
        }
    }
}


      

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Get the phoneno from the TextBox
                string phonenumber = textBox3.Text.Trim();

                if (string.IsNullOrEmpty(phonenumber))
                {
                    MessageBox.Show("Please enter a phone number.");
                    return;
                }

                // Step 1: Fetch the row from APPOINTMENTS table using phoneno
                string selectQuery = "SELECT name, phoneno, gender, date, email, style, totalprice FROM APPOINTMENT WHERE phoneno = @phoneno";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch the row
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@phoneno", phonenumber);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string gender = reader["gender"].ToString();
                                DateTime date = Convert.ToDateTime(reader["date"]);
                                string email = reader["email"].ToString();
                                string style = reader["style"].ToString();
                                string totalprice = reader["totalprice"].ToString();
                                reader.Close();
                                // Step 2: Insert the row into ACCEPT table
                                string insertQuery = @"
                                    INSERT INTO ACCEPT (name, phoneno, gender, date, email, style, totalprice)
                                    VALUES (@name, @phoneno, @gender, @date, @email, @style, @totalprice)";

                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@name", name);
                                    insertCommand.Parameters.AddWithValue("@phoneno", phonenumber);
                                    insertCommand.Parameters.AddWithValue("@gender", gender);
                                    insertCommand.Parameters.AddWithValue("@date", date);
                                    insertCommand.Parameters.AddWithValue("@email", email);
                                    insertCommand.Parameters.AddWithValue("@style", style);
                                    insertCommand.Parameters.AddWithValue("@totalprice", totalprice);

                                    int rowsAffected = insertCommand.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        string deletequery = "delete from appointment where phoneno=@phoneno";
                                        using (SqlCommand deletecommand = new SqlCommand(deletequery, connection))
                                        {
                                            deletecommand.Parameters.AddWithValue("@phoneno", phonenumber);
                                            int deleteRowsaffected = deletecommand.ExecuteNonQuery();
                                            if (deleteRowsaffected > 0)
                                            {

                                                MessageBox.Show("Appointment Moved sucessfully !");

                                                loadacceptappointments();// Refresh DataGridView2

                                            }
                                            else
                                            {
                                                MessageBox.Show("failed to delete original appointment");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to accept appointment.");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Appointment not found.");
                            }
                        }
                    }
                }

                

            }



            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string phoneno = textBox1.Text; // Assuming the phone number is entered in a TextBox named txtPhoneNo

            if (string.IsNullOrEmpty(phoneno))
            {
                MessageBox.Show("Please enter a phone number.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to delete the row with the specified phone number
                    string deleteQuery = "DELETE FROM accept WHERE phoneno = @phoneno";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        // Add the phone number parameter
                        deleteCommand.Parameters.AddWithValue("@phoneno", phoneno);

                        // Execute the delete command
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment deleted successfully!");
                            LoadAppointments(); // Refresh the DataGridView or any UI component
                        }
                        else
                        {
                            MessageBox.Show("No appointment found with the provided phone number.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while deleting the appointment: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }
        private void LoadAppointments()
        {
            // Implement this method to refresh the UI (e.g., reload DataGridView)
            // Example:
            // dataGridView1.DataSource = GetAppointmentsFromDatabase();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = "select * from accept";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter dataadapter = new SqlDataAdapter(query, connection);
                DataTable datatable = new DataTable();
                dataadapter.Fill(datatable);
                dataGridView2.DataSource = datatable;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            string phonenumber = textBox3.Text; // Assuming the phone number is entered in a TextBox named txtPhoneNo

            if (string.IsNullOrEmpty(phonenumber))
            {
                MessageBox.Show("Please enter a phone number.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to delete the row with the specified phone number
                    string deleteQuery = "DELETE FROM appointment WHERE phoneno = @phoneno";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        // Add the phone number parameter
                        deleteCommand.Parameters.AddWithValue("@phoneno", phonenumber);

                        // Execute the delete command
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            
                            MessageBox.Show("APPOINTMENT DELETED SUCCESSFULLY","deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAppointments(); // Refresh the DataGridView or any UI component
                        }
                        else
                        {
                            MessageBox.Show("No appointment found with the provided phone number.","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while deleting the appointment: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            adminpannel ap = new adminpannel();
            ap.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            product p = new product();
            p.Show();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            manageadmin ma = new manageadmin();
            ma.Show();
            this.Close();

        }

        private void label6_Click(object sender, EventArgs e)
        {
            adminlogin al = new adminlogin();
            al.Show();
            this.Close();

        }



        

        private void app_Load(object sender, EventArgs e)
        {
            calculatetotalprice();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

       

        private void button6_Click(object sender, EventArgs e)
        {
            temp tt = new temp();
            tt.Show();
        }


        private void calculatetotalprice()
        {
            double total = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["totalprice"].Value != null)
                {
                    double price;
                    if (double.TryParse(row.Cells["totalprice"].Value.ToString(), out price))
                    {
                        total += price;
                    }
                }
            }
            label12.Text = total.ToString("0.00");
        }
        private void dataGridview2_cellvalueChanged(object sender, DataGridViewCellEventArgs e)
        {
            calculatetotalprice();
        }



    }
}
       

    




     
     
    

        
        

       
        

       
    

