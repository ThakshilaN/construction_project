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
    public partial class EmployeeLeave : Form
    {
        public EmployeeLeave()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog=User_db;username=root;password= ;Convert Zero Datetime=True;Allow Zero Datetime=True");
        MySqlCommand command;
        MySqlDataReader mdr;

        private void button6_Click(object sender, EventArgs e)
        {
            connection.Open();

            string selectQuery = "SELECT * FROM user_db.employee_registration WHERE Emp_ID=" + int.Parse(textBoxEmpId.Text);
            command = new MySqlCommand(selectQuery, connection);

            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                textBoxName.Text = mdr.GetString("name");
            }
            else
            {
                MessageBox.Show("no data this id");
            }
            connection.Close();
        }

        public void openConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void executeMyQuery(string query)
        {
            try
            {
                openConnection();
                command = new MySqlCommand(query, connection);

                if (command.ExecuteNonQuery() == 1)
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

        private void button_insert(object sender, EventArgs e)
        {
            string insertQuary = "INSERT INTO employee_leave(Emp_ID, Start_Date, End_Date) VALUES('" + 
                int.Parse(textBoxEmpId.Text) + "', '" + DateTime.Parse(dateTimeStart.Text).ToShortDateString() + 
                "', '" + DateTime.Parse(dateTimeEnd.Text).ToShortDateString() + "')";
            executeMyQuery(insertQuary);
            populateDGV();
            // DateTime theDate = DateTime.Now;
            // theDate.ToString("yyyy-MM-dd H:mm:ss");
        }

        private void EmployeeLeave_Load(object sender, EventArgs e)
        {
            populateDGV();
        }

        public void populateDGV()
        {
            // populate the datagridview
            string selectQuery = "SELECT * FROM employee_leave";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection);
            adapter.Fill(table);
            dataGridView_leave.DataSource = table;
        }

        private void dataGridView_leave_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxEmpId.Text = dataGridView_leave.CurrentRow.Cells[0].Value.ToString();
            dateTimeStart.Value = Convert.ToDateTime(dataGridView_leave.CurrentRow.Cells[1].Value);
            dateTimeEnd.Value = Convert.ToDateTime(dataGridView_leave.CurrentRow.Cells[2].Value);
        }
    }
}

