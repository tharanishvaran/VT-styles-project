using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace VT_STYLES
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select * from login where uname ='"+textBox1.Text+"' and password='"+textBox2.Text+"'",con);
            dr = cmd.ExecuteReader();
            if (string.IsNullOrEmpty(textBox1.Text))
            {

                MessageBox.Show("Fill The Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dr.Read())
            {
                MessageBox.Show("success","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                WEBSITE WS = new WEBSITE();
                WS.Show();
                string phoneNumber = textBox1.Text;
                string password = textBox1.Text;
            }
            
            else
            {
                MessageBox.Show("Invalid login. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            con.Close();
         
        }

        private void label3_Click(object sender, EventArgs e)
        {
            adminlogin all = new adminlogin();
            all.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NEW_USER NW= new NEW_USER();
            NW.Show();
        }
     
         
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        public object phoneno { get; set; }

       
    }
}
