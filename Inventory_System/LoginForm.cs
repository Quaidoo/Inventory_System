using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Inventory_System
{
    public partial class LoginForm : Form
    {

        //linking it to the Database
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\Mac\Home\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public LoginForm()
        {
            InitializeComponent();
        }


        //show Password 
        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == true)
                txtPass.UseSystemPasswordChar = false;
            else
                txtPass.UseSystemPasswordChar = true;
        }

        //fetching
        private void btnLogin_Click(object sender, EventArgs e)
           
        {
        
            try
            {
                cm = new SqlCommand("SELECT * FROM tbUser WHERE username=@username AND password=@password",con);
                cm.Parameters.AddWithValue("@username", txtName.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                con.Open();
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show("welcome " + dr["fullname"].ToString(),"ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mainForm main = new mainForm();
                    main.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Invalid username or password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                con.Close();




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            // this clears all context in the Username and Password textBox
            txtName.Clear();
            txtPass.Clear();
        }

        

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click_1(object sender, EventArgs e)
        {

            //When the close icon is clicked
            if (MessageBox.Show("Exit Applicaton", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}