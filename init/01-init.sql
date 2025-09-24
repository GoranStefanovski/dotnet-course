-- Sample initialization script for .NET application
-- This file will be executed when MariaDB starts for the first time

USE dotnetapp;

-- Example table structure (modify as needed for your application)
CREATE TABLE IF NOT EXISTS Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    IsActive BOOLEAN DEFAULT TRUE
);

-- Example data (optional)
INSERT INTO Users (Username, Email, PasswordHash) VALUES 
('admin', 'admin@example.com', 'hashed_password_here'),
('testuser', 'test@example.com', 'hashed_password_here');

-- Create indexes for better performance
CREATE INDEX idx_users_email ON Users(Email);
CREATE INDEX idx_users_username ON Users(Username);

-- Grant additional permissions if needed
GRANT ALL PRIVILEGES ON dotnetapp.* TO 'dotnetuser'@'%';
FLUSH PRIVILEGES;
