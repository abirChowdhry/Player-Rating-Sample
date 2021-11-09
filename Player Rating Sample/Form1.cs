using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Player_Rating_Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            connection.Open();

            string sql = "Insert into RATINGS(NAMe,prerate) values('" + NametextBox.Text + "','"+comboBox1.Text+"') ";
            SqlCommand command = new SqlCommand(sql, connection);

            int flag = command.ExecuteNonQuery();

            if (flag == 1) MessageBox.Show("Added");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            connection.Open();

            string sql = "select * from ratings where name = '"+textBox3.Text+"' ";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read()) 
            {
                textBox3.Text = reader["name"].ToString();
                comboBox3.Text = reader["prerate"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
       
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            connection.Open();


            string sql = "Update ratings set prerate = @prerate where name = '" + textBox3.Text + "'";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("rating", ((Convert.ToSingle(comboBox2.Text) + Convert.ToSingle(comboBox3.Text))/2));
            command.Parameters.AddWithValue("prerate", ((Convert.ToSingle(comboBox2.Text) + Convert.ToSingle(comboBox3.Text))/2));

            int flag = command.ExecuteNonQuery();
            connection.Close();

            if (flag == 1) MessageBox.Show("UPDATED");
        }

        private void NametextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) 
            {
                e.Handled = true;
                MessageBox.Show("Only Numbers or Digits");
            }
        }
    }
}
