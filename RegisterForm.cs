using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MySqlConnectivity
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void textBoxFirstName_Enter(object sender, EventArgs e)
        {
            String fname = textBoxFirstName.Text;
            if (fname.Equals("First Name"))
            {
                textBoxFirstName.Text = "";
                textBoxFirstName.ForeColor = Color.Black;
            }
        }

        private void textBoxFirstName_Leave(object sender, EventArgs e)
        {
            String fname = textBoxFirstName.Text;
            if (fname.Equals("First Name") || fname.Trim().Equals(""))
            {
                textBoxFirstName.Text = "First Name"; 
                textBoxFirstName.ForeColor = Color.Gray;
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }

        private void textBoxLastName_Enter(object sender, EventArgs e)
        {
            String lname = textBoxLastName.Text;
            if (lname.Equals("Last Name"))
            {
                textBoxLastName.Text = "";
                textBoxLastName.ForeColor = Color.Black;
            }
        }

        private void textBoxLastName_Leave(object sender, EventArgs e)
        {
            String lname = textBoxLastName.Text;
            if (lname.Equals("Last Name") || lname.Trim().Equals(""))
            {
                textBoxLastName.Text = "Last Name";
                textBoxLastName.ForeColor = Color.Gray;
            }
        }

        private void textBoxEmail_Enter(object sender, EventArgs e)
        {
            String email = textBoxEmail.Text;
            if (email.Equals("Email"))
            {
                textBoxEmail.Text = "";
                textBoxEmail.ForeColor = Color.Black;
            }
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            String email = textBoxEmail.Text;
            if (email.Equals("Email") || email.Trim().Equals(""))
            {
                textBoxEmail.Text = "Email";
                textBoxEmail.ForeColor = Color.Gray;
            }
        }

        private void textBoxUsername_Enter(object sender, EventArgs e)
        {
            String username = textBoxUsername.Text;
            if (username.Equals("User Name"))
            {
                textBoxUsername.Text = "";
                textBoxUsername.ForeColor = Color.Black;
            }
        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            String username = textBoxUsername.Text;
            if (username.Equals("User Name") || username.Trim().Equals(""))
            {
                textBoxUsername.Text = "User Name";
                textBoxUsername.ForeColor = Color.Gray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            String password = textBoxPassword.Text;
            if (password.Equals("Password"))
            {
                textBoxPassword.Text = "";
                textBoxPassword.UseSystemPasswordChar = true;
                textBoxPassword.ForeColor = Color.Black;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            String password = textBoxPassword.Text;
            if (password.Equals("Password") || password.Trim().Equals(""))
            {
                textBoxPassword.Text = "Password";
                textBoxPassword.UseSystemPasswordChar = false;
                textBoxPassword.ForeColor = Color.Gray;
            }
        }

        private void textBoxConfirmPassword_Enter(object sender, EventArgs e)
        {
            String cpassword = textBoxConfirmPassword.Text;
            if (cpassword.Equals("Confirm Password"))
            {
                textBoxConfirmPassword.Text = "";
                textBoxConfirmPassword.UseSystemPasswordChar = true;
                textBoxConfirmPassword.ForeColor = Color.Black;
            }
        }

        private void textBoxConfirmPassword_Leave(object sender, EventArgs e)
        {
            String cpassword = textBoxConfirmPassword.Text;
            if (cpassword.Equals("Confirm Password") || cpassword.Trim().Equals(""))
            {
                textBoxConfirmPassword.Text = "Confirm Password";
                textBoxConfirmPassword.UseSystemPasswordChar = false;
                textBoxConfirmPassword.ForeColor = Color.Gray;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void buttonCreateAccount_Click(object sender, EventArgs e)
        {
            // add new user
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users`(`firstname`, `lastname`, `email`, `username`, `password`) VALUES (@fn, @ln, @email, @usn, @pass)", db.getConnection());
            
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = textBoxFirstName.Text;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = textBoxLastName.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBoxEmail.Text;
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = textBoxUsername.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBoxPassword.Text;

            //open the connection
            db.openConnection();

            if (!checkTextBoxesValues())
            {
                // check if the password equals confirm password
                if (textBoxPassword.Text.Equals(textBoxConfirmPassword.Text))
                {
                    // check if this username already excist
                    if (CheckUsername())
                    {
                        MessageBox.Show("This Username Already Excist");
                    }
                    else
                    {
                        //execute the query
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Your Account Created","Account Created",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Wrong Confirm Password","Password Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                }
                
                
            }
            else
            {
                MessageBox.Show("Enter Your Information First","Empty Data",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
            

            

            //close the connection
            db.closeConnection();

            
        }
        // check if the username already exsist
        public Boolean CheckUsername()
        {
            DB db = new DB();

            String username = textBoxUsername.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE  `username`= @usn", db.getConnection());

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = username;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // check if the text boxes contains the defoult values
        public Boolean checkTextBoxesValues()
        {
            String fname = textBoxFirstName.Text;
            String lname = textBoxLastName.Text;
            String email = textBoxEmail.Text;
            String uname = textBoxUsername.Text;
            String pass = textBoxPassword.Text;

            if(fname.Equals("First Name") || lname.Equals("Last Name") || email.Equals("Email") || uname.Equals("User Name") || pass.Equals("Password"))
            {
                return true;
            }
            else
            {
                return false;
            }
 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelGoToLogin_MouseEnter(object sender, EventArgs e)
        {
            labelGoToLogin.ForeColor = Color.Red;
        }

        private void labelGoToLogin_MouseLeave(object sender, EventArgs e)
        {
            labelGoToLogin.ForeColor = Color.Black;
        }

        private void labelGoToLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
    
}
