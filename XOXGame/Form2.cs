using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOXGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static int tursayisi=0;
        public static string ad1, ad2;
        public static bool oyunmodu = true; //true 2 kişilik, false, PC'ye karşı

        private void button1_Click(object sender, EventArgs e)
        {
            ad1 = textBox1.Text;
            ad2 = textBox2.Text;
            tursayisi = Convert.ToInt32(textBox3.Text);
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button1.Visible = false;
            pictureBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            button1.Visible = true;
            pictureBox1.Visible = true;
            textBox1.Text = "Bilgisayar";
            textBox1.Enabled=false;
            oyunmodu = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            button1.Visible = true;
            pictureBox1.Visible = true;
        }

    }
}
