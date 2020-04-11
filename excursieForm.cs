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
using System.Threading;
using System.Resources;

namespace TravelApplication
{
    public partial class excursieForm : Form
    {
        public excursieForm()
        {
            InitializeComponent();
            pictureBox1.Image = (Bitmap)Properties.Resources.defaultImage;
        }

        // Conexiunea cu baza de date 
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\School\Anul III\C#\Lucrare individuală\travel-application\TravelApplication.mdf;Integrated Security=True");

        // Declaration of date1, date2;
        string date1, date2;

        // DataTable 3
        DataTable dt3 = new DataTable();

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void excursieForm_Leave(object sender, EventArgs e)
        {
            this.Hide();
            menuForm f5 = new menuForm();
            f5.Show();
        }

        private void excursieForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            menuForm f5 = new menuForm();
            f5.Show();
        }

        private void excursieForm_Load(object sender, EventArgs e)
        {
            // dataGridView1
            string query = "SELECT Nume, DataStart, DataStop FROM Planificari INNER JOIN Localitati ON Planificari.IdLocalitate = Localitati.IdLocalitate";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dt1 = new DataTable();
            sda.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fillDataGrid()
        {
            // dataGridView2
            date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            // Retrieve data from database
            string query2 = "SELECT Nume, DataStart, DataStop FROM Planificari";
            query2 += " INNER JOIN Localitati ON Planificari.IdLocalitate = Localitati.IdLocalitate WHERE DataStart BETWEEN";
            query2 += "'" + date1 + "' AND '" + date2 + "'";
            SqlDataAdapter sda2 = new SqlDataAdapter(query2, sqlcon);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // dataGridView3_VizualizareItinerariu
            // Retrieve data from database
            string query3 = "SELECT Nume, CalendarDate FROM Planificari ";
            query3 += "INNER JOIN Localitati ON Planificari.IdLocalitate = Localitati.IdLocalitate ";
            query3 += "JOIN Calendar c ON c.CalendarDate BETWEEN Planificari.DataStart AND Planificari.DataStop ";
            query3 += "WHERE DataStart BETWEEN '" + date1 + "' AND '" + date2 + "'" + " ORDER BY Nume, CalendarDate;";
            SqlDataAdapter sda3 = new SqlDataAdapter(query3, sqlcon);
            dt3.Clear();
            sda3.Fill(dt3);
            dataGridView3.DataSource = dt3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void excursieForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fillDataGrid();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fillDataGrid();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fillDataGrid();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fillDataGrid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // DataTable with CaleFisier
            DataTable dt4 = new DataTable();
            string query = "SELECT CaleFisier FROM Imagini";
            SqlDataAdapter sda4 = new SqlDataAdapter(query, sqlcon);
            sda4.Fill(dt4);

            // Lenght of dt4
            int dt4Lenght = dt4.Rows.Count;

            // Lenght of dt3
            int n = dt3.Rows.Count;

            // Check if DataTable contain rows
            if (n == 0)
            {
                MessageBox.Show("Selectați data excursiei!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                // progressBar1 initializer
                progressBar1.Maximum = n - 1;
                progressBar1.Step = 1;
                progressBar1.Value = 0;

                // Declaration of list localitateImagini (List with CaleFisier)
                List<string> localitateImagini = new List<string>();
                localitateImagini.Add(dt4.Rows[0]["CaleFisier"].ToString());
                int m = 0;

                for (int i = 0; i < n; i++)
                {
                    // label3
                    label3.Text = dt3.Rows[i]["Nume"].ToString();
                    label3.Invalidate();
                    label3.Update();

                    // label4
                    var date = Convert.ToDateTime(dt3.Rows[i]["CalendarDate"]);
                    label4.Text = date.ToString("dd-MM-yyyy");
                    label4.Invalidate();
                    label4.Update();

                    // progressBar1 
                    progressBar1.Increment(1);

                    // String of Nume row 
                    string numeString = dt3.Rows[i]["Nume"].ToString();

                    // Array localitateImagini loop
                    if ((numeString.Substring(0, 2) != localitateImagini[0].Substring(0, 2)))
                    {
                        m = 0;
                        localitateImagini.Clear();
                        for (int j = 0; j < dt4Lenght; j++)
                        {
                            string CaleString = dt4.Rows[j]["CaleFisier"].ToString();
                            if (numeString.Substring(0, 2) == CaleString.Substring(0, 2))
                            {
                                localitateImagini.Add(CaleString);
                            }
                        }
                    }

                    // Check if variable m is the end of the list localitateImagini
                    if (m == localitateImagini.Count())
                    {
                        m = 0;
                        pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(localitateImagini[m]);
                        pictureBox1.Invalidate();
                        pictureBox1.Update();
                        // MessageBox.Show(localitateImagini[m]);
                        m++;
                    }
                    else
                    {
                        pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(localitateImagini[m]);
                        pictureBox1.Invalidate();
                        pictureBox1.Update();
                        m++;
                    }

                    // Timer
                    Thread.Sleep(1000);
                }
                MessageBox.Show("Slideshow finalizat!", "Finalizare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }
    }
}
