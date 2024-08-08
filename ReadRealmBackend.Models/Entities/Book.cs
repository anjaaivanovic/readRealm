﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ReadRealmBackend.Models.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateOnly Published { get; set; }

    public string Description { get; set; }

    public int ChapterCount { get; set; }

    public int WordCount { get; set; }

    public string Isbn { get; set; }

    public int? TypeId { get; set; }

    public virtual ICollection<BookUser> BookUsers { get; set; } = new List<BookUser>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual BookType Type { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Language> Languages { get; set; } = new List<Language>();
}