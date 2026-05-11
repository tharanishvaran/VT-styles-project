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
    public partial class adminpannel : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        
        public adminpannel()
        {
            InitializeComponent();
        }

        

        private void adminpannel_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=tharanishvaran\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("insert into employe values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+comboBox2.Text+"','"+textBox4.Text+"','"+textBox5.Text+"','"+textBox6.Text+"')",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("insert successfully");
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select * from employe",con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("delete employe where id='"+textBox1.Text+"'",con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("deleted successfully");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("update employe set name='"+textBox2.Text+"',phoneno='"+textBox3.Text+"',gender='"+comboBox2.Text+"',salary='"+textBox4.Text+"',address='"+textBox5.Text+"',email='"+textBox6.Text+"' where id='"+textBox1.Text+"'",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("SUCCESSFULLY UPDATED");
            con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            app a = new app();
            a.Show();
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
            adminlogin a = new adminlogin();
            a.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
        
    }
}
