﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ReadRealmBackend.Models.Entities;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}