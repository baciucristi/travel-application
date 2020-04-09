using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;

namespace TravelApplication
{
    public partial class posterForm : Form
    {
        // Database connection
        SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\School\Anul III\C#\Lucrare individuală nr. 1\TravelApplication\TravelApplication.mdf;Integrated Security=True");

        public posterForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox2 Imagine
            comboBox2.Items.Clear();
            String selectedValue = comboBox1.Text;
            string query2 = "SELECT Nume, CaleFisier FROM Imagini INNER JOIN Localitati ON Imagini.IdLocalitate = Localitati.IdLocalitate WHERE Nume = N'" + selectedValue + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query2, sqlcon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(Path.GetFileName((dr["CaleFisier"].ToString())));
            }
            comboBox2.SelectedIndex = 0;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void posterForm_Load(object sender, EventArgs e)
        {
            // ComboBox1 Localitati
            string query = "SELECT Nume FROM Localitati";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Nume"].ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void posterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            menuForm f5 = new menuForm();
            f5.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show image
            String selectedValue = comboBox2.Text;
            pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(selectedValue);
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String selectedValue = comboBox2.Text;
            listBox1.Items.Add(selectedValue);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedItem = listBox1.GetItemText(listBox1.SelectedItem);
            // Show image
            pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(selectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Save image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG format|*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var Path = saveFileDialog.FileName;
                pictureBox1.Image.Save(Path, ImageFormat.Png);
                MessageBox.Show("Fișierul a fost salvat cu succes!", "Salvare reușită", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
