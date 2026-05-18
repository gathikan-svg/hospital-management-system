-- Hospital Management System Database Setup

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Patients (
    PatientId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    ContactNumber NVARCHAR(15) NOT NULL,
    AssignedDoctor NVARCHAR(100) NOT NULL,
    AppDate DATE NOT NULL,
    TimeSlot NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT 'Registered'
);

CREATE TABLE DoctorAttendance (
    AttendanceId INT PRIMARY KEY IDENTITY(1,1),
    DoctorName NVARCHAR(50) NOT NULL,
    AttendanceDate DATE NOT NULL,
    Status NVARCHAR(20) DEFAULT 'NOT ARRIVED',
    CheckInTime DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Insert Sample Users
INSERT INTO Users (Username, Password, Role) VALUES
('smith', 'password123', 'Doctor'),
('john', 'password123', 'Doctor'),
('lee', 'password123', 'Doctor'),
('adam', 'password123', 'Doctor'),
('staff1', 'staff123', 'Staff'),
('staff2', 'staff123', 'Staff');

-- Insert Sample Patients
INSERT INTO Patients (FullName, Address, ContactNumber, AssignedDoctor, AppDate, TimeSlot) VALUES
('John Doe', '123 Main St', '+1234567890', 'Dr.smith - Neurology', '2026-05-20', '08:00 AM - 08:15 AM'),
('Jane Smith', '456 Oak Ave', '+1987654321', 'Dr.smith - Neurology', '2026-05-20', '08:15 AM - 08:30 AM'),
('Mike Johnson', '789 Pine Rd', '+1555666777', 'Dr.John - Cardiology', '2026-05-20', '09:00 AM - 09:15 AM'),
('Sarah Williams', '321 Elm St', '+1444555666', 'Dr.John - Cardiology', '2026-05-20', '09:15 AM - 09:30 AM'),
('Robert Brown', '654 Maple Dr', '+1333444555', 'Dr.Lee - Orthopedics', '2026-05-20', '10:00 AM - 10:15 AM'),
('Emily Davis', '987 Cedar Ln', '+1222333444', 'Dr.Lee - Orthopedics', '2026-05-20', '10:15 AM - 10:30 AM'),
('David Wilson', '147 Birch Way', '+1111222333', 'Dr.Adam - Pediatrics', '2026-05-20', '11:00 AM - 11:15 AM'),
('Lisa Anderson', '258 Spruce Ave', '+1999888777', 'Dr.Adam - Pediatrics', '2026-05-20', '11:15 AM - 11:30 AM');
