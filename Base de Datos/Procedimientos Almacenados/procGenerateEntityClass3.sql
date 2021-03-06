USE [DbGlobalCertification]
GO
/****** Object:  StoredProcedure [dbo].[procGenerateEntityClass3]    Script Date: 24/03/2015 01:59:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[procGenerateEntityClass3]
@ObjectName varchar(100)
AS
DECLARE @name varchar(50),
@type varchar(20),
@objRel varchar(50),
@nulo int,
@columna varchar(50),
@propertyObjRel varchar(8000),
@propertyPrivateObjRel varchar(8000),
@salida varchar(MAX)


DECLARE objCursor CURSOR
FOR
SELECT sc.name, st.name type , sc.isnullable nulo FROM syscolumns sc 
INNER JOIN systypes st
ON st.xusertype = sc.xusertype
WHERE Id=OBJECT_ID(@ObjectName)
DECLARE @declarationCodes varchar(8000),
@ctorCodes varchar(8000),
@propertyCodes varchar(8000)
SET @propertyObjRel = ''
SET @propertyPrivateObjRel = ''
SET @declarationCodes = ''
SET @ctorCodes = ''
SET @propertyCodes = ''
OPEN objCursor
FETCH NEXT FROM objCursor
INTO @name, @type, @nulo
DECLARE @cType varchar(50)-- C# type
DECLARE @ctorParms varchar(5000)
SET @ctorParms = ''
IF @@FETCH_STATUS <> 0
    BEGIN
    CLOSE objCursor
    DEALLOCATE objCursor
    PRINT 'Error... Please CHECK passed parameter'
    RETURN
END

WHILE @@FETCH_STATUS = 0


    BEGIN
    SET @cType = 
    CASE
    WHEN @type LIKE '%char%' OR @type LIKE '%text%' 
    THEN 'string'
    WHEN @type IN ('decimal', 'numeric')
    THEN 'double'
    WHEN @type = 'real'
    THEN 'float'
    WHEN @type LIKE '%money%'
    THEN 'decimal'
    WHEN @type = 'bit'
    THEN 'bool'
    WHEN @type = 'bigint'
    THEN 'long'
    WHEN @type LIKE '%int%'
    THEN 'int'
       WHEN @type LIKE 'datetime'
    THEN 'DateTime'
       WHEN @type LIKE 'uniqueidentifier'
       THEN 'Guid'
    ELSE
    @type    

END

IF (@nulo = 1 OR @cType LIKE '%bool%') AND @cType NOT LIKE '%string%'
BEGIN
SET @cType = @cType + '?'  
END

--PRINT CHAR(9) + 'private ' + @ctype + 
--     ' ' + 'm_' + @name + ';'
SET @declarationCodes = @declarationCodes + CHAR(9) + CHAR(9) + 'private ' + @ctype + ' ' + LOWER(LEFT(@name, 1)) + SUBSTRING(@name, 2, LEN(@name)) + ';' + CHAR(13)
SET @ctorCodes = @ctorCodes + CHAR(9) + CHAR(9) + CHAR(9) + + LOWER(LEFT(@name, 1)) + SUBSTRING(@name, 2, LEN(@name)) + ' = _' + LOWER(@name)  + ';' + CHAR(13)
SET @ctorParms = @ctorParms + @ctype + ' _' + LOWER(LEFT(@name, 1)) + SUBSTRING(@name, 2, LEN(@name)) + ', '
SET @propertyCodes = @propertyCodes + CHAR(9) + CHAR(9) + 'public ' + @ctype + ' ' + @name + CHAR(13) +
CHAR(9) + CHAR(9) + '{' + CHAR(13) +
CHAR(9) + CHAR(9) + CHAR(9) + 'get { return ' + LOWER(LEFT(@name, 1)) + SUBSTRING(@name, 2, LEN(@name)) + '; }' + CHAR(13) +
CHAR(9) + CHAR(9) + CHAR(9) + 'set { ' + LOWER(LEFT(@name, 1)) + SUBSTRING(@name, 2, LEN(@name)) + ' = value; }' + CHAR(13) +
CHAR(9) + CHAR(9) + '}' + CHAR(13)
FETCH NEXT FROM objCursor
INTO @name, @type ,@nulo
END
set @salida = '';

set @salida = @salida + 'using System;' + CHAR(13)
set @salida = @salida + 'using System.Linq;' + CHAR(13)
set @salida = @salida + 'using System.Text;' + CHAR(13)
set @salida = @salida + 'namespace EntidadesLayout.Ent ' + CHAR(13)
set @salida = @salida + '{' + CHAR(13)
set @salida = @salida + CHAR(9) +'[Serializable]' + CHAR(13)
set @salida = @salida + CHAR(9) + 'public class ' + @ObjectName + CHAR(13)
set @salida = @salida + CHAR(9) + '{' + CHAR(13)
set @salida = @salida + @declarationCodes
set @salida = @salida + ''

set @salida = @salida + CHAR(9) + CHAR(9) + 'public ' + @ObjectName + '(){}' + CHAR(13)
set @salida = @salida + '' + CHAR(13)
set @salida = @salida + @propertyCodes + CHAR(13)

CLOSE objCursor
DEALLOCATE objCursor

DECLARE objCursor2 CURSOR FOR
SELECT     
    FK_Column = CU.COLUMN_NAME, 
    PK_Table  = PK.TABLE_NAME   
FROM 
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C 
    INNER JOIN 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK 
        ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME 
    INNER JOIN 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK 
        ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME 
    INNER JOIN 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU 
        ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME 
    INNER JOIN 
    ( 
        SELECT 
            i1.TABLE_NAME, i2.COLUMN_NAME 
        FROM 
            INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 
            INNER JOIN 
            INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 
            ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME 
            WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY' 
    ) PT 
    ON PT.TABLE_NAME = PK.TABLE_NAME 
-- optional:
WHERE FK.TABLE_NAME=@ObjectName  
OPEN objCursor2;
FETCH NEXT FROM objCursor2 INTO @columna,@objRel


WHILE @@FETCH_STATUS = 0
   BEGIN
      SET @propertyPrivateObjRel = @propertyPrivateObjRel + CHAR(9) + CHAR(9) + 'private ' + @objRel + ' ' + LOWER(LEFT(SUBSTRING(@columna, 3, LEN(@columna)), 1)) + SUBSTRING(@columna, 4, LEN(@columna)) + ';' + CHAR(13);

       SET @propertyObjRel = @propertyObjRel + CHAR(9) + CHAR(9) + 'public ' + @objRel + ' ' +  SUBSTRING(@columna, 3, LEN(@columna)) + CHAR(13) +
          CHAR(9) + CHAR(9) + '{' + CHAR(13) +
          CHAR(9) + CHAR(9) + CHAR(9) + 'get { return ' + LOWER(LEFT(SUBSTRING(@columna, 3, LEN(@columna)), 1)) + SUBSTRING(@columna, 4, LEN(@columna)) + '; }' + CHAR(13) +
       CHAR(9) + CHAR(9) + CHAR(9) + 'set { ' + LOWER(LEFT(SUBSTRING(@columna, 3, LEN(@columna)), 1)) + SUBSTRING(@columna, 4, LEN(@columna)) + ' = value; }' + CHAR(13) +
          CHAR(9) + CHAR(9) + '}' + CHAR(13) 
       

     

      FETCH NEXT FROM objCursor2 INTO @columna,@objRel      
   END

set @salida = @salida + '//Objetos relacionados con la entidad generada' + CHAR(13)
set @salida = @salida + '//' + CHAR(13);

set @salida = @salida + @propertyPrivateObjRel + CHAR(13)
set @salida = @salida + @propertyObjRel + CHAR(13)

CLOSE objCursor2;
DEALLOCATE objCursor2

SET @propertyPrivateObjRel = '';
SET @propertyObjRel = '';

DECLARE objCursor3 CURSOR FOR
SELECT     
   FK_Table  = FK.TABLE_NAME 
    
FROM 
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C 
    INNER JOIN 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK 
        ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME 
    INNER JOIN 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK 
        ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME 
    INNER JOIN 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU 
        ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME 
    INNER JOIN 
    ( 
        SELECT 
            i1.TABLE_NAME, i2.COLUMN_NAME 
        FROM 
            INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 
            INNER JOIN 
            INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 
            ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME 
            WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY' 
    ) PT 
    ON PT.TABLE_NAME = PK.TABLE_NAME  and (PK.TABLE_NAME != FK.TABLE_NAME)
-- optional:
WHERE PK.TABLE_NAME=@ObjectName  
OPEN objCursor3;
FETCH NEXT FROM objCursor3 INTO @columna



WHILE @@FETCH_STATUS = 0
   BEGIN
      SET @propertyPrivateObjRel = @propertyPrivateObjRel + CHAR(9) + CHAR(9) + 'private List<' + @columna + '> ' + LOWER(LEFT(@columna, 1)) + SUBSTRING(@columna, 2, LEN(@columna)) + ';' + CHAR(13);

      SET @propertyObjRel = @propertyObjRel + CHAR(9) + CHAR(9) + 'public List<' + @columna + '> ' +  @columna +
          CHAR(9) + CHAR(9) + '{' + CHAR(13) +
          CHAR(9) + CHAR(9) + CHAR(9) + 'get { return ' + LOWER(LEFT(@columna, 1)) + SUBSTRING(@columna, 2, LEN(@columna)) + '; }' + CHAR(13) +
       CHAR(9) + CHAR(9) + CHAR(9) + 'set { ' + LOWER(LEFT(@columna, 1)) + SUBSTRING(@columna, 2, LEN(@columna)) + ' = value; }' + CHAR(13) +
          CHAR(9) + CHAR(9) + '}' + CHAR(13) 
       

     

      FETCH NEXT FROM objCursor3 INTO @columna 
   END

set @salida = @salida + '//Listas relacionadas con la entidad generada' + CHAR(13)
set @salida = @salida + '//' + CHAR(13);

set @salida = @salida + @propertyPrivateObjRel + CHAR(13)
set @salida = @salida + @propertyObjRel + CHAR(13)

CLOSE objCursor3;
DEALLOCATE objCursor3


set @salida = @salida + CHAR(9) + '}' + CHAR(13)
set @salida = @salida + '}' + CHAR(13)

print @salida;
select @salida;
