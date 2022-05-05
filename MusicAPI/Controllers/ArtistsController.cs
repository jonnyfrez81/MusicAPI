using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Data;
using MusicAPI.Helpers;
using MusicAPI.Models;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public ArtistsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetArtist(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;

            var artists = await (from artist in _dbContext.Artists
                          select new
            {
                //You can change the name of id, Name or ImageUrl and this change will be shown in 
                //Postman in the JsonKeys.
               
                id = artist.Id,
                Name = artist.Name,
                ImageUrl = artist.ImageUrl

            }).ToListAsync();

            return Ok(artists.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ArtistDetails(int artistId)
        {
            var artistDetails = await _dbContext.Artists.Where(a => a.Id == artistId).Include(x => x.Songs).ToListAsync();
            return Ok(artistDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artist artist)
        {

            var imageUrl = await FileHelper.UploadImage(artist.Image);
            artist.ImageUrl = imageUrl;

            await _dbContext.Artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
