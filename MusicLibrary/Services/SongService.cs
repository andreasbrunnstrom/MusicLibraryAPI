using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicLibrary.Services
{
    public class SongService
    {
        private readonly IDataStore _dataStore;
        private CreateResponseIdName _response;

        public SongService(IDataStore datastore, CreateResponseIdName response)
        {
            _dataStore = datastore;
            _response = response;
        }

        public async Task<CreateResponseIdName> CreateNewSong(string name, int seconds, int albumId)
        {
            try
            {
                List<Album> albums = await _dataStore.GetAsync<Album>();

                List<Song> songs = await _dataStore.GetAsync<Song>();
                foreach (var song in songs) if (song.Name == name) throw new AccessViolationException("Song already exists");

                Album albumObjectToAdd = await _dataStore.GetAsync<Album>(albumId);
                if (albumObjectToAdd is null) throw new NullReferenceException("Could not find album id ");

                Song mySong = new Song();
                mySong.Name = name;
                mySong.Length = seconds;
                mySong.Album = albumObjectToAdd;
                mySong.Artist = albumObjectToAdd.Artist;
                albumObjectToAdd.songs.Add(mySong);
                albumObjectToAdd.NumberOfSongs = albumObjectToAdd.songs.Count;
                mySong.Album.Artist.NumberOfSongs += 1;
               
                foreach (var album in albums)
                {
                    if (album.Artist == albumObjectToAdd.Artist)
                    {
                        if (!mySong.Artist.Albums.Contains(album)) mySong.Artist.Albums.Add(album);
                    }
                }
                await _dataStore.Add(mySong);
                Song songFromDb = await _dataStore.GetAsync<Song>(mySong.Id);
                if (songFromDb is null) throw new NullReferenceException("Song was not found after attempt to create it in database");
                _response.Id = songFromDb.Id;
                _response.Name = songFromDb.Name;

                return _response;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<List<Song>> GetAsync()
        {
            try
            {
                List<Song> getFromDb = await _dataStore.GetAsync<Song>();           
                return getFromDb;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<SongIdNameLengthArtistAlbum> GetAsync(int id)
        {
            try
            {
                if (id == 0) new Exception();
                var getFromDb = await _dataStore.GetAsync<Song>(id);
                SongIdNameLengthArtistAlbum song = new SongIdNameLengthArtistAlbum();
                song.Id = getFromDb.Id;
                song.Name = getFromDb.Name;
                song.Length = getFromDb.Length;

                CreateResponseIdName newArtist = new CreateResponseIdName();
                newArtist.Id = getFromDb.Artist.Id;
                newArtist.Name = getFromDb.Artist.Name;
                song.ArtistIdName = newArtist;

                CreateResponseIdName newAlbum = new CreateResponseIdName();
                newAlbum.Id = getFromDb.Album.Id;
                newAlbum.Name = getFromDb.Album.Name;
                song.AlbumIdName = newAlbum;

                return song;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<Song> UpdateAsync(int id, Song song)
        {
            try
            {
                if (id == 0) new Exception();

                Song songToUpdate = await _dataStore.GetAsync<Song>(id);
                await _dataStore.Update(songToUpdate, song);
                return await _dataStore.GetAsync<Song>(songToUpdate.Id);
            }
            catch (Exception)
            { throw; }
        }

        public async Task<Song> DeleteAsync(int id)
        {
            try
            {
                if (id == 0) throw new Exception("Id cannot be 0, try again.");

                Song songToDelete = await _dataStore.GetAsync<Song>(id);
                await _dataStore.Delete(songToDelete);
                Song shouldBeEmpty = await _dataStore.GetAsync<Song>(id);
                if (shouldBeEmpty != null) throw new Exception("Was not able to remove song for unknown reason, try again.");
                return songToDelete;
            }
            catch (Exception)
            { throw; }
        }
    }
}
