﻿using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;

namespace MusicAPI.Data
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {

        }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }


        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Song>().HasData(
        //        new Song
        //        {
        //            Id = 1,
        //            Title = "Willow",
        //            Language = "English",
        //            Duration = "4:35"
        //        },
        //          new Song
        //          {
        //              Id = 2,
        //              Title = "Despacito",
        //              Language = "Spanish",
        //              Duration = "4:15"
        //          }
        //        );
        //}
    }
}
