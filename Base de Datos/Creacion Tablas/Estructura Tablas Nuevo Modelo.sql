USE [DbGlobalCertification]
GO
CREATE TABLE dbo.Standards(
	StandardKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(50), 
	Description VARCHAR(250), 
	Cost Numeric Not null, 
	Orden INT
)
GO 
CREATE TABLE dbo.IAF(
	IAFKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(50), 
	Description VARCHAR(250), 
	Orden INT)
GO 
CREATE TABLE dbo.StandardIAF(
	StandardIAFKey UNIQUEIDENTIFIER PRIMARY KEY, 
	StandardFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Standards (StandardKey), 
	IAFFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.IAF (IAFKey)
	)
GO
CREATE TABLE dbo.MD5QMS(
	MD5QMSKey UNIQUEIDENTIFIER PRIMARY KEY, 
	StageI DECIMAL(5,2), 
	StageII DECIMAL(5,2), 
	Recertification DECIMAL(5,2), 
	Seg DECIMAL(5,2), 
	Annual DECIMAL(5,2), 
	High INTEGER, 
	Low INTEGER,
	StandardFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Standards(StandardKey)
	)
GO
CREATE TABLE dbo.SchemeMoney(
	SchemeMoneyKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(250),
	Orden INTEGER
)
GO
CREATE TABLE dbo.DurationAdjustment(
	DurationAdjustmentKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(250), 
	Description VARCHAR(500), 
	Percentage DECIMAL(5,2)
)
GO
CREATE TABLE dbo.SchemeCertification(
	SchemeCertificationKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(MAX), 
	Orden INTEGER
)
GO
CREATE TABLE dbo.Office(
	OfficeKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(250), 
	Orden INT
)
GO
CREATE TABLE dbo.TypeCertification(
	TypeCertificationKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(50),
	Orden int
)
GO
CREATE TABLE dbo.Country(
	Countrykey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(MAX)
)
GO
CREATE TABLE dbo.States(
	StateKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(250) NOT NULL,
	CountryFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Country(Countrykey)
)
GO
CREATE TABLE dbo.Departamento(
	DepartamentoKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(50), 
	Description VARCHAR(300), 
	Orden int
)
GO
CREATE TABLE dbo.Rights(
	RightKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(50), 
	Description VARCHAR(250), 
	Orden INT
)
GO
CREATE TABLE dbo.Usuario(
	UsuarioKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Nombres VARCHAR(50),
	Nickname VARCHAR(25) NOT NULL, 
	password VARCHAR(32) NOT NULL,
	ApPaterno VARCHAR(50), 
	ApMaterno VARCHAR(50), 
	Domicilio VARCHAR(300),
	Telefono VARCHAR(300), 
	Correo VARCHAR(300), 
	CuotaPago DECIMAL(5,2), 
	Baja Bit,
	Bilingue Bit, 
	CreationDate DATE,	
	DepartamentoFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Departamento(DepartamentoKey),
	OfficeFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Office(OfficeKey)
)
GO
CREATE TABLE dbo.SourceClient(
	SourceClientKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(100) Not Null,
	Orden int
)
GO
CREATE TABLE dbo.Interest(
	InterestKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(120) Not Null
)
GO 
CREATE TABLE dbo.StatusCliente(
	StatusClienteKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(150) NOT NULL, 
	Orden Int
)
GO
CREATE TABLE dbo.Cliente(
	Clientekey UNIQUEIDENTIFIER PRIMARY KEY, 
	ClaveCliente VARCHAR(250), 
	NombreCliente VARCHAR(MAX), 
	RazonSocial VARCHAR(max),
	RFC VARCHAR(13), 
	Street VARCHAR(500), 
	Suburb VARCHAR(500),
	Delegation VARCHAR(500),
	Town VARCHAR(500),
	Zip VARCHAR(10),
	NumExt VARCHAR(25), 
	NumInt VARCHAR(25), 
	WebSite VARCHAR(250), 
	Baja Bit, 
	Perfil VARCHAR(mAX), 
	ArchivoPerfil varchar(max), 
	NickName VARCHAR(25) NOT NULL, 
	Pass VARCHAR(32) NOT NULL, 
	IsCliente Bit Default 0, 
	Recoment VARCHAR(150),
	TypeCertificationFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES TypeCertification(TypeCertificationKey),
	StateFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES States(StateKey), 
	OfficeFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Office(OfficeKey),
	DateProspect DATETIME, 
	DateCliente DATETIME, 
	SourceClientFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.SourceClient(SourceClientKey),
	InterestFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Interest(InterestKey),
	StatusClienteFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.StatusCliente(StatusClienteKey)
)
GO
CREATE TABLE dbo.AplicationForm(
	AplicationFormKey UNIQUEIDENTIFIER PRIMARY KEY, 
	TimeJustification VARCHAR(500),
	ExchangeRate INTEGER, 
	IsDesigned Bit, 
	CreationDate DATETIME,
	MD5QMSFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES MD5QMS(MD5QMSKey), 
	SchemeCertificationFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES SchemeCertification(SchemeCertificationKey), 
	ClienteFk UNIQUEiDENTIFIER FOREIGN KEY REFERENCES Cliente(Clientekey), 
	SchemeMoneyFk UNIQUEiDENTIFIER FOREIGN KEY REFERENCES dbo.SchemeMoney(SchemeMoneyKey)
)
GO
CREATE TABLE DurationAdjustmentAplicationForm(
	DurationAdjustmentAplicationFormKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Percentage DECIMAL(5,2), 
	AplicationFormFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES AplicationForm(AplicationFormKey), 
	DurationAdjustmentFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES DurationAdjustment(DurationAdjustmentKey)
)
GO
CREATE TABLE dbo.Region(
	RegionKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(50), 
	Description VARCHAR(500),
	Orden INT
)
GO
CREATE TABLE dbo.Site(
	SiteKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(MAX) NOT NULL, 
	Street VARCHAR(MAX),
	NumExt VARCHAR(50),
	NumInt VARCHAR(50),
	Suburb VARCHAR(MAX),
	Delegation VARCHAR(MAX),
	Town VARCHAR(MAX),
	Zip VARCHAR(50),
	EmployeNumber INT NOT NULL, 
	IsPrincipal Bit, 
	GooglePoint Varchar(50),
	StateFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.States(StateKey),  
	ClienteFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Cliente(ClienteKey),  
	RegionFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Region(RegionKey)
)
GO
CREATE TABLE dbo.ContactType(
	ContactTypeKey UNIQUEIDENTIFIER PRIMARY KEY,
	Name VARCHAR(250), 
	Orden INT
)
GO
CREATE TABLE dbo.ContactSite(
	ContactSiteKey UNIQUEIDENTIFIER PRIMARY KEY,
	Name VARCHAR(300), 
	Phone VARCHAR(300), 
	Email VARCHAR(300), 
	Position VARCHAR(300), 
	StartDate DATETIME,
	SiteFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Site(SiteKey),
	ContactTypeKey UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.ContactType(ContactTypeKey) 
)
GO 
CREATE TABLE dbo.StatusIAF(
	StatusIAFKey UNIQUEIDENTIFIER PRIMARY KEY,
	Name VARCHAR(100), 
	Ordern INT
)
GO
CREATE TABLE dbo.StandardIAFUsuario(
	StandardIAFUsuariokey UNIQUEIDENTIFIER PRIMARY KEY,
	DateLiberation DATETIME, 
	StandardIAFFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.StandardIAF(StandardIAFKey), 
	StatusIAFFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.StatusIAF(StatusIAFKey), 
	UsuarioFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Usuario(UsuarioKey)
)
GO 
CREATE TABLE dbo.TypeSkill(
	TypeSkillKey UNIQUEIDENTIFIER PRIMARY KEY,
	Name VARCHAR(250), 
	Description VARCHAR(500), 
	Orden INT
)
GO
CREATE TABLE dbo.TypeSkillStandard(
	TypeSkillStandardKey UNIQUEIDENTIFIER PRIMARY KEY,
	StandardFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Standards(StandardKey), 
	TypeSkillFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.TypeSkill(TypeSkillKey)
)
GO 
CREATE TABLE dbo.SkillTable(
	SkillTableKey UNIQUEIDENTIFIER PRIMARY KEY,
	Point DECIMAL(5,2), 
	StandardIAFUsuarioFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.StandardIAFUsuario(StandardIAFUsuarioKey), 
	TypeSkillStandardFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.TypeSkillStandard(TypeSkillStandardKey), 
	Orden INT
)
GO 
CREATE TABLE dbo.SkillEvidence(
	SkillEvidenceKey UNIQUEIDENTIFIER PRIMARY KEY,
	Description VARCHAR(150), 
	DateEvidence DATE, 
	DateUpload DATE, 
	FileName VARCHAR(150)
)
GO 
CREATE TABLE dbo.SkillTableSkillEvidence(
	SkillTableSkillEvidenceKey UNIQUEIDENTIFIER PRIMARY KEY,
	SkillEvidenceFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.SkillEvidence(SkillEvidenceKey), 
	SkillTableFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.SkillTable(SkillTableKey)
)
GO 
--ADD
CREATE TABLE dbo.Course(
	courseKey UNIQUEIDENTIFIER PRIMARY KEY, 
	Name VARCHAR(250) Not Null 
)
Go 
CREATE TABLE dbo.StandardCourse(
	StandardCourseKey UNIQUEIDENTIFIER PRIMARY KEY, 
	CourseFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Course(CourseKey), 
	StandardFk UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Standards(StandardKey)
)
GO
