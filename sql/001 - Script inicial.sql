-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ArmShopDb')
BEGIN
    CREATE DATABASE ArmShopDb;
END
GO

USE ArmShopDb;
GO

-- Eliminar tablas en orden inverso de dependencias
DROP TABLE IF EXISTS CarritoItems;
DROP TABLE IF EXISTS OrdenItems;
DROP TABLE IF EXISTS Ordenes;
DROP TABLE IF EXISTS ProductoVariacionValores;
DROP TABLE IF EXISTS ProductoVariaciones;
DROP TABLE IF EXISTS AtributoValores;
DROP TABLE IF EXISTS Atributos;
DROP TABLE IF EXISTS Productos;
DROP TABLE IF EXISTS Usuarios;
GO

-- Tabla de usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabla de productos base
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabla de atributos (ej: Talle, Color)
CREATE TABLE Atributos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

-- Valores de atributos (ej: "M", "L", "Rojo")
CREATE TABLE AtributoValores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AtributoId INT NOT NULL,
    Valor NVARCHAR(100) NOT NULL,
    FOREIGN KEY (AtributoId) REFERENCES Atributos(Id)
);

-- Variaciones de producto (combinaciones de atributos)
CREATE TABLE ProductoVariaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductoId INT NOT NULL,
    SKU NVARCHAR(50) NOT NULL UNIQUE,
    Precio DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);

-- Relación N a N entre variaciones y valores de atributos
CREATE TABLE ProductoVariacionValores (
    VariacionId INT NOT NULL,
    AtributoValorId INT NOT NULL,
    PRIMARY KEY (VariacionId, AtributoValorId),
    FOREIGN KEY (VariacionId) REFERENCES ProductoVariaciones(Id),
    FOREIGN KEY (AtributoValorId) REFERENCES AtributoValores(Id)
);

-- Órdenes
CREATE TABLE Ordenes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

-- Ítems de órdenes
CREATE TABLE OrdenItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrdenId INT NOT NULL,
    VariacionId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrdenId) REFERENCES Ordenes(Id),
    FOREIGN KEY (VariacionId) REFERENCES ProductoVariaciones(Id)
);

-- Ítems de carrito (referencian variaciones)
CREATE TABLE CarritoItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    VariacionId INT NOT NULL,
    Cantidad INT NOT NULL,
    FechaAgregado DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    FOREIGN KEY (VariacionId) REFERENCES ProductoVariaciones(Id)
);