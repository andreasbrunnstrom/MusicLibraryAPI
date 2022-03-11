using EFDataAccess;
using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary
{
    public class DataStore : IDataStore
    {
        private readonly MusicLibraryContext _dbContext;
        public DataStore(MusicLibraryContext context)
        {
            _dbContext = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _dbContext.AddAsync(entity);
            _dbContext.SaveChanges();
        }

        public async Task<List<T>> GetAsync<T>() where T : class
        {
            return await Task.Run(() =>
            {
                if (typeof(T) == typeof(Album))
                {

                    return _dbContext.Set<Album>().Include(x => x.Artist).Include(x => x.songs).ToList() as List<T>;
                }

                if (typeof(T) == typeof(Artist))
                {
                    
                    return _dbContext.Set<Artist>().Include(x => x.Albums).ToList() as List<T>;
                }

                if (typeof(T) == typeof(Song))
                {
                    return _dbContext.Set<Song>().Include(x => x.Album).Include(z => z.Artist).Include(y => y.Artist.Albums).ToList() as List<T>;
                }
                return null;
            });
        }

        public async Task<T> GetAsync<T>(int id) where T : class
        {
            return await Task.Run(() => {
                if (typeof(T) == typeof(Album))
                {
                    return _dbContext.Album.Include(x => x.Artist).Include(z => z.songs).FirstOrDefault(x => x.Id == id) as T;
                }

                if (typeof(T) == typeof(Artist))
                {
                    return _dbContext.Artist.Include(x => x.Albums).FirstOrDefault(x => x.Id == id) as T;
                }

                if (typeof(T) == typeof(Song))
                {
                    return _dbContext.Song.Include(x => x.Album).Include(z => z.Artist).Include(y => y.Artist.Albums).FirstOrDefault(x => x.Id == id) as T;
                }
                return null;
            });
        }

        public async Task Update<T>(T oldEntity, T newEntity) where T : class
        {
            await Task.Run(() =>
            {
                _dbContext.Entry(oldEntity).CurrentValues.SetValues(newEntity);
                _dbContext.SaveChanges();
            });
        }

        public async Task Delete<T>(T entity) where T : class
        {
            await Task.Run(() =>
            {
                try // Om man vill köra stacktrace hela vägen ner hit så kan det vara bra med try catch även här nere och på ovan metoder.
                {
                    if (typeof(T) == typeof(Artist))
                    {
                        var artist = entity as Artist;
                        List<Album> albums = _dbContext.Album.Include(x => x.songs).Where(x => x.Artist == artist).ToList();
                        foreach (var album in albums)
                        {
                            if (artist == album.Artist)
                            {
                                foreach (var song in album.songs)
                                {
                                    _dbContext.Remove(song);
                                }
                                _dbContext.Remove(album);
                            }
                        }
                        _dbContext.Remove(artist);
                        _dbContext.SaveChanges();
                    }

                    if (typeof(T) == typeof(Album))
                    {
                        var album = entity as Album;
                        List<Song> songs = _dbContext.Song.Where(x => x.Artist.Albums.Contains(album)).ToList();

                        foreach (var song in songs)
                        {

                            _dbContext.Remove(song);

                        }
                        _dbContext.Remove(album);
                        _dbContext.SaveChanges();
                    }

                    if (typeof(T) == typeof(Song))
                    {
                        Song song = entity as Song;
                        Song songToDelete = _dbContext.Song.FirstOrDefault(x => x.Id == song.Id) as Song;
                        if (songToDelete is null) { return null; }
                        songToDelete.Artist.NumberOfSongs -= 1;
                        songToDelete.Album.NumberOfSongs -= 1;
                        _dbContext.Remove(songToDelete);
                        _dbContext.SaveChanges();
                    }
                    return Task.CompletedTask;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
