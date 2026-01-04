USE Spa
GO

-- Crear tabla Usuarios
CREATE TABLE Usuarios
(
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL
)
GO

-- Usuario de prueba
INSERT INTO Usuarios (NombreUsuario, Password) VALUES ('admin', 'admin')
GO

SELECT * FROM Usuarios
GO
