use ReadRealm
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'BookUsers'))
BEGIN
	IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'EndDate' AND Object_ID = Object_ID(N'BookUsers') AND is_nullable=0)
	BEGIN  
		ALTER TABLE BookUsers
		ALTER COLUMN EndDate DATE NULL
	END
END