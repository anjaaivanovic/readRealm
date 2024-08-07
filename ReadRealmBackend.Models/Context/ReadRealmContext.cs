﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.Models.Context;

public partial class ReadRealmContext : DbContext
{
    public ReadRealmContext(DbContextOptions<ReadRealmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookType> BookTypes { get; set; }

    public virtual DbSet<BookUser> BookUsers { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<NoteType> NoteTypes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Author_Id");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BookId");

            entity.Property(e => e.CoverImage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Isbn).HasColumnName("ISBN");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Type).WithMany(p => p.Books)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Book_BookTypeId");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookAuthor_AuthorId"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookAuthor_BookId"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("PK_BookAuthor_BookId_AuthorId");
                        j.ToTable("BookAuthors");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookGenre_GenreId"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookGenre_BookId"),
                    j =>
                    {
                        j.HasKey("BookId", "GenreId").HasName("PK_BookGenre_BookId_GenreId");
                        j.ToTable("BookGenres");
                    });

            entity.HasMany(d => d.Languages).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookLanguage",
                    r => r.HasOne<Language>().WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookLanguage_LanguageId"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookLanguage_BookId"),
                    j =>
                    {
                        j.HasKey("BookId", "LanguageId").HasName("PK_BookLanguage_BookId_LanguageId");
                        j.ToTable("BookLanguages");
                    });
        });

        modelBuilder.Entity<BookType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BookType_Id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BookUser>(entity =>
        {
            entity.HasKey(e => new { e.BookId, e.UserId }).HasName("PK_BookUser_BookId_UserId");

            entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Book).WithMany(p => p.BookUsers)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookUser_BookId");

            entity.HasOne(d => d.Status).WithMany(p => p.BookUsers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_BookUser_StatusId");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => new { e.FirstUserId, e.SecondUserId }).HasName("PK_Friend_FirstUserId_SecondUserId");
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => new { e.SenderUserId, e.ReceiverUserId }).HasName("PK_FriendRequest_SenderUserId_ReceiverUserId");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GenreId");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Language_Id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => new { e.BookId, e.UserId, e.Chapter, e.DatePosted }).HasName("PK_Note_BookId_UserId_Chapter_DatePosted");

            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.Book).WithMany(p => p.Notes)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Note_BookId");

            entity.HasOne(d => d.Type).WithMany(p => p.Notes)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Note_TypeId");
        });

        modelBuilder.Entity<NoteType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NoteType_Id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Status_Id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}