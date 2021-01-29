using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySqlConnectivity
{
    public partial class EmployeeRegistration : Form
    {
        public EmployeeRegistration()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog=User_db;username=root;password=");
        MySqlCommand Command;

        private void textBoxEmpID_Enter(object sender, EventArgs e)
        {
            {
                String Eid = textBoxEmpID.Text;
                if (Eid.Equals("Employee ID"))
                {
                    textBoxEmpID.Text = "";
                    textBoxEmpID.ForeColor = Color.Black;
                }
            }
        }

        private void textBoxEmpID_Leave(object sender, EventArgs e)
        {
            {
                String Eid = textBoxEmpID.Text;
                if (Eid.Equals("Employee ID") || Eid.Trim().Equals(""))
                {
                    textBoxEmpID.Text = "Employee ID";
                    textBoxEmpID.ForeColor = Color.Gray;
                }
            }
        }

        private void textBoxName_Enter(object sender, EventArgs e)
        {
            {
                String EName = textBoxName.Text;
                if (EName.Equals("Employee Name"))
                {
                    textBoxName.Text = "";
                    textBoxName.ForeColor = Color.Black;
                }
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            {
                String EName = textBoxName.Text;
                if (EName.Equals("Employee Name") || EName.Trim().Equals(""))
                {
                    textBoxName.Text = "Employee Name";
                    textBoxName.ForeColor = Color.Gray;
                }
            }
        }

        private void textBoxAge_Enter(object sender, EventArgs e)
        {
            {
                String EAge = textBoxAge.Text;
                if (EAge.Equals("Employee Age"))
                {
                    textBoxAge.Text = "";
                    textBoxAge.ForeColor = Color.Black;
                }
            }
        }

        private void textBoxAge_Leave(object sender, EventArgs e)
        {
            {
                String EAge = textBoxAge.Text;
                if (EAge.Equals("Employee Age") || EAge.Trim().Equals(""))
                {
                    textBoxAge.Text = "Employee Age";
                    textBoxAge.ForeColor = Color.Gray;
                }
            }
        }

        private void textBoxNIC_Enter(object sender, EventArgs e)
        {
            {
                String NIC = textBoxNIC.Text;
                if (NIC.Equals("Employee NIC"))
                {
                    textBoxNIC.Text = "";
                    textBoxNIC.ForeColor = Color.Black;
                }
            }
        }

        private void textBoxNIC_Leave(object sender, EventArgs e)
        {
            {
                String NIC = textBoxNIC.Text;
                if (NIC.Equals("Employee NIC") || NIC.Trim().Equals(""))
                {
                    textBoxNIC.Text = "Employee NIC";
                    textBoxNIC.ForeColor = Color.Gray;
                }
            }
        }

        private void textBoxMobile_Enter(object sender, EventArgs e)
        {
            {
                String Mob_Num = textBoxMobile.Text;
                if (Mob_Num.Equals("Mobile Number"))
                {
                    textBoxMobile.Text = "";
                    textBoxMobile.ForeColor = Color.Black;
                }
            }
        }

        private void textBoxMobile_Leave(object sender, EventArgs e)
        {
            {
                String Mob_Num = textBoxMobile.Text;
                if (Mob_Num.Equals("Mobile Number") || Mob_Num.Trim().Equals(""))
                {
                    textBoxMobile.Text = "Mobile Number";
                    textBoxMobile.ForeColor = Color.Gray;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string insertQuary = "INSERT INTO employee_registration(Emp_ID, Name, Age, NIC, Mobile_Number) VALUES('" + textBoxEmpID.Text + "', '" + textBoxName.Text + "', '" + textBoxAge.Text + "', '" + textBoxNIC.Text + "', '" + textBoxMobile.Text + "')";
            executeMyQuery(insertQuary);
            populateDGV();

            /*(DB db = new DB();

            String Eid = textBoxEmpID.Text;
            String EName = textBoxName.Text;
            String EAge = textBoxAge.Text;
            String NIC = textBoxNIC.Text;
            String Mob_Num = textBoxMobile.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("INSERT INTO `employee_registration`(`Emp_ID`, `Name`, `Age`, `NIC`, `Mobile_Number`) VALUES (@eid, @name, @age, @nic, @mobnumber)", db.getConnection());

            command.Parameters.Add("@eid", MySqlDbType.VarChar).Value = Eid;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = EName;
            command.Parameters.Add("@age", MySqlDbType.VarChar).Value = EAge;
            command.Parameters.Add("@nic", MySqlDbType.VarChar).Value = NIC;
            command.Parameters.Add("@mobnumber", MySqlDbType.VarChar).Value = Mob_Num;

            adapter.SelectCommand = command;

            adapter.Fill(table);*/
        }

        

        private void EmployeeRegistration_Load(object sender, EventArgs e)
        {
            populateDGV();
        }

        public void populateDGV()
        {
            // populate the datagridview
            string selectQuery = "SELECT * FROM employee_registration";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection);
            adapter.Fill(table);
            dataGridView_Employee.DataSource = table;
        }

        private void dataGridView_Employee_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxEmpID.Text = dataGridView_Employee.CurrentRow.Cells[0].Value.ToString();
            textBoxName.Text = dataGridView_Employee.CurrentRow.Cells[1].Value.ToString();
            textBoxAge.Text = dataGridView_Employee.CurrentRow.Cells[2].Value.ToString();
            textBoxNIC.Text = dataGridView_Employee.CurrentRow.Cells[3].Value.ToString();
            textBoxMobile.Text = dataGridView_Employee.CurrentRow.Cells[4].Value.ToString();
        }

        public void openConnection()
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void executeMyQuery(string query)
        {
            try
            {
                openConnection();
                Command = new MySqlCommand(query, connection);

                if(Command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Quary Executed");
                }
                else
                {
                    MessageBox.Show("Quary Not Executed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                closeConnection();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string updateQuary = "UPDATE employee_registration SET Name ='" + textBoxName.Text + "',Age ='" + textBoxAge.Text + "',NIC ='" + textBoxNIC.Text + "',Mobile_Number ='" + textBoxMobile.Text + "' WHERE Emp_ID = " + int.Parse(textBoxEmpID.Text) ;
            executeMyQuery(updateQuary);
            populateDGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string deleteQuary = "DELETE FROM employee_registration WHERE Emp_ID = " + int.Parse(textBoxEmpID.Text);
            executeMyQuery(deleteQuary);
            populateDGV();
        }

        private void searchButton(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxSearch.Text)) { 
            MySqlDataReader mdr;
            string select = "SELECT * FROM employee_registration WHERE Emp_ID = " + textBoxSearch.Text;
            Command = new MySqlCommand(select, connection);
            openConnection();
            mdr = Command.ExecuteReader();

            if (mdr.Read())
            {
                DataTable table = new DataTable();
                //textBoxName.Text = mdr.GetString("name");
                //textBoxNIC.Text = mdr.GetString("nic");
                table.Load(mdr);


                MySqlDataAdapter adapter = new MySqlDataAdapter(select, connection);
                adapter.Fill(table);
                dataGridView_Employee.DataSource = table;
               
                }
                else
            {
                MessageBox.Show("User Not Found");
            }
            }
            else
            {
                MessageBox.Show("User ID Should Not Be Null !");
            }

            closeConnection();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_Employee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxEmpID_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeLeave employeeLeave = new EmployeeLeave();
            employeeLeave.Show();
        }
    }
}
