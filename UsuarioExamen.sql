-- Crear un login a nivel del servidor
CREATE LOGIN usuarioExamen WITH PASSWORD = '1234';

-- Usar la base de datos ExamenMoviles
USE ExamenMoviles;

-- Crear un usuario dentro de la base de datos basado en el login anterior
CREATE USER usuarioExamen FOR LOGIN usuarioExamen;

-- Darle permisos de db_owner para darle control total
ALTER ROLE db_owner ADD MEMBER usuarioExamen;
ALTER ROLE db_owner ADD MEMBER usuarioExamen;

