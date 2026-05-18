# Complete Installation Guide - Hospital Management System

## Step-by-Step Setup Instructions

### Phase 1: Environment Setup

#### 1.1 Install Required Software
1. **Visual Studio 2022 Community Edition**
   - Download: https://visualstudio.microsoft.com/vs/community/
   - Include: .NET 6.0 SDK, Desktop development with C++

2. **SQL Server Management Studio (SSMS)**
   - Download: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms
   - Version: 19.0 or higher

3. **.NET 6.0 SDK** (if not included with Visual Studio)
   - Download: https://dotnet.microsoft.com/download/dotnet/6.0

### Phase 2: Database Setup

#### 2.1 Create Database
1. Open SQL Server Management Studio
2. Connect to `(LocalDB)\MSSQLLocalDB`
3. Open new Query window
4. Copy and paste contents of `DATABASE_SETUP.sql`
5. Click Execute or press `F5`
6. Verify all tables created successfully

#### 2.2 Verify Database
```sql
-- Run this to verify
SELECT * FROM Users;
SELECT * FROM Patients;
```

### Phase 3: Project Setup

#### 3.1 Clone/Open Project
1. Clone from GitHub:
   ```bash
   git clone https://github.com/gathikan-svg/hospital-management-system.git
   ```
2. OR download as ZIP and extract

#### 3.2 Open in Visual Studio
1. Open Visual Studio 2022
2. File → Open → Folder
3. Navigate to project folder
4. Open `IntHospitalsys.csproj`

#### 3.3 Install NuGet Packages
1. Tools → NuGet Package Manager → Package Manager Console
2. Run:
   ```powershell
   Install-Package Twilio
   Install-Package Microsoft.Data.SqlClient
   ```

### Phase 4: Configuration

#### 4.1 Update Connection String
Find this in all `.xaml.cs` files:
```csharp
string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\"D:\UoM 2nd Sem\Visual Applications\IntHospitalsys\user.mdf\";Integrated Security=True";
```

Replace with your path:
```csharp
string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\"C:\Users\YourUsername\Documents\hospital-management-system\user.mdf\";Integrated Security=True";
```

#### 4.2 Optional: Configure Twilio SMS
Edit `StaffDashboard.xaml.cs` (around line 22-24):

```csharp
private const string TWILIO_ACCOUNT_SID = "ACxxxxxxxxxxxxxxxxxx";
private const string TWILIO_AUTH_TOKEN = "your_auth_token_here";
private const string TWILIO_PHONE_NUMBER = "+1234567890";
```

**To get Twilio credentials:**
1. Go to https://www.twilio.com/console
2. Sign up for free account
3. Copy Account SID from dashboard
4. Copy Auth Token from dashboard
5. Get a Twilio phone number (or use trial number)

### Phase 5: First Run

#### 5.1 Build Project
1. Press `Ctrl + Shift + B` or Build → Build Solution
2. Wait for build to complete (should show "Build succeeded")

#### 5.2 Run Application
1. Press `F5` or Debug → Start Debugging
2. Application should launch

#### 5.3 Test Login
**Doctor Login:**
- Username: `smith`
- Password: `password123`

**Staff Login:**
- Username: `staff1`
- Password: `staff123`

### Phase 6: Troubleshooting

#### Issue: "Cannot connect to database"
**Solution:**
1. Open SQL Server Management Studio
2. Connect to `(LocalDB)\MSSQLLocalDB`
3. Right-click → Properties → Files
4. Note the file path
5. Update connection string in code with correct path

#### Issue: "Object reference not set to an instance"
**Solution:**
1. Clean solution: Build → Clean Solution
2. Rebuild: Build → Rebuild Solution
3. Delete bin/obj folders manually
4. Reopen project

#### Issue: "NuGet packages not found"
**Solution:**
1. Tools → NuGet Package Manager → Manage NuGet Packages for Solution
2. Click "Restore" button
3. Wait for restore to complete

#### Issue: "Twilio not working"
**Solution:**
1. Verify credentials are correct
2. Check phone numbers include country code
3. Ensure Twilio account has sufficient credits
4. Check SMS status in StaffDashboard for error messages

### Phase 7: Create Database File (If user.mdf missing)

1. In Visual Studio, open Package Manager Console
2. Run:
   ```powershell
   Invoke-Sql -Server '(LocalDB)\MSSQLLocalDB' -Database 'IntHospitalDB'
   ```
3. Then run the DATABASE_SETUP.sql script

### Phase 8: Testing All Features

#### 8.1 Patient Registration
1. Click "Register for Appointment"
2. Fill in all fields
3. Submit
4. Verify in Staff Dashboard

#### 8.2 Doctor Dashboard
1. Login as doctor
2. View your assigned patients
3. Click "Refresh List"
4. Verify patient count

#### 8.3 Staff Dashboard
1. Login as staff
2. View all patients
3. Toggle doctor status buttons
4. Send SMS (if Twilio configured)
5. Refresh statistics

## Performance Tips

- Keep database file on local drive (faster access)
- Rebuild solution before first run
- Close other Visual Studio instances
- Ensure antivirus doesn't block LocalDB

## Support Contacts

- **GitHub Issues**: Report bugs on GitHub
- **Twilio Support**: https://support.twilio.com
- **SQL Server Help**: https://learn.microsoft.com/en-us/sql/

## Success Checklist

- [ ] Visual Studio 2022 installed
- [ ] SQL Server Management Studio installed
- [ ] Project cloned/extracted
- [ ] NuGet packages installed
- [ ] Database created and populated
- [ ] Connection string updated
- [ ] Project builds successfully
- [ ] Doctor login works
- [ ] Staff login works
- [ ] Patient registration works

---

**✅ Application is ready for testing!**
