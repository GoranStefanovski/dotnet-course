-- Sample initialization script for .NET application
-- This file will be executed when MariaDB starts for the first time

USE dotnetapp;

-- Users table matching the C# User model
CREATE TABLE IF NOT EXISTS Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Gender VARCHAR(10),
    Active BOOLEAN DEFAULT TRUE
);

-- Example data
INSERT INTO Users (FirstName, LastName, Email, Gender, Active) VALUES 
('John', 'Doe', 'john.doe@example.com', 'Male', TRUE),
('Jane', 'Smith', 'jane.smith@example.com', 'Female', TRUE),
('Bob', 'Johnson', 'bob.johnson@example.com', 'Male', FALSE),
('Alice', 'Williams', 'alice.williams@example.com', 'Female', TRUE);

-- Create indexes for better performance
CREATE INDEX idx_users_email ON Users(Email);

-- Grant additional permissions if needed
GRANT ALL PRIVILEGES ON dotnetapp.* TO 'dotnetuser'@'%';
FLUSH PRIVILEGES;
