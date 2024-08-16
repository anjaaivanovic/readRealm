use ReadRealm
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Books'))
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'BriefDescription' AND Object_ID = Object_ID(N'Books')))
	BEGIN  
		ALTER TABLE Books
		ADD BriefDescription VARCHAR(100) NOT NULL
	END
END