using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TravelApplication
{
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void bunifuGradientPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void bunifuGradientPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void register()
        {
            // Extract data from TextBox
            string username = alphaBlendTextBox2.Text;
            string password = alphaBlendTextBox3.Text;
            string password2 = alphaBlendTextBox4.Text;

            // Database connection
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\School\Anul III\C#\Lucrare individuală nr. 1\travel-application\TravelApplication.mdf;Integrated Security=True");
            sqlcon.Open();

            // Data validation
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(password2))
            {
                if (password == password2)
                {
                    // Check in database if exists identical username
                    string query = "SELECT Username FROM Users WHERE Username = '" + username.Trim() + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
                    DataTable checkDuplicates = new DataTable();
                    sda.Fill(checkDuplicates);
                    if (checkDuplicates.Rows.Count == 1)
                    {
                        MessageBox.Show("Username existent!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string queryInsert = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                        SqlCommand command = new SqlCommand(queryInsert, sqlcon);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Cont înregistrat cu succes!", "Autentificare reușită!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Parolele nu coincid!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nu puteți lăsa câmpuri goale!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            register();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm f3 = new loginForm();
            f3.Show();
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void registerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                var result = MessageBox.Show("Ești sigur că dorești să ieși?", "Ieșire",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                    Application.Exit();
            }

            if (e.KeyCode == Keys.Enter)
            {
                register();
            }
        }
    }
}
