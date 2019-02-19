using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace lab63312
{
    class Program
    {
        static void Main(string[] args)
        {
            // Delete the database every time to ensure that it won't place repeats in the database
            using (var db = new MovieContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            // Create the Studio 20th Century Fox and add movies to it
            using (var db = new MovieContext())
            {
                Studio studio = new Studio
                {
                    Name = "20th Century Fox",
                    Movies = new List<Movie>
                    {
                        new Movie { Title = "Avatar", Genre = "Action"},
                        new Movie { Title = "Deadpool", Genre = "Action"},
                        new Movie { Title = "Apollo 13", Genre = "Drama"},
                        new Movie { Title = "The Martian", Genre = "Sci-Fi"}
                    }
                };

                db.Add(studio);
                db.SaveChanges();
            }

            // Create a second studio called Universal Pictures
            using (var db = new MovieContext())
            {
                Studio studio2 = new Studio { Name = "Universal Pictures"};

                db.Add(studio2);
                db.SaveChanges();
            }

            // Add a movie to the new Universal Pictures studio
            using (var db = new MovieContext())
            {
                Movie movies = new Movie { Title = "Jurassic Park", Genre = "Action"};
                Studio studioToUpdate = db.Studios.Include(b => b.Movies).Where(b => b.Name == "Universal Pictures").First();
                studioToUpdate.Movies.Add(movies);
                db.SaveChanges();
            }

            // Move the movie Apollo 13 from Fox Studio to Universal Pictures
            using (var db = new MovieContext())
            {
                Movie movies = db.Movies.Where(m => m.Title == "Apollo 13").First();
                movies.Studio = db.Studios.Where(s => s.Name == "Universal Pictures").First();
                db.SaveChanges();
            }

            // Remove the movie Deadpool entirely from the database
            using (var db = new MovieContext())
            {
                Movie movieToRemove = db.Movies.Where(m => m.Title == "Deadpool").First();
                db.Remove(movieToRemove);
                db.SaveChanges();
            }

            

            // How to view the current database
            using (var db = new MovieContext())
                {
                    var studios = db.Studios.Include(m => m.Movies);
                    foreach (var m in studios)
                    {
                        Console.WriteLine(m);
                        foreach(var s in m.Movies)
                        {
                            Console.WriteLine("\t" + s);
                        }
                    }
                }

            
        }
    }
}
