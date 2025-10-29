ALTER TABLE Categorias
ADD ImagenNombreArchivo NVARCHAR(255) NULL 
    CONSTRAINT DF_Categorias_ImagenNombreArchivo DEFAULT 'sample.jpg';