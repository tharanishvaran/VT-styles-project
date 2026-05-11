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
    public partial class NEW_USER : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        
         public NEW_USER()
        {
            InitializeComponent();
        }

        private void NEW_USER_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
            {

                MessageBox.Show("Fill The Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string email = textBox4.Text;
            string emailpattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailpattern))
            {
                MessageBox.Show("in correct email", "failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            string phonenumber = textBox3.Text;
            string phonepattern = @"^\+?[0-9]{10,15}$";
            if (!Regex.IsMatch(phonenumber, phonepattern))
            {
                MessageBox.Show("in correct phone number", "failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
             if (textBox2.Text == textBox6.Text)
            {
                con.Open();
                cmd = new SqlCommand("Insert into login values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
            else
            {
                MessageBox.Show("Check password","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
