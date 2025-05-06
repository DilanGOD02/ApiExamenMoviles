CREATE DATABASE ExamenMoviles

use ExamenMoviles

CREATE TABLE Courses (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    imageUrl NVARCHAR(255),
    schedule NVARCHAR(255),
    professor NVARCHAR(255)
);

CREATE TABLE Students (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    phone NVARCHAR(15),
    courseId INT,
    FOREIGN KEY (courseId) REFERENCES Courses(id) ON DELETE SET NULL
);