CREATE TABLE EmpresaMetadata (
    Id INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Direccion NVARCHAR(300) NULL,
    Telefono NVARCHAR(50) NULL,
    Email NVARCHAR(150) NULL,
    SitioWeb NVARCHAR(150) NULL,
    FacebookUrl NVARCHAR(200) NULL,
    InstagramUrl NVARCHAR(200) NULL,
    TwitterUrl NVARCHAR(200) NULL,
    LinkedinUrl NVARCHAR(200) NULL,
    Descripcion NVARCHAR(500) NULL
);