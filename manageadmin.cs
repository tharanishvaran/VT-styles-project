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
    public partial class manageadmin : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
       
        
        public manageadmin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            adminpannel ad = new adminpannel();
            ad.Show();
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
            adminlogin ad = new adminlogin();
            ad.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
            con.Open();
            if (textBox4.Text == textBox5.Text)
            {
                cmd = new SqlCommand("insert into manageadmin values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox2.Text + "','" + textBox5.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("inserted successfully");
                con.Close();
            }
            else
            {
                MessageBox.Show("password mismatched");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("update manageadmin set name='" + textBox2.Text + "',phoneno='" + textBox3.Text + "',password='" + textBox4.Text + "' ,gender='"+comboBox2.Text+"',confirmpassword='"+textBox5.Text+"' where id='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("SUCCESSFULLY UPDATED");
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("delete manageadmin where id='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("deleted successfully");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");

            con.Open();
            cmd = new SqlCommand("select * from manageadmin", con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();

        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            adminpannel ap = new adminpannel();
            ap.Show();
            this.Close();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            app a = new app();
            a.Show();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            product p = new product();
            p.Show();
            this.Close();
        }

        private void manageadmin_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            adminlogin al = new adminlogin();
            al.Show();
        }

    }
}