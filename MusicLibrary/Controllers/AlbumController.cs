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
    public class AlbumController : ControllerBase
    {
        private AlbumService _albumService;

        public AlbumController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Album> album = await _albumService.GetAsync();
                if (album.Count < 1) { return NotFound("There is no data to retrive"); }
                return Ok(album);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                AlbumIdNameArtistSongs album = await _albumService.GetAsync(id);                
                return Ok(album);
            }
            catch (NullReferenceException ex)
            { return NotFound("The id does not exist"); }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name,int id)
        {
            try
            {
                CreateResponseIdName newAlbum = await _albumService.CreateNewAlbum(name, id);
                return Ok(newAlbum);
            }
            catch (NullReferenceException ex)
            { return NotFound(ex.Message); }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Album album)
        {
            try
            {
                Album updatedAlbum = await _albumService.UpdateAsync(id, album);
                return Ok(updatedAlbum);
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
                Album deletedAlbum = await _albumService.DeleteAsync(id);
                return Ok("Album has been deleted");
            }
            catch (Exception ex)
            { return BadRequest(ex.Message + ex.InnerException + ex.StackTrace); }
        }
    }
}
