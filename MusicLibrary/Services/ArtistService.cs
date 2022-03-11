using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicLibrary.Services
{
    public class ArtistService
    {
        private readonly IDataStore _dataStore;
        private CreateResponseIdName _response;

        public ArtistService(IDataStore datastore, CreateResponseIdName response)
        {
            _dataStore = datastore;
            _response = response;
        }

        public async Task<CreateResponseIdName> CreateNewArtist(string name)
        {
            try
            {
                List<Artist> artists = await _dataStore.GetAsync<Artist>();
                foreach (var artist in artists) if (artist.Name == name) throw new Exception("Artist name already exists");

                Artist newArtist = new Artist();
                newArtist.Name = name;
                await _dataStore.Add(newArtist);
                var newArtistFromDb = await _dataStore.GetAsync<Artist>(newArtist.Id);
                if (newArtistFromDb is null) throw new NullReferenceException("Artist was not found after attempt to create in database");
                _response.Id = newArtistFromDb.Id;
                _response.Name = newArtistFromDb.Name;
                return _response;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<List<Artist>> GetAsync()
        {
            try
            {
                List<Artist> getFromDb = await _dataStore.GetAsync<Artist>();
                return getFromDb;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<ArtistIdNameAlbums> GetAsync(int id)
        {
            try
            {
                var getFromDb = await _dataStore.GetAsync<Artist>(id);
                ArtistIdNameAlbums newArtist = new ArtistIdNameAlbums();
                List<CreateResponseIdName> albums = new List<CreateResponseIdName>();
                CreateResponseIdName newAlbum = new CreateResponseIdName();
                foreach (var album in getFromDb.Albums)
                {
                    newAlbum.Id = album.Id;
                    newAlbum.Name = album.Name;
                    albums.Add(newAlbum);
                }

                newArtist.Id = getFromDb.Id;
                newArtist.Name = getFromDb.Name;
                newArtist.Albums = new List<CreateResponseIdName>();
                newArtist.Albums = albums;

                return newArtist;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<Artist> UpdateAsync(int id, Artist artist)
        {
            try
            {
                if (id == 0) new Exception();
                Artist artistToUpdate = await _dataStore.GetAsync<Artist>(id);
                await _dataStore.Update(artistToUpdate, artist);
                return await _dataStore.GetAsync<Artist>(artistToUpdate.Id);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<Artist> DeleteAsync(int id)
        {
            try
            {
                if (id == 0) throw new Exception("Id cannot be 0, try again.");

                Artist artistToDelete = await _dataStore.GetAsync<Artist>(id);
                await _dataStore.Delete(artistToDelete);
                Artist shouldBeEmpty = await _dataStore.GetAsync<Artist>(id);
                if (shouldBeEmpty != null) throw new Exception("Was not able to remove album for unknown reason, try again.");
                return artistToDelete;
            }
            catch (Exception)
            { throw; }
        }
    }
}
