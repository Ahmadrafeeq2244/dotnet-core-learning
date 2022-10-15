﻿namespace my_books.Data.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; }
        public String Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string CoverUrl { get; set; }
    }
}