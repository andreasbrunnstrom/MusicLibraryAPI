using EFDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Artist> artist = await _artistService.GetAsync();
                if(artist.Count < 1) { return NotFound("There is no data to retrive"); }
                return Ok(artist);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                ArtistIdNameAlbums artist = await _artistService.GetAsync(id);
                return Ok(artist);
            }
            catch (NullReferenceException ex)
            { return NotFound("The id does not exist"); }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            try
            {
                CreateResponseIdName newArtist = await _artistService.CreateNewArtist(name);
                return Ok(newArtist);
            }
            catch (NullReferenceException ex)
            { return NotFound(ex.Message); }
            catch (Exception ex)
            { return BadRequest("Was unable to create new Artist, " + ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Artist artist)
        {
            try
            {
                Artist updatedArtist = await _artistService.UpdateAsync(id, artist);
                return Ok(updatedArtist);
            }
            catch (NullReferenceException ex)
            { return NotFound(ex.Message); }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Artist deletedArtist = await _artistService.DeleteAsync(id);
                return Ok("Artist has been deleted");
            }
            catch (Exception ex)
            { return BadRequest(ex.Message + ex.InnerException + ex.StackTrace); }
        }
    }
}
