using EFDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private SongService _songService;

        public SongController(SongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Song> songs = await _songService.GetAsync();
                return Ok(songs);
            }
            catch (NullReferenceException ex)
            { return NotFound(ex.Message); }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                SongIdNameLengthArtistAlbum song = await _songService.GetAsync(id);
                return Ok(song);
            }
            catch (NullReferenceException ex)
            { return NotFound(ex.Message); }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, int seconds, int albumId)
        {
            try
            {
                CreateResponseIdName newSong = await _songService.CreateNewSong(name, seconds, albumId);
                return Ok(newSong);
            }
            catch (NullReferenceException ex)
            { return NotFound(ex.Message); }
            catch (AccessViolationException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return BadRequest("Was unable to create new Song, " + ex.Message + "\n" + ex.StackTrace); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Song song)
        {
            try
            {
                Song updatedSong = await _songService.UpdateAsync(id, song);
                return Ok(updatedSong);
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
                Song deletedSong = await _songService.DeleteAsync(id);
                return Ok("Song has been deleted");
            }
            catch (Exception ex)
            { return BadRequest(ex.Message + ex.InnerException + ex.StackTrace); }
        }
    }
}
