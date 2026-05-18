using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace IntHospitalsys
{
    public partial class StaffDashboard : Window
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\UoM 2nd Sem\Visual Applications\IntHospitalsys\user.mdf"";Integrated Security=True";

        private const string TWILIO_ACCOUNT_SID = "YOUR_ACCOUNT_SID_HERE";
        private const string TWILIO_AUTH_TOKEN = "YOUR_AUTH_TOKEN_HERE";
        private const string TWILIO_PHONE_NUMBER = "+1234567890";

        private DataTable patientsData;

        public StaffDashboard()
        {
            InitializeComponent();
            InitializeTwilio();
            LoadGlobalData();
            DoctorFilterCombo.SelectedIndex = 0;
        }

        private void InitializeTwilio()
        {
            try
            {
                if (TWILIO_ACCOUNT_SID == "YOUR_ACCOUNT_SID_HERE")
                {
                    MessageBox.Show("Warning: Twilio credentials not configured.\n\nTo enable SMS:\n1. Update TWILIO_ACCOUNT_SID\n2. Update TWILIO_AUTH_TOKEN\n3. Update TWILIO_PHONE_NUMBER", 
                        "Configuration Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                TwilioClient.Init(TWILIO_ACCOUNT_SID, TWILIO_AUTH_TOKEN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Twilio initialization warning: " + ex.Message);
            }
        }

        private void LoadGlobalData()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    SqlDataAdapter adp = new SqlDataAdapter("SELECT FullName, ContactNumber, AssignedDoctor, TimeSlot FROM Patients ORDER BY AssignedDoctor", conn);
                    patientsData = new DataTable();
                    adp.Fill(patientsData);
                    DgAllPatients.ItemsSource = patientsData.DefaultView;

                    string stats = "";
                    string[] doctors = { "smith", "John", "Lee", "Adam" };

                    foreach (string doc in doctors)
                    {
                        string query = "SELECT COUNT(*) FROM Patients WHERE AssignedDoctor LIKE @doc";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@doc", "%" + doc + "%");
                            int count = (int)cmd.ExecuteScalar();
                            stats += $"Dr. {doc}: {count} Patients\n";
                        }
                    }
                    TxtStats.Text = stats;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void SendSmsAll_Click(object sender, RoutedEventArgs e)
        {
            string message = SmsMessageInput.Text.Trim();

            if (string.IsNullOrEmpty(message))
            {
                UpdateSmsStatus("❌ Error: Please enter a message", Brushes.Red);
                return;
            }

            if (patientsData == null || patientsData.Rows.Count == 0)
            {
                UpdateSmsStatus("❌ Error: No patients found in database", Brushes.Red);
                return;
            }

            if (TWILIO_ACCOUNT_SID == "YOUR_ACCOUNT_SID_HERE")
            {
                UpdateSmsStatus("❌ Error: Twilio credentials not configured. Please add credentials to code.", Brushes.Red);
                return;
            }

            UpdateSmsStatus("📤 Sending SMS to all patients...", Brushes.Blue);

            try
            {
                int successCount = 0;
                int failureCount = 0;

                foreach (DataRow row in patientsData.Rows)
                {
                    string contactNumber = row["ContactNumber"].ToString();
                    if (!string.IsNullOrEmpty(contactNumber))
                    {
                        bool sent = await SendSmsAsync(contactNumber, message);
                        if (sent)
                            successCount++;
                        else
                            failureCount++;
                    }
                }

                UpdateSmsStatus($"✓ Complete!\nSuccess: {successCount} | Failed: {failureCount}", Brushes.Green);
            }
            catch (Exception ex)
            {
                UpdateSmsStatus($"❌ Error: {ex.Message}", Brushes.Red);
            }
        }

        private async void SendSmsSelected_Click(object sender, RoutedEventArgs e)
        {
            string selectedDoctor = DoctorFilterCombo.SelectedItem?.ToString();
            string message = SmsMessageInput.Text.Trim();

            if (selectedDoctor == "All Doctors")
            {
                UpdateSmsStatus("❌ Error: Please select a specific doctor", Brushes.Red);
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                UpdateSmsStatus("❌ Error: Please enter a message", Brushes.Red);
                return;
            }

            if (TWILIO_ACCOUNT_SID == "YOUR_ACCOUNT_SID_HERE")
            {
                UpdateSmsStatus("❌ Error: Twilio credentials not configured.", Brushes.Red);
                return;
            }

            UpdateSmsStatus($"📤 Sending to Dr. {selectedDoctor}'s patients...", Brushes.Blue);

            try
            {
                int successCount = 0;
                int failureCount = 0;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT ContactNumber FROM Patients WHERE AssignedDoctor LIKE @doc";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@doc", "%" + selectedDoctor + "%");
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string contactNumber = reader["ContactNumber"].ToString();
                            if (!string.IsNullOrEmpty(contactNumber))
                            {
                                bool sent = await SendSmsAsync(contactNumber, message);
                                if (sent)
                                    successCount++;
                                else
                                    failureCount++;
                            }
                        }
                        reader.Close();
                    }
                }

                UpdateSmsStatus($"✓ Complete!\nDr. {selectedDoctor}\nSuccess: {successCount} | Failed: {failureCount}", Brushes.Green);
            }
            catch (Exception ex)
            {
                UpdateSmsStatus($"❌ Error: {ex.Message}", Brushes.Red);
            }
        }

        private async Task<bool> SendSmsAsync(string toPhoneNumber, string messageBody)
        {
            try
            {
                var smsMessage = await MessageResource.CreateAsync(
                    body: messageBody,
                    from: new PhoneNumber(TWILIO_PHONE_NUMBER),
                    to: new PhoneNumber(toPhoneNumber)
                );

                return !string.IsNullOrEmpty(smsMessage.Sid);
            }
            catch
            {
                return false;
            }
        }

        private void UpdateSmsStatus(string message, SolidColorBrush color)
        {
            Dispatcher.Invoke(() =>
            {
                SmsStatusLabel.Text = message;
                SmsStatusLabel.Foreground = color;
            });
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag == null) return;

            string docName = btn.Tag.ToString();

            if (btn.Content.ToString().Contains("NOT ARRIVED"))
            {
                btn.Content = $"Dr. {docName}: ARRIVED";
                btn.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                btn.Content = $"Dr. {docName}: NOT ARRIVED";
                btn.Background = new SolidColorBrush(Colors.LightCoral);
            }
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            LoadGlobalData();
            MessageBox.Show("Patient list and stats updated!", "Refreshed", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}