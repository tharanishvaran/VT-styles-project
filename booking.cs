using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace VT_STYLES
{
    public partial class booking : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        // Dictionary to hold DateTime and booking count
        private Dictionary<DateTime, int> bookingSlots = new Dictionary<DateTime, int>();
        
       


        public booking(string receivedstyle, int receivedprice)
        {
            InitializeComponent();

            textBox2.Text = receivedstyle;
            textBox4.Text = receivedprice.ToString();
            double discount = receivedprice - (receivedprice * 0.15);
            textBox6.Text = discount.ToString("f2");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            stylists s = new stylists();
            s.Show();
            this.Close();

        }

        private void booking_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
            dateTimePicker1.MinDate = DateTime.Today;
            
        }



        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                DateTime selectedDateTime = dateTimePicker1.Value;
                DateTime startTime = selectedDateTime;
                DateTime endTime = selectedDateTime.AddMinutes(1); // Slot duration

                con.Open();

                // Check if the slot already has 2 or more bookings
                string checkQuery = "SELECT COUNT(*) FROM appointment WHERE date >= @startTime AND date < @endTime";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@startTime", startTime);
                    checkCmd.Parameters.AddWithValue("@endTime", endTime);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count >= 2)
                    {
                        MessageBox.Show("Slots filled for the selected time!", "Booking Full", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Proceed with insert
                string insertQuery = "INSERT INTO appointment(name, phoneno, gender, date, email, style, totalprice, status) " +
                                     "VALUES (@name, @phoneno, @gender, @date, @email, @style, @totalprice, 'BOOKED')";
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@phoneno", textBox3.Text);
                    cmd.Parameters.AddWithValue("@gender", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@date", selectedDateTime);
                    cmd.Parameters.AddWithValue("@email", textBox5.Text);
                    cmd.Parameters.AddWithValue("@style", textBox2.Text);
                    cmd.Parameters.AddWithValue("@totalprice", textBox6.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Successfully Submitted. We will send an email for confirmation.", "BOOKED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker timepicker = new DateTimePicker();
            timepicker.Format = DateTimePickerFormat.Time;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMMM yyyy - hh:mm tt";
            dateTimePicker1.ShowUpDown = true;



        }
        
    }
    
}
        
        


