use ReadRealm
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Notes'))
BEGIN
	IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'Private' AND Object_ID = Object_ID(N'Notes'))
	BEGIN  
		ALTER TABLE Notes
		ALTER COLUMN [Private] BIT NOT NULL
	END

	IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'TypeId' AND Object_ID = Object_ID(N'Notes') AND is_nullable=1)
	BEGIN  
		ALTER TABLE Notes
		ALTER COLUMN TypeId INT NOT NULL
	END
END