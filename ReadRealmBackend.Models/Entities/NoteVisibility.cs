﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ReadRealmBackend.Models.Entities;

public partial class NoteVisibility
{
    public byte Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}