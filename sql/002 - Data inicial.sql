USE ArmShopDb;
GO

-- Asegurar atributos base
IF NOT EXISTS (SELECT 1 FROM Atributos WHERE Nombre = 'Talle')
    INSERT INTO Atributos (Nombre) VALUES ('Talle');
IF NOT EXISTS (SELECT 1 FROM Atributos WHERE Nombre = 'Color')
    INSERT INTO Atributos (Nombre) VALUES ('Color');

DECLARE @TalleId INT = (SELECT Id FROM Atributos WHERE Nombre = 'Talle');
DECLARE @ColorId INT = (SELECT Id FROM Atributos WHERE Nombre = 'Color');

-- Insertar valores de talles si no existen
IF NOT EXISTS (SELECT 1 FROM AtributoValores WHERE Valor = 'S' AND AtributoId=@TalleId)
    INSERT INTO AtributoValores (AtributoId, Valor) VALUES (@TalleId, 'S');
IF NOT EXISTS (SELECT 1 FROM AtributoValores WHERE Valor = 'M' AND AtributoId=@TalleId)
    INSERT INTO AtributoValores (AtributoId, Valor) VALUES (@TalleId, 'M');
IF NOT EXISTS (SELECT 1 FROM AtributoValores WHERE Valor = 'L' AND AtributoId=@TalleId)
    INSERT INTO AtributoValores (AtributoId, Valor) VALUES (@TalleId, 'L');

-- Insertar valores de colores si no existen
IF NOT EXISTS (SELECT 1 FROM AtributoValores WHERE Valor = 'Rojo' AND AtributoId=@ColorId)
    INSERT INTO AtributoValores (AtributoId, Valor) VALUES (@ColorId, 'Rojo');
IF NOT EXISTS (SELECT 1 FROM AtributoValores WHERE Valor = 'Azul' AND AtributoId=@ColorId)
    INSERT INTO AtributoValores (AtributoId, Valor) VALUES (@ColorId, 'Azul');
IF NOT EXISTS (SELECT 1 FROM AtributoValores WHERE Valor = 'Negro' AND AtributoId=@ColorId)
    INSERT INTO AtributoValores (AtributoId, Valor) VALUES (@ColorId, 'Negro');

-- Variables para IDs de valores
DECLARE @S INT = (SELECT Id FROM AtributoValores WHERE Valor='S' AND AtributoId=@TalleId);
DECLARE @M INT = (SELECT Id FROM AtributoValores WHERE Valor='M' AND AtributoId=@TalleId);
DECLARE @L INT = (SELECT Id FROM AtributoValores WHERE Valor='L' AND AtributoId=@TalleId);

DECLARE @Rojo INT = (SELECT Id FROM AtributoValores WHERE Valor='Rojo' AND AtributoId=@ColorId);
DECLARE @Azul INT = (SELECT Id FROM AtributoValores WHERE Valor='Azul' AND AtributoId=@ColorId);
DECLARE @Negro INT = (SELECT Id FROM AtributoValores WHERE Valor='Negro' AND AtributoId=@ColorId);

-- Procedimiento rápido para insertar producto con 3 variaciones
DECLARE @i INT = 1;
WHILE @i <= 20
BEGIN
    DECLARE @ProductoId INT;
    INSERT INTO Productos (Nombre, Descripcion) 
    VALUES (CONCAT('Producto ', @i), CONCAT('Descripción del producto ', @i));
    SET @ProductoId = SCOPE_IDENTITY();

    -- Variación 1: S Rojo
    DECLARE @Var1 INT;
    INSERT INTO ProductoVariaciones (ProductoId, SKU, Precio, Stock)
    VALUES (@ProductoId, CONCAT('PROD', @i, '-S-ROJO'), 1000+@i*10, 5+@i);
    SET @Var1 = SCOPE_IDENTITY();
    INSERT INTO ProductoVariacionValores VALUES (@Var1, @S);
    INSERT INTO ProductoVariacionValores VALUES (@Var1, @Rojo);

    -- Variación 2: M Azul
    DECLARE @Var2 INT;
    INSERT INTO ProductoVariaciones (ProductoId, SKU, Precio, Stock)
    VALUES (@ProductoId, CONCAT('PROD', @i, '-M-AZUL'), 1100+@i*10, 10+@i);
    SET @Var2 = SCOPE_IDENTITY();
    INSERT INTO ProductoVariacionValores VALUES (@Var2, @M);
    INSERT INTO ProductoVariacionValores VALUES (@Var2, @Azul);

    -- Variación 3: L Negro
    DECLARE @Var3 INT;
    INSERT INTO ProductoVariaciones (ProductoId, SKU, Precio, Stock)
    VALUES (@ProductoId, CONCAT('PROD', @i, '-L-NEGRO'), 1200+@i*10, 15+@i);
    SET @Var3 = SCOPE_IDENTITY();
    INSERT INTO ProductoVariacionValores VALUES (@Var3, @L);
    INSERT INTO ProductoVariacionValores VALUES (@Var3, @Negro);

    SET @i = @i + 1;
END