using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.Services
{
    public class AlbumService
    {
        private readonly IDataStore _dataStore;
        private CreateResponseIdName _response;

        public AlbumService(IDataStore datastore, CreateResponseIdName respone)
        {
            _dataStore = datastore;
            _response = respone;
        }

        public async Task<CreateResponseIdName> CreateNewAlbum(string name,int id)
        {
            try
            {
                Artist artist = await _dataStore.GetAsync<Artist>(id);
                if (artist is null) throw new NullReferenceException("Could not find artist id");
                Album album = new Album();
                album.Name = name;
                album.Artist = artist;
                album.Artist.Albums.Add(album);
                await _dataStore.Add(album);
                var albumFromDb = await _dataStore.GetAsync<Album>(album.Id);
                if (albumFromDb is null) throw new NullReferenceException("Album is empty after attempt to create it in database");
                _response.Id = albumFromDb.Id;
                _response.Name = albumFromDb.Name;

                return _response;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<List<Album>> GetAsync()
        {
            try
            {
                List<Album> getFromDb = await _dataStore.GetAsync<Album>();
                return getFromDb;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<AlbumIdNameArtistSongs> GetAsync(int id)
        {
            try
            {
                Album getFromDb = await _dataStore.GetAsync<Album>(id);
                AlbumIdNameArtistSongs newAlbum = new AlbumIdNameArtistSongs();
                CreateResponseIdName newArtist = new CreateResponseIdName();
                List<CreateResponseIdName> newListSongs = new List<CreateResponseIdName>();
                CreateResponseIdName newSong = new CreateResponseIdName();
                foreach (var song in getFromDb.songs)
                {
                    newSong.Id = song.Id;
                    newSong.Name = song.Name;
                    newListSongs.Add(newSong);
                }
                newAlbum.songs = newListSongs;
                newArtist.Id = getFromDb.Artist.Id;
                newArtist.Name = getFromDb.Artist.Name;
                newAlbum.Id = getFromDb.Id;
                newAlbum.Name = getFromDb.Name;
                newAlbum.Artist = newArtist;

                return newAlbum;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<Album> UpdateAsync(int id, Album album)
        {
            try
            {
                if (id == 0) new Exception();
                Album albumToUpdate = await _dataStore.GetAsync<Album>(id);
                await _dataStore.Update(albumToUpdate, album);
                return await _dataStore.GetAsync<Album>(albumToUpdate.Id);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<Album> DeleteAsync(int id)
        {
            try
            {
                if (id == 0) throw new Exception("Id cannot be 0, try again.");

                Album albumToDelete = await _dataStore.GetAsync<Album>(id);
                await _dataStore.Delete(albumToDelete);
                Album shouldBeEmpty = await _dataStore.GetAsync<Album>(id);
                if (shouldBeEmpty != null) throw new Exception("Was not able to remove album for unknown reason, try again.");
                return albumToDelete;
            }
            catch (Exception)
            { throw; }
        }
    }
}
