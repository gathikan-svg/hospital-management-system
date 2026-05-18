using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows;

namespace IntHospitalsys
{
    public partial class DoctorDashboard : Window
    {
        private string doctorUsername;
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\UoM 2nd Sem\Visual Applications\IntHospitalsys\user.mdf"";Integrated Security=True";

        public DoctorDashboard()
        {
            InitializeComponent();
        }

        public DoctorDashboard(string user)
        {
            InitializeComponent();
            this.doctorUsername = user;
            LblWelcome.Text = "Welcome, Dr. " + user;
            LoadPatientData();
        }

        private void LoadPatientData()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT PatientId, FullName, ContactNumber, AppDate, TimeSlot FROM Patients WHERE AssignedDoctor LIKE @doc ORDER BY AppDate DESC";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@doc", "%" + doctorUsername + "%");

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    DgPatients.ItemsSource = dt.DefaultView;
                    TxtTotalPatients.Text = dt.Rows.Count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadPatientData();
            MessageBox.Show("Patient data refreshed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}