use ReadRealm
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Books'))
BEGIN
	IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'BriefDescription' AND Object_ID = Object_ID(N'Books'))
	BEGIN  
		ALTER TABLE Books
		ALTER COLUMN BriefDescription VARCHAR(300)
	END

	IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'Description' AND Object_ID = Object_ID(N'Books'))
	BEGIN  
		ALTER TABLE Books
		ALTER COLUMN [Description] VARCHAR(MAX)
	END
END