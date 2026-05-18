using System;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace IntHospitalsys
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnDoctorLogin_Click(object sender, RoutedEventArgs e)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\UoM 2nd Sem\Visual Applications\IntHospitalsys\user.mdf"";Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT COUNT(1) FROM Users WHERE Username=@user AND Password=@pass AND Role='Doctor'";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", TxtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", TxtPassword.Password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            string doctorName = TxtUsername.Text.Trim();
                            DoctorDashboard docDash = new DoctorDashboard(doctorName);
                            docDash.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Doctor Credentials.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnStaffLogin_Click(object sender, RoutedEventArgs e)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\UoM 2nd Sem\Visual Applications\IntHospitalsys\user.mdf"";Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT COUNT(1) FROM Users WHERE Username=@user AND Password=@pass AND Role='Staff'";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", TxtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", TxtPassword.Password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            StaffDashboard staffDash = new StaffDashboard();
                            staffDash.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Staff Credentials. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnPatientJump_Click(object sender, RoutedEventArgs e)
        {
            new PatientRegistration().Show();
            this.Close();
        }
    }
}