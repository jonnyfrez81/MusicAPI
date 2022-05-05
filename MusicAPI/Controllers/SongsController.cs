﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Helpers;
using MusicAPI.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public SongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Song song)
        {

            var imageUrl = await FileHelper.UploadImage(song.Image);
            song.ImageUrl = imageUrl;

            var audioUrl = await FileHelper.UploadFile(song.AudioFile);
            song.AudioUrl = audioUrl;
            song.UploadedDate = DateTime.Now;

            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs(int? pageNumber, int? pageSize)
        {

            int currentPageNumber = pageNumber ?? 1;
           int  currentPageSize = pageSize ?? 5;


            var songs = await (from song in _dbContext.Songs
                        select new
                        {
                            Id = song.Id,
                            Titel = song.Titel,
                            Duration = song.Duration,
                            ImageUrl = song.ImageUrl,
                            AudioUrl = song.AudioUrl
                        }).ToListAsync();

            return Ok(songs.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FeaturedSongs()
        {
            var songs = await (from song in _dbContext.Songs
                               where song.IsFeatured == true
                               select new
                               {
                                   Id = song.Id,
                                   Titel = song.Titel,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl
                               }).ToListAsync();

            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songs = await (from song in _dbContext.Songs
                               //Descending takes the latest songs
                               orderby song.UploadedDate descending
                               select new
                               {
                                   Id = song.Id,
                                   Titel = song.Titel,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl
                                   //.Take limits the amount of songs that it fetches from the database
                               }).Take(10).ToListAsync();

            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(string query)
        {
            var songs = await (from song in _dbContext.Songs

                               where song.Titel.StartsWith(query)
                               select new
                               {
                                   Id = song.Id,
                                   Titel = song.Titel,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl
                                   //.Take limits the amount of songs that it fetches from the database
                               }).Take(10).ToListAsync();

            return Ok(songs);
        }
    }
}
