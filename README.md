# Hospital Management System with SMS Gateway

A comprehensive C# WPF desktop application for hospital staff, doctors, and patients to manage appointments with integrated SMS notifications.

## Features

### 🔐 Role-Based Access
- **Doctor Dashboard**: View assigned patients and appointment details
- **Staff Dashboard**: Manage all patients, send bulk SMS notifications
- **Patient Registration**: Self-service appointment booking

### 📱 SMS Notifications (Twilio Integration)
- Send SMS to individual patients
- Bulk SMS to all patients
- Send targeted SMS to specific doctor's patients
- Real-time delivery status tracking

### 📊 Key Functionalities
- Patient appointment scheduling
- Doctor workload tracking
- Clinic attendance management
- Appointment history and logs
- Role-based authentication

## Technology Stack

- **Frontend**: WPF (Windows Presentation Foundation) / XAML
- **Backend**: C# (.NET 6.0)
- **Database**: SQL Server (LocalDB)
- **SMS Service**: Twilio API

## Prerequisites

- Visual Studio 2022 or higher
- .NET 6.0 SDK
- SQL Server Management Studio
- Twilio Account (for SMS functionality)

## Installation & Setup

### 1. Clone Repository
```bash
git clone https://github.com/gathikan-svg/hospital-management-system.git
cd hospital-management-system
```

### 2. Install Dependencies
```bash
Install-Package Twilio
Install-Package Microsoft.Data.SqlClient
```

### 3. Database Setup
1. Open SQL Server Management Studio
2. Run the `DATABASE_SETUP.sql` script
3. This creates all required tables and inserts sample data

### 4. Configure Database Connection
Update connection string in code files:
```csharp
string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\"D:\YourPath\user.mdf\";Integrated Security=True";
```

### 5. Configure Twilio (Optional)
Update in `StaffDashboard.xaml.cs`:
```csharp
private const string TWILIO_ACCOUNT_SID = "YOUR_ACCOUNT_SID";
private const string TWILIO_AUTH_TOKEN = "YOUR_AUTH_TOKEN";
private const string TWILIO_PHONE_NUMBER = "+1234567890";
```

## Test Credentials

### Doctors
- Username: `smith` | Password: `password123`
- Username: `john` | Password: `password123`
- Username: `lee` | Password: `password123`
- Username: `adam` | Password: `password123`

### Staff
- Username: `staff1` | Password: `staff123`
- Username: `staff2` | Password: `staff123`

## Usage

### Doctor Dashboard
1. Login with doctor credentials
2. View all patients assigned to you
3. Check appointment dates and times
4. Click "Refresh List" to update data

### Staff Dashboard
1. Login with staff credentials
2. View all hospital patients
3. Manage doctor attendance status
4. Send SMS notifications:
   - Select message template or write custom
   - Click "Send to All Patients" or "Send to Selected Doctor"
   - Monitor delivery status

### Patient Registration
1. Click "Register for Appointment" on login screen
2. Fill in patient details
3. Select appointment date and time
4. Choose assigned doctor
5. Submit registration
6. Return to login

## Project Structure

```
├── MainWindow.xaml/.cs          # Login interface
├── DoctorDashboard.xaml/.cs     # Doctor view
├── StaffDashboard.xaml/.cs      # Staff administration
├── PatientRegistration.xaml/.cs # Patient signup
├── DATABASE_SETUP.sql           # Database schema
└── README.md                    # Documentation
```

## Database Schema

### Users Table
- UserId (PK)
- Username
- Password
- Role (Doctor/Staff)
- CreatedDate

### Patients Table
- PatientId (PK)
- FullName
- Address
- ContactNumber (for SMS)
- AssignedDoctor
- AppDate
- TimeSlot
- Status
- CreatedDate

### DoctorAttendance Table
- AttendanceId (PK)
- DoctorName
- AttendanceDate
- Status
- CheckInTime
- CreatedDate

## Troubleshooting

### Database Connection Error
- Verify SQL Server LocalDB is running
- Check connection string path
- Ensure user.mdf file exists

### Twilio SMS Not Working
- Verify credentials are correct
- Check phone numbers include country code (e.g., +1)
- Ensure Twilio account has available credits

### Login Issues
- Verify username and password in Users table
- Check Role column matches (Doctor/Staff)
- Ensure database is properly initialized

## Future Enhancements

- [ ] Email notifications
- [ ] Appointment reminders (scheduled SMS)
- [ ] Patient records encryption
- [ ] Appointment feedback/ratings
- [ ] Analytics dashboard
- [ ] Multi-hospital support
- [ ] Mobile app integration
- [ ] Payment integration

## License

MIT License - Feel free to use and modify

## Author

Gathikan SVG Android

## Support

For issues or questions, please create an issue on GitHub or contact the developer.

---

**Last Updated**: May 18, 2026
**Version**: 1.0.0
