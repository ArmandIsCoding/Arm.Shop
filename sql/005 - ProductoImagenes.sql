CREATE TABLE ProductoImagenes (
    Id INT IDENTITY PRIMARY KEY,
    ProductoId INT NOT NULL,
    NombreArchivo NVARCHAR(200) NOT NULL,
    EsPrincipal BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);