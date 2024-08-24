use ReadRealm
GO

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'NoteVisibilities'))
BEGIN
	CREATE TABLE [dbo].NoteVisibilities(
		Id TINYINT IDENTITY,
		[Name] VARCHAR(20) NOT NULL,
		CONSTRAINT PK_NoteVisibility_Id PRIMARY KEY (Id)
	);

	INSERT INTO NoteVisibilities ([Name])
	VALUES ('Private'),
	('Shared'),
	('Public');
END

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Notes'))
BEGIN
	IF (EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'Private' AND Object_ID = Object_ID(N'Notes')))
	BEGIN  
		ALTER TABLE Notes
		DROP COLUMN [Private]
	END

	IF (NOT EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'NoteVisibilityId' AND Object_ID = Object_ID(N'Notes')))
	BEGIN  
		ALTER TABLE Notes
		ADD NoteVisibilityId TINYINT NOT NULL DEFAULT 1

		ALTER TABLE Notes
		ADD CONSTRAINT FK_Note_NoteVisibilityId FOREIGN KEY (NoteVisibilityId) REFERENCES NoteVisibilities(Id)
	END
END