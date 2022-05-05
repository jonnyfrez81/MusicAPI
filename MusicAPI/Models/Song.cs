using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Duration { get; set; }
        public DateTime UploadedDate { get; set; }
        public bool IsFeatured { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImageUrl  { get; set; }
        [NotMapped]
        public IFormFile AudioFile { get; set; }
        public string AudioUrl { get; set; }
        public int ArtistID { get; set; }

        //? means that albumId can be null
        public int? AlbumId { get; set; }
    }
}
