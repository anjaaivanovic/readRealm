USE ReadRealm
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'BookAuthors'))
BEGIN
    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookAuthor_BookId')))
    BEGIN
        ALTER TABLE BookAuthors
        DROP CONSTRAINT FK_BookAuthor_BookId;

        ALTER TABLE BookAuthors
        ADD CONSTRAINT FK_BookAuthor_BookId FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE;
    END

    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookAuthor_AuthorId')))
    BEGIN
        ALTER TABLE BookAuthors
        DROP CONSTRAINT FK_BookAuthor_AuthorId;

        ALTER TABLE BookAuthors
        ADD CONSTRAINT FK_BookAuthor_AuthorId FOREIGN KEY (AuthorId) REFERENCES Authors(Id);
    END
END

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Books'))
BEGIN
    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Book_BookTypeId')))
    BEGIN
        ALTER TABLE Books
        DROP CONSTRAINT FK_Book_BookTypeId;

        ALTER TABLE Books
        ADD CONSTRAINT FK_Book_BookTypeId FOREIGN KEY (TypeId) REFERENCES BookTypes(Id);
    END
END

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'BookGenres'))
BEGIN
    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookGenre_BookId')))
    BEGIN
        ALTER TABLE BookGenres
        DROP CONSTRAINT FK_BookGenre_BookId;

        ALTER TABLE BookGenres
        ADD CONSTRAINT FK_BookGenre_BookId FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE;
    END

    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookGenre_GenreId')))
    BEGIN
        ALTER TABLE BookGenres
        DROP CONSTRAINT FK_BookGenre_GenreId;

        ALTER TABLE BookGenres
        ADD CONSTRAINT FK_BookGenre_GenreId FOREIGN KEY (GenreId) REFERENCES Genres(Id);
    END
END

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'BookLanguages'))
BEGIN
    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookLanguage_BookId')))
    BEGIN
        ALTER TABLE BookLanguages
        DROP CONSTRAINT FK_BookLanguage_BookId;

        ALTER TABLE BookLanguages
        ADD CONSTRAINT FK_BookLanguage_BookId FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE;
    END

    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookLanguage_LanguageId')))
    BEGIN
        ALTER TABLE BookLanguages
        DROP CONSTRAINT FK_BookLanguage_LanguageId;

        ALTER TABLE BookLanguages
        ADD CONSTRAINT FK_BookLanguage_LanguageId FOREIGN KEY (LanguageId) REFERENCES Languages(Id);
    END
END

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'BookUsers'))
BEGIN
    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookUser_BookId')))
    BEGIN
        ALTER TABLE BookUsers
        DROP CONSTRAINT FK_BookUser_BookId;

        ALTER TABLE BookUsers
        ADD CONSTRAINT FK_BookUser_BookId FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE;
    END

    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_BookUser_StatusId')))
    BEGIN
        ALTER TABLE BookUsers
        DROP CONSTRAINT FK_BookUser_StatusId;

        ALTER TABLE BookUsers
        ADD CONSTRAINT FK_BookUser_StatusId FOREIGN KEY (StatusId) REFERENCES Statuses(Id);
    END
END

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Notes'))
BEGIN
    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Note_BookId')))
    BEGIN
        ALTER TABLE Notes
        DROP CONSTRAINT FK_Note_BookId;

        ALTER TABLE Notes
        ADD CONSTRAINT FK_Note_BookId FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE;
    END

    IF (EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Note_TypeId')))
    BEGIN
        ALTER TABLE Notes
        DROP CONSTRAINT FK_Note_TypeId;

        ALTER TABLE Notes
        ADD CONSTRAINT FK_Note_TypeId FOREIGN KEY (TypeId) REFERENCES NoteTypes(Id);
    END
END