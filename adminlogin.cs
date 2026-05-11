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
    public partial class adminlogin : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        public adminlogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("select * from manageadmin where name ='" + textBox1.Text + "' and password='" + textBox2.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Login success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                adminpannel ap = new adminpannel();
                ap.Show();
            }
            else
            {
                MessageBox.Show("failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
            
             
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            WEBSITE ww = new WEBSITE();
            ww.Show();
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
    }
}
