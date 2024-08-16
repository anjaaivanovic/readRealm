IF (NOT EXISTS (SELECT 1 FROM sys.databases WHERE name='ReadRealm'))
BEGIN
	CREATE DATABASE ReadRealm
END
GO

USE ReadRealm
GO

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'BookTypes'))
BEGIN
	CREATE TABLE [dbo].[BookTypes](
		Id INT IDENTITY,
		Name VARCHAR(50) NOT NULL,
		CONSTRAINT PK_BookType_Id PRIMARY KEY (Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Books'))
BEGIN
	CREATE TABLE [dbo].[Books](
		Id INT IDENTITY,
		Title VARCHAR(200) NOT NULL,
		Published DATE NOT NULL,
		[Description] VARCHAR(1000) NOT NULL,
		ChapterCount INT NOT NULL,
		[WordCount] INT NOT NULL,
		ISBN VARCHAR(20) NOT NULL,
		TypeId INT,
		CONSTRAINT PK_BookId PRIMARY KEY (Id),
		CONSTRAINT FK_Book_BookTypeId FOREIGN KEY (TypeId) REFERENCES BookTypes(Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Genres'))
BEGIN
	CREATE TABLE [dbo].[Genres](
		Id INT IDENTITY,
		Name VARCHAR(50) NOT NULL,
		CONSTRAINT PK_GenreId PRIMARY KEY (Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'BookGenres'))
BEGIN
	CREATE TABLE [dbo].[BookGenres](
		BookId INT,
		GenreId INT,
		CONSTRAINT PK_BookGenre_BookId_GenreId PRIMARY KEY (BookId, GenreId),
		CONSTRAINT FK_BookGenre_BookId FOREIGN KEY (BookId) REFERENCES Books(Id),
		CONSTRAINT FK_BookGenre_GenreId FOREIGN KEY (GenreId) REFERENCES Genres(Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Authors'))
BEGIN
	CREATE TABLE [dbo].[Authors](
		Id INT IDENTITY,
		FirstName VARCHAR(50) NOT NULL,
		LastName VARCHAR(50) NOT NULL,
		CONSTRAINT PK_Author_Id PRIMARY KEY (Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'BookAuthors'))
BEGIN
	CREATE TABLE [dbo].[BookAuthors](
		BookId INT,
		AuthorId INT,
		CONSTRAINT PK_BookAuthor_BookId_AuthorId PRIMARY KEY (BookId, AuthorId),
		CONSTRAINT FK_BookAuthor_BookId FOREIGN KEY(BookId) REFERENCES Books(Id),
		CONSTRAINT FK_BookAuthor_AuthorId FOREIGN KEY(AuthorId) REFERENCES Authors(Id),
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Languages'))
BEGIN
	CREATE TABLE [dbo].[Languages](
		Id INT IDENTITY,
		Name VARCHAR(50) NOT NULL,
		CONSTRAINT PK_Language_Id PRIMARY KEY (Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'BookLanguages'))
BEGIN
	CREATE TABLE [dbo].[BookLanguages](
		BookId INT,
		LanguageId INT,
		CONSTRAINT PK_BookLanguage_BookId_LanguageId PRIMARY KEY (BookId, LanguageId),
		CONSTRAINT FK_BookLanguage_BookId FOREIGN KEY(BookId) REFERENCES Books(Id),
		CONSTRAINT FK_BookLanguage_LanguageId FOREIGN KEY(LanguageId) REFERENCES Languages(Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Statuses'))
BEGIN
	CREATE TABLE [dbo].[Statuses](
		Id INT IDENTITY,
		Name VARCHAR(50) NOT NULL,
		CONSTRAINT PK_Status_Id PRIMARY KEY (Id)
	);
END


IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'BookUsers'))
BEGIN
	CREATE TABLE [dbo].[BookUsers](
		BookId INT,
		UserId INT,
		CurrentChapter INT NOT NULL,
		StartDate DATE NOT NULL,
		EndDate DATE NOT NULL,
		Rating DECIMAL CHECK(Rating BETWEEN 0 AND 5) NULL,
		StatusId INT,
		CONSTRAINT PK_BookUser_BookId_UserId PRIMARY KEY (BookId, UserId),
		CONSTRAINT FK_BookUser_BookId FOREIGN KEY(BookId) REFERENCES Books(Id),
		CONSTRAINT FK_BookUser_StatusId FOREIGN KEY (StatusId) REFERENCES Statuses(Id)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'NoteTypes'))
BEGIN
	CREATE TABLE [dbo].[NoteTypes](
		Id INT IDENTITY,
		Name VARCHAR(50) NOT NULL,
		CONSTRAINT PK_NoteType_Id PRIMARY KEY (Id)
	);
END


IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Notes'))
BEGIN
	CREATE TABLE [dbo].[Notes](
		UserId INT NOT NULL,
		BookId INT,
		Chapter INT NOT NULL,
		DatePosted DATE NOT NULL,
		[Text] VARCHAR(1000) NOT NULL,
		[Private] INT NOT NULL,
		TypeId INT,
		CONSTRAINT PK_Note_BookId_UserId_Chapter_DatePosted PRIMARY KEY (BookId, UserId, Chapter, DatePosted),
		CONSTRAINT FK_Note_BookId FOREIGN KEY(BookId) REFERENCES Books(Id),
		CONSTRAINT FK_Note_TypeId FOREIGN KEY(TypeId) REFERENCES NoteTypes(Id),
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'Friends'))
BEGIN
	CREATE TABLE [dbo].[Friends](
		FirstUserId INT NOT NULL,
		SecondUserId INT NOT NULL,
		CONSTRAINT PK_Friend_FirstUserId_SecondUserId PRIMARY KEY (FirstUserId, SecondUserId)
	);
END

IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'FriendRequests'))
BEGIN
	CREATE TABLE [dbo].[FriendRequests](
		SenderUserId INT NOT NULL,
		ReceiverUserId INT NOT NULL,
		CONSTRAINT PK_FriendRequest_SenderUserId_ReceiverUserId PRIMARY KEY (SenderUserId, ReceiverUserId)
	);
END