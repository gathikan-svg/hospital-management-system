using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace IntHospitalsys
{
    public partial class PatientRegistration : Window
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\UoM 2nd Sem\Visual Applications\IntHospitalsys\user.mdf"";Integrated Security=True";

        public PatientRegistration()
        {
            InitializeComponent();
            CmbTimeSlot.SelectedIndex = 0;
            CmbDoctor.SelectedIndex = 0;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFullName.Text))
            {
                MessageBox.Show("Please enter patient name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtContact.Text))
            {
                MessageBox.Show("Please enter contact number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DpAppDate.SelectedDate == null)
            {
                MessageBox.Show("Please select appointment date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CmbTimeSlot.SelectedIndex < 0)
            {
                MessageBox.Show("Please select time slot.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CmbDoctor.SelectedIndex < 0)
            {
                MessageBox.Show("Please select doctor.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO Patients (FullName, Address, AssignedDoctor, ContactNumber, AppDate, TimeSlot) " +
                                 "VALUES (@name, @address, @doc, @contact, @date, @slot)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", TxtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@address", TxtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@doc", CmbDoctor.Text);
                        cmd.Parameters.AddWithValue("@contact", TxtContact.Text.Trim());
                        cmd.Parameters.AddWithValue("@date", DpAppDate.SelectedDate);
                        cmd.Parameters.AddWithValue("@slot", CmbTimeSlot.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration Successful! Your appointment has been booked.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        TxtFullName.Clear();
                        TxtAddress.Clear();
                        TxtContact.Clear();
                        CmbTimeSlot.SelectedIndex = 0;
                        CmbDoctor.SelectedIndex = 0;
                        DpAppDate.SelectedDate = null;

                        MainWindow login = new MainWindow();
                        login.Show();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}