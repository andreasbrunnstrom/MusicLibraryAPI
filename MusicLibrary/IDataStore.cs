using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public interface IDataStore
    {
        public Task<List<T>> GetAsync<T>() where T : class;
        public Task<T> GetAsync<T>(int id) where T : class;
        public Task Add<T>(T entity) where T : class;
        public Task Update<T>(T oldEntity, T newEntity) where T : class;
        public Task Delete<T>(T entity) where T : class;
    }
}
