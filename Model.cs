using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace lab63312
{
    public class MovieContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database.db");
        }
        public DbSet<Movie> Movies {get; set;}
        public DbSet<Studio> Studios {get; set;}
    }

    public class Movie
    {
        public int MovieID {get; set;}
        public string Title {get; set;}
        public string Genre {get; set;}
        public int StudioId {get; set;}
        public Studio Studio {get; set;}
        
        public override string ToString()
        {
            return $"Movie {MovieID} - {Title}";
        }
    }

    public class Studio
    {
        public int StudioId {get; set;}
        public string Name {get; set;}
        public List<Movie> Movies {get; set;}

        public override string ToString()
        {
            return $"Studio {StudioId} - {Name}";
        }
    }
}