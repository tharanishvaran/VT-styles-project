using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VT_STYLES
{
    public partial class mensoneside : Form
    {
        public mensoneside()
        {
            InitializeComponent();
            comboBox1.Items.Add("BOX HAIRCUT");
            comboBox1.Items.Add("LONG HAIRCUT");
            comboBox1.Items.Add("MULLET HAIRCUT");
            comboBox1.Items.Add("BEARD GROOMING");
            comboBox1.Items.Add("MUSTACHE STYLING");
            comboBox1.Items.Add("ONESIDE HAIRCUT");
            comboBox1.Items.Add("SPIKE HAIRCUT");
            comboBox1.Items.Add("CLEAN SHAVE");
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "BOX HAIRCUT":
                    men m = new men();
                    m.Show();
                    break;

                case "LONG HAIRCUT":
                    menlonghair ml = new menlonghair();
                    ml.Show();
                    break;

                case "MULLET HAIRCUT":
                    mensmullet mm = new mensmullet();
                    mm.Show();
                    break;

                case "BEARD GROOMING":
                    beard b = new beard();
                    b.Show();
                    break;

                case "MUSTACHE STYLING":
                    mensmustache ms = new mensmustache();
                    ms.Show();
                    break;

                case "ONESIDE HAIRCUT":
                    mensoneside mo = new mensoneside();
                    mo.Show();
                    break;

                case "SPIKE HAIRCUT":
                    mensspike s = new mensspike();
                    s.Show();
                    break;

                case "CLEAN SHAVE":
                    mencleanshave cs = new mencleanshave();
                    cs.Show();
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string style = "ONESIDE HAIRCUT";
            int price = 250;
            booking bb = new booking(style, price);
            bb.Show();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            WEBSITE w = new WEBSITE();
            w.Show();
            this.Close();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            stylists st = new stylists();
            st.Show();
            this.Close();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            adminlogin al = new adminlogin();
            al.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            fulloneside fso = new fulloneside();
            fso.Show();
        }
    }
}
