select *from dbo.Standards

--SELECT *FROM dbo.Habilidades


INSERT INTO dbo.Standards VALUES
('B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB', 'ISO 9001:2008', 'Auditorias de Certificacion ISO 9001:2008', 1000, 1),
('129880AD-B3F9-4BC0-8DDA-71DDC04A80A2', 'ISO 14001:2004', 'Auditorias de Certificacion ISO 14001:2004', 1000, 2),
('88E5C2B5-388F-45D9-92DE-AB3E038A1029', 'ISO 22000:2005', 'Auditorias de Certificacion ISO 22000:2005', 1000, 3),
('408F5277-7A45-41B8-BBE4-35CE16637072', 'OHSAS', 'Auditorias de Certificacion OHSAS', 1000, 4),
('1AA623BB-4D67-4582-AFCD-039A7D69206A', 'HACCP', 'Auditorias de Certificacion HACCP', 1000, 5),
('C53AA56A-303A-49AA-AA83-1D2C78D55E7F', 'ISO IEC 27001:2013', 'Auditorias de Certificacion ISO IEC 27001:2013', '1000', 6), 
('26C2D21C-68E2-427A-8E0C-5DF9BA8EAE95', 'Courso Principal', 'Para Corsos que pueden aplicar a cualquier standard o que no necesariamente se derivan de un standard',0,7)

 
---ISO 9001:2008
---ISO 14001:2004
---ISO 22000:2005
---OHSAS
---HACCP
---ISO IEC 27001:2013

--[dbo].[procGenerateEntityClass3] 'Usuario'
SELECT *FROM dbo.usuario
SELECT *FROm dbo.Office
SELECT *FROm dbo.Departamento
SELECT *FROM dbo.Cliente;


INSERT INTO dbo.Office VALUES('C3AB8E8F-CAE4-4912-AC9B-C67F935BC023', 'Global Headquartes', 1)
INSERT INTO dbo.Office VALUES('4CB263AB-3D43-491B-B89C-46D1D4E1D315', 'Canada', 2)
INSERT INTO dbo.Office VALUES('37C0CA30-9FF3-4528-8D95-6BAD27708EB9', 'Turkey', 3)
INSERT INTO dbo.Departamento VALUES('FA0B528A-B1F9-48DF-81CD-6FD598E777C2', 'Desarrollo de Software', 'Desarrollo de Software', 1)
INSERT INTO dbo.Usuario VALUES(NEWID(), 'Sergio', 'salvarez', 'c893bad68927b457dbed39460e6afd62', 'Alvarez', 'Castellanos', 'Pedro Moreno 1677 Piso 3 Oficina 4', '3317882849', 'soporte@globalstd.com', 1.00, 0, 0, GETDATE(), 'FA0B528A-B1F9-48DF-81CD-6FD598E777C2','C3AB8E8F-CAE4-4912-AC9B-C67F935BC023' )


SELECT * FROM (select ROW_NUMBER() OVER(ORDER BY Orden ASC) AS 'RN',T.* from (SELECT *FROM dbo.Office) as T) as T2 WHERE 1=1 AND RN>= 1 AND RN<= 3 

INSERT INTO dbo.Country VALUES
('3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'M�xico'),
('7394F468-320B-47AA-99C6-19BD5BFDB00D', 'Kingdom of Saudi Arabia'),
('80C624EA-6A58-461D-A46B-1EB617E03483', 'United States'),
('ADBCC065-4EBE-4990-B599-207E404AFF75', 'Costa Rica'),
('5DCDB5B8-217B-4734-A96C-6307A93AA3A3', 'Turkey'),
('15B68E23-F70B-4E03-9519-8FF0F81D24B2', 'India'),
('8732345D-3D23-4AD4-8614-BA94FA978656', 'Colombia'),
('04E2F3CE-EE03-4751-A28C-E539F495700A', 'Pakistan'),
('F6D3355A-1B1D-44E2-BE9E-ED51A8ECDD3B', 'Canada')

DELETE dbo.States
SELECT *FROM dbo.States
ORDER BY Name

INSERT INTO dbo.States(stateKey, CountryFk, Name) VALUES
('253F8711-0F71-4AA2-B5E8-046D4C169C3C', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Michoac�n'),
('2C424A80-A2A4-4556-AFBF-04872A793AFA', '15B68E23-F70B-4E03-9519-8FF0F81D24B2', 'Uttar Pradesh'),	
('05104AD4-7B0C-4C17-A489-0FE988F909B7', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Chiuahua'),
('B4CA14DB-0AFB-49C6-8B45-100850E0BF4C', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Colima'),
('D91768AC-9794-4DAD-B8AC-136A915EF802', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Coahuila'),
('038BB84E-6245-4ECA-B08C-1BD3C1E9DFEE', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Morelos'),	
('E6357EE6-6C5E-4E94-9EB9-323406D916DA', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Durango'),
('799E2F6B-54FD-402E-83F3-3B820C883A11', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Campeche'),
('BCDF64AC-1554-46BD-BBAC-40A55B9D8BE0', '04E2F3CE-EE03-4751-A28C-E539F495700A', 'Lahore'),
('7E5B6617-EA76-43F1-8718-48C383B15C8C', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Sinaloa'),
('4DE775B6-FB42-48D9-B052-4C4200764849', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'San Luis Potos�'),	
('41A6E77D-FC09-46E8-8B2D-4C64882CD000', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Guanajuato'),
('B51F98BA-AF6A-4ADD-AB33-4F4C33F7DB2E', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Zacatecas'),
('3614F252-E226-41FB-BE20-4F61E972F03F', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Tlaxcala'),
('8999573C-EAEA-4056-BADC-5062FED87E56', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Sonora'),
('FEAE4044-4076-45B5-9800-542DECD9D751', '5DCDB5B8-217B-4734-A96C-6307A93AA3A3', 'Istanbul'),
('22A298F1-A417-4742-BFF6-58AD10A2305A', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Veracruz'),
('1D4987E9-FAD6-4A0E-9A53-5CA0728921C8', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Oaxaca'),
('05292F71-1305-4109-92F2-612C98139F5F', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Nayarit'),
('361BD110-091B-4AD7-8C2A-7473D4A0A4B2', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Distrito Federal'),
('C748A236-F58B-4472-8C2F-79ED2F8B5128', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Quer�taro'),
('846DABE8-97B4-4EA6-B55F-7CE84816B6B6', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Guerrero'),
('C48FEB8F-E2A0-431F-9C0D-895B979EFD3A', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Estado de M�xico'),	
('E8B7F92B-F53A-4B43-8695-899F60D4211C', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Hidalgo'),
('F32774FD-47B2-4CA9-B8BA-8EF15E0360C5', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Tamaulipas'),	
('05E831A9-0DFC-4124-8E05-93E407B82A6E', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Tabasco'),
('DC0CDE50-AA03-4365-B2D9-9863FDFD9BF1', '7394F468-320B-47AA-99C6-19BD5BFDB00D', 'Jeddah'),
('1F476C7B-C2A6-47F4-8A36-99FB0F27FD5B', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Puebla'),
('78D5A2D3-0439-40D5-A726-A03445FC472C', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Baja California Sur'),	
('975AA1DA-665E-4C3E-A3BF-A7C48EF47651', 'F6D3355A-1B1D-44E2-BE9E-ED51A8ECDD3B', 'Ontario'),
('9E5CB08B-C78C-49E4-8B95-B5768E1563A2', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Baja California'),	
('1042E119-DD3F-4F44-83D0-B7BA3AAFAA97', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Nuevo Le�n'),	
('0B705132-788B-4229-9E50-BC0717904C7A', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Chiapas'),	
('40F7F987-38C3-4C01-8448-C108BCBAF5A6', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Quintana Roo'),	
('A66562E8-C6E5-4E22-B992-D6718DD7884B', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Aguascalientes'),	
('E4F81D15-1485-4522-B242-EDA33CB02371', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Yucat�n'),	
('4419C90D-C5D8-4566-875C-F3455A773DCC', '3177E8C2-FBAA-424B-A60F-0BAF24C34F18', 'Jalisco'),
('ECCBE8F2-4F31-4DA7-A458-AB2B1D88D023', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Alabama'),
('DD75305E-6A85-456A-9C4A-624F250A0FFA', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Alaska'),
('AB3E28E7-2A00-441C-92D9-B0BC23BBF87A', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Arizona'),
('58A90515-9110-445B-8995-7B5B62E37EC3', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Arkansas'),
('F5D3EA2E-7FA8-408E-8C93-0B18BC588EC5', '80C624EA-6A58-461D-A46B-1EB617E03483', 'California'),
('EC93EB52-14B3-4E8F-AC1F-A0468DC73CEA', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Carolina del Norte'),
('39CE1985-3A4A-42B1-AC67-0618E87A5A59', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Carolina del Sur'),
('A977B7EA-3242-4292-BBA0-D004A960C6E7', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Colorado'),
('C05A89A3-E78F-4F72-B141-233CBD9F780B', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Connecticut'),
('2510D0A6-E6CD-4F1A-A8B7-F1376086450F', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Dakota del Norte'),
('C7C2893C-880B-4D4A-BDA6-A94AFA263824', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Dakota del Sur'),
('0456D28E-A278-4207-AC07-CAD29588BAD5', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Delaware'),
('109E0826-8300-46FF-ADBC-35EE5DC6052C', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Florida'),
('7256B917-3EB0-42C0-8D55-AB6A80982A94', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Georgia'),
('5B151025-9A36-4E3C-911C-1350D66B3EE5', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Haw�i'),
('BDDE1650-0876-49FA-B045-50C376E6B707', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Idaho'),
('99D6E757-B20B-4281-A7D7-5913C5725312', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Illinois'),
('B78A2B9B-252C-49DC-AA62-B2581D7D7C9B', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Indiana'),
('219F95CD-1EA2-47FA-93AF-F66409AF4D87', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Iowa'),
('E93C10B1-E79D-4F8E-B908-537F4265B8A0', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Kansas'),
('19E31B19-41E6-478D-8994-8A2A5F63EDE5', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Kentucky'),
('87B06505-A9BD-4CE5-B984-84F5A593DAAD', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Luisiana'),
('4F8AC9EC-7786-42F2-B4C7-85BCB4BD40ED', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Maine'),
('9EB7E7DA-6779-4E62-A062-EBE55CEA89BF', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Maryland'),
('5E221BA4-8072-4C3C-A404-0FFEB834AD50', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Massachusetts'),
('A1B0A6CE-FAAC-4913-91F5-CEB1D76ED364', '80C624EA-6A58-461D-A46B-1EB617E03483', 'M�chigan'),
('1C548F1C-2253-4FE9-8A0B-679954D8A102', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Minnesota'),
('55EEC55D-E03B-41B3-A045-7D351DDE1499', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Misisipi'),
('8E88BC8F-8C7E-4D5A-A229-31EE4D68BC1F', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Misuri'),
('1DAAF05C-7056-4D38-BF09-A244261C3196', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Montana'),
('94C686B6-0FD2-464B-A7D5-C155150173D0', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Nebraska'),
('188CEDCE-EA70-4025-A11A-97366DB237D7', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Nevada'),
('3C3A3F52-7FF4-44DF-B4B9-AC7FD87F8063', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Nueva Jersey'),
('5352D949-5898-4FC3-B834-B7684E8B281E', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Nueva York'),
('DF5E8F47-80C5-49D1-ABD1-442204E51AF2', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Nuevo Hampshire'),
('1F5F3ED9-160A-4614-B530-21A61A232D28', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Nuevo M�xico'),
('D9AE5337-99A5-4AB2-8F5D-42C746C5A27A', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Ohio'),
('61272AD6-6A9C-436C-904C-4D242EE49E33', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Oklahoma'),
('92D2E393-D07E-4780-8A67-FDD6B3C1C1FC', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Oreg�n'),
('248B49E4-81AE-466E-BEBC-6348A02ADAF6', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Pensilvania'),
('EBAB3725-1DAF-4D4E-B819-89DAFD5732B3', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Rhode Island'),
('65325944-B767-4AD1-BAA4-743BE9412301', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Tennessee'),
('A695F5B7-07A0-43B7-B35F-6F2EBEC508F5', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Texas'),
('846B0F38-8ECE-45B5-9D5C-91723253015E', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Utah'),
('DA0840F1-7D63-4E3F-8C80-66F943381CDE', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Vermont'),
('C456AA43-32D9-4A78-8727-135DDCCE6D72', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Virginia'),
('23893F3B-4903-4D32-ABCC-D4C432270EA0', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Virginia Occidental'),
('55F49691-4F5E-4ADF-AE7C-E21D8029D71B', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Washington'),
('2427EE94-7CEE-4747-B4D8-E985A2AE89A9', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Wisconsin'),
('FFA0568C-5DA2-4DDF-BC51-1E1ACED3D298', '80C624EA-6A58-461D-A46B-1EB617E03483', 'Wyoming')

GO 
SELECT *FROM dbo.SourceClient
GO 
INSERT INTO dbo.SourceClient VALUES
('7B36B84C-E28F-456B-9664-1069F5B7C3B0', 'Chat', 1),
('0EAE1E23-FF38-46A6-B983-106EB4C3781F', 'Consultor', 2),
('3E5C4A8B-2597-4BCF-8431-2E205F514D99', 'Curso/Conferencia',3),
('D1342ACB-30F4-4961-80B5-36F141FF2EB9', 'Expos', 4),
('305FFC40-975C-40E8-9265-3927D4196D87', 'Llamada a Global', 5),
('59251F33-4E13-479D-A1C8-4CCA403210AC', 'Networking (C�maras de Comercio)', 6),
('2EAF5C15-A5D2-44CE-BDD7-553DCFE6F96A', 'Prospectaci�n Directa', 7),
('F4A95EB4-D046-4C72-89A9-7C334D99CA66', 'Publicidad', 8),
('FEBC5015-E258-43E7-B27C-7E23EAE3E910', 'Redes Sociales', 9),
('BD8BEB4B-90FA-40CF-A832-9AFCC7479C73', 'Recomendaci�n Auditor/Empleado', 10),
('4A4E7242-BB4C-4E55-9E2D-B5AB62F729B1', 'Facebook', 11),
('9FFAC2A8-BAA7-44C9-A6F5-BC695FE95950', 'Recomendaci�n Empresa/Cliente', 12),
('C85DD9BE-02E6-4548-959A-C597190C6D76', 'Twitter', 13),
('563ABF46-25B4-4C35-919E-C830B5D203C4', 'Web (B�squeda)', 14)
GO 
SELECT *FROM dbo.InterestType
GO 
INSERT INTO dbo.InterestType VALUES
('1B74F20A-975D-430F-A105-2AE0B428EDF5', 'Certificaci�n'),
('E9EB421B-C84E-4ED0-9E62-70D8488CB379', 'Capacitaci�n'),
('347AC229-609C-4D9D-B865-7B53A8CA37E4', 'Auditorias 2da Parte'),
('D9D50CA4-CBAA-43E0-A12F-FB4B8B92E11E', 'Certificaci�n & Capacitaci�n')
Go
INSERT INTO dbo.ContactType VALUES
('2870BE2F-E49D-4614-9708-22955A6A0E95', 'Finanzas', 1),
('5918A942-2EB4-4123-BCA0-752C862B00E1', 'Calidad', 1),
('570653AD-3A83-44B3-9A17-82F2E88FB104', 'Direcci�n', 1),
('8A8B368C-4C8B-43AB-BD6F-E7F0B9C81A70', 'Sistemas', 1)
GO 
INSERT INTO dbo.Course VALUES
('A40621B1-3307-43AF-BF64-126F3DBC005B', 'Auditor interno Sin Acreditar'),
('48CE844C-B748-4BF1-B449-1A391A521CD8', 'Auditor interno acreditado'),
('D95B82CD-E243-48D6-811F-4D0019FD864B', 'Auditor Lider Acreditado'),
('F71C86DF-E97B-4B8B-AB12-57EC18DD4347', 'Auditor Lider Sin Acreditar'),
('748FB26D-ADE2-4FF2-ADE6-717A7A2A6322', 'Requerimientos Acreditado'),
('EDDB1589-2FF7-4ABB-954F-7364B600518C', 'Requerimiento Sin Acreditar'),
('976AF37E-2FDC-426A-836A-95B5C731583E', 'Aplicaci�n de 5s'),
('63BCEBF0-D0FF-4B9F-B05A-C3057BCF955E', 'Herramientas de las 8 disciplinas'),
('C97020E8-225D-4440-B2EB-CF3B4283A14A', 'Core Tools'),
('3493D18F-F58C-41A9-A20E-D42923EA1F76', 'KAISEN')
GO 
SELECT *FROM dbo.Standards
GO 
SELECT *FROM dbo.StandardCourse 
GO
INSERT INTO dbo.StandardCourse VALUES
(NEWID(), 'A40621B1-3307-43AF-BF64-126F3DBC005B', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 
(NEWID(), '48CE844C-B748-4BF1-B449-1A391A521CD8', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 
(NEWID(), 'D95B82CD-E243-48D6-811F-4D0019FD864B', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 
(NEWID(), 'F71C86DF-E97B-4B8B-AB12-57EC18DD4347', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 
(NEWID(), '748FB26D-ADE2-4FF2-ADE6-717A7A2A6322', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 
(NEWID(), 'EDDB1589-2FF7-4ABB-954F-7364B600518C', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 
(NEWID(), '976AF37E-2FDC-426A-836A-95B5C731583E', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 
(NEWID(), '63BCEBF0-D0FF-4B9F-B05A-C3057BCF955E', 'B565CE9E-3A43-4AAC-B70E-C74CFC03F9FB'), 

(NEWID(), 'A40621B1-3307-43AF-BF64-126F3DBC005B', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 
(NEWID(), '48CE844C-B748-4BF1-B449-1A391A521CD8', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 
(NEWID(), 'D95B82CD-E243-48D6-811F-4D0019FD864B', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 
(NEWID(), 'F71C86DF-E97B-4B8B-AB12-57EC18DD4347', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 
(NEWID(), '748FB26D-ADE2-4FF2-ADE6-717A7A2A6322', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 
(NEWID(), 'EDDB1589-2FF7-4ABB-954F-7364B600518C', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 
(NEWID(), '976AF37E-2FDC-426A-836A-95B5C731583E', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 
(NEWID(), '63BCEBF0-D0FF-4B9F-B05A-C3057BCF955E', '129880AD-B3F9-4BC0-8DDA-71DDC04A80A2'), 

(NEWID(), 'A40621B1-3307-43AF-BF64-126F3DBC005B', '408F5277-7A45-41B8-BBE4-35CE16637072'), 
(NEWID(), '48CE844C-B748-4BF1-B449-1A391A521CD8', '408F5277-7A45-41B8-BBE4-35CE16637072'), 
(NEWID(), 'D95B82CD-E243-48D6-811F-4D0019FD864B', '408F5277-7A45-41B8-BBE4-35CE16637072'), 
(NEWID(), 'F71C86DF-E97B-4B8B-AB12-57EC18DD4347', '408F5277-7A45-41B8-BBE4-35CE16637072'), 
(NEWID(), '748FB26D-ADE2-4FF2-ADE6-717A7A2A6322', '408F5277-7A45-41B8-BBE4-35CE16637072'), 
(NEWID(), 'EDDB1589-2FF7-4ABB-954F-7364B600518C', '408F5277-7A45-41B8-BBE4-35CE16637072'), 
(NEWID(), '976AF37E-2FDC-426A-836A-95B5C731583E', '408F5277-7A45-41B8-BBE4-35CE16637072'), 
(NEWID(), '63BCEBF0-D0FF-4B9F-B05A-C3057BCF955E', '408F5277-7A45-41B8-BBE4-35CE16637072'), 

(NEWID(), 'A40621B1-3307-43AF-BF64-126F3DBC005B', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'), 
(NEWID(), '48CE844C-B748-4BF1-B449-1A391A521CD8', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'), 
(NEWID(), 'D95B82CD-E243-48D6-811F-4D0019FD864B', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'), 
(NEWID(), 'F71C86DF-E97B-4B8B-AB12-57EC18DD4347', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'), 
(NEWID(), '748FB26D-ADE2-4FF2-ADE6-717A7A2A6322', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'), 
(NEWID(), 'EDDB1589-2FF7-4ABB-954F-7364B600518C', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'), 
(NEWID(), '976AF37E-2FDC-426A-836A-95B5C731583E', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'), 
(NEWID(), '63BCEBF0-D0FF-4B9F-B05A-C3057BCF955E', '88E5C2B5-388F-45D9-92DE-AB3E038A1029'),

(NEWID(), 'C97020E8-225D-4440-B2EB-CF3B4283A14A', '26C2D21C-68E2-427A-8E0C-5DF9BA8EAE95'),
(NEWID(), '3493D18F-F58C-41A9-A20E-D42923EA1F76', '26C2D21C-68E2-427A-8E0C-5DF9BA8EAE95')






