# ApiExamenMpviles

Integrantes:
- Yeiler Madrigal Matamoros
- Dilan Gutierrez Hernandez


Paso 1- Clonamos el repositorio en la carpeta de github o en la carpeta que desee con el siguiente comando:

git clone https://github.com/DilanGOD02/ApiExamenMoviles.git

Paso 2- Se crea una base de datos llamada ExamenMoviles utilizando el siguiente comando:

CREATE DATABASE ExamenMoviles;

Luego definimos las tablas necesarias para nuestro sistema:

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

. 
Paso 3- Creación de usuario en SQL Server
Se crea un usuario con los permisos necesarios para trabajar sobre la base de datos:

-- Crear un login a nivel del servidor
CREATE LOGIN usuarioExamen WITH PASSWORD = '1234';

-- Usar la base de datos ExamenMoviles
USE ExamenMoviles;

-- Crear un usuario dentro de la base de datos basado en el login anterior
CREATE USER usuarioExamen FOR LOGIN usuarioExamen;

-- Darle permisos de db_owner para darle control total
ALTER ROLE db_owner ADD MEMBER usuarioExamen;
ALTER ROLE db_owner ADD MEMBER usuarioExamen;


Paso 4- Vamos a utilizar nuestro backend en .net donde vamos a configurar
 nuestro la conexión con la base de datos en el archivo appsettings.json de esta forma:

{
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ExamenMoviles;User Id=usuarioExamen;Password=1234;TrustServerCertificate=True;"
 }

Paso 5- Para aplicar las migraciones y actualizar la base de datos, se utiliza el siguiente comando en la terminal de Visual Studio Code:

dotnet ef database update

Paso 6- utilizar el comando dotnet build


Paso 7-Finalmente, se inicia la aplicación en modo desarrollo con el siguiente comando:

dotnet watch run
