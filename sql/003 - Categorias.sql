-- ============================================
-- 1) Crear tabla Categorias (si no existe)
-- ============================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Categorias' AND type = 'U')
BEGIN
    CREATE TABLE dbo.Categorias (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Descripcion NVARCHAR(255) NULL,
        CategoriaPadreId INT NULL,
        CONSTRAINT FK_Categorias_CategoriasPadre
            FOREIGN KEY (CategoriaPadreId) REFERENCES dbo.Categorias(Id)
    );
END;
GO

-- ============================================
-- 2) Insertar jerarqu�a de categor�as (3 niveles)
--    Idempotente: no duplica si ya existen por Nombre
-- ============================================
IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Indumentaria')
    INSERT INTO dbo.Categorias (Nombre, Descripcion) VALUES ('Indumentaria', 'Ropa y accesorios');

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Calzado')
    INSERT INTO dbo.Categorias (Nombre, Descripcion) VALUES ('Calzado', 'Zapatillas y zapatos');

DECLARE @IndumentariaId INT = (SELECT Id FROM dbo.Categorias WHERE Nombre = 'Indumentaria');
DECLARE @CalzadoId      INT = (SELECT Id FROM dbo.Categorias WHERE Nombre = 'Calzado');

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Remeras')
    INSERT INTO dbo.Categorias (Nombre, Descripcion, CategoriaPadreId) VALUES ('Remeras', 'Remeras de algod�n', @IndumentariaId);

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Pantalones')
    INSERT INTO dbo.Categorias (Nombre, Descripcion, CategoriaPadreId) VALUES ('Pantalones', 'Jeans y joggers', @IndumentariaId);

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Zapatillas deportivas')
    INSERT INTO dbo.Categorias (Nombre, Descripcion, CategoriaPadreId) VALUES ('Zapatillas deportivas', 'Para correr o entrenar', @CalzadoId);

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Zapatos urbanos')
    INSERT INTO dbo.Categorias (Nombre, Descripcion, CategoriaPadreId) VALUES ('Zapatos urbanos', 'Elegantes o casuales', @CalzadoId);

DECLARE @RemerasId    INT = (SELECT Id FROM dbo.Categorias WHERE Nombre = 'Remeras');
DECLARE @PantalonesId INT = (SELECT Id FROM dbo.Categorias WHERE Nombre = 'Pantalones');
DECLARE @ZapatillasId INT = (SELECT Id FROM dbo.Categorias WHERE Nombre = 'Zapatillas deportivas');

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Remeras estampadas')
    INSERT INTO dbo.Categorias (Nombre, Descripcion, CategoriaPadreId) VALUES ('Remeras estampadas', 'Con dise�os gr�ficos', @RemerasId);

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Joggers deportivos')
    INSERT INTO dbo.Categorias (Nombre, Descripcion, CategoriaPadreId) VALUES ('Joggers deportivos', 'C�modos para entrenar', @PantalonesId);

IF NOT EXISTS (SELECT 1 FROM dbo.Categorias WHERE Nombre = 'Running profesional')
    INSERT INTO dbo.Categorias (Nombre, Descripcion, CategoriaPadreId) VALUES ('Running profesional', 'Alta gama para corredores', @ZapatillasId);
GO

-- ============================================
-- 3) Agregar columna CategoriaId a Productos (si no existe) y FK
-- ============================================
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Productos' AND COLUMN_NAME = 'CategoriaId'
)
BEGIN
    ALTER TABLE dbo.Productos ADD CategoriaId INT NULL;
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'FK_Productos_Categorias' AND parent_object_id = OBJECT_ID('dbo.Productos')
)
BEGIN
    ALTER TABLE dbo.Productos
    ADD CONSTRAINT FK_Productos_Categorias
        FOREIGN KEY (CategoriaId) REFERENCES dbo.Categorias(Id);
END;
GO

-- ============================================
-- 4) Asignar categor�as hoja aleatorias a productos sin categor�a
--    Evita CTE y usa CROSS APPLY con TOP 1 ORDER BY NEWID()
-- ============================================
UPDATE p
SET p.CategoriaId = ch.Id
FROM dbo.Productos AS p
CROSS APPLY (
    SELECT TOP 1 c.Id
    FROM dbo.Categorias AS c
    LEFT JOIN dbo.Categorias AS sub ON sub.CategoriaPadreId = c.Id
    WHERE sub.Id IS NULL  -- hojas: categor�as sin hijas
    ORDER BY NEWID()      -- aleatoria
) AS ch
WHERE p.CategoriaId IS NULL;
GO

-- Opcional: si quer�s forzar NOT NULL en CategoriaId despu�s de asignar
-- (asegurate de que no queden productos sin categor�a)
-- ALTER TABLE dbo.Productos ALTER COLUMN CategoriaId INT NOT NULL;