﻿using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Notes
{
    public class InsertNoteRequest
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Chapter { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Visibility { get; set; }

        [Required]
        public int TypeId { get; set; }

    }
}