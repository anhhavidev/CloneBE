using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloneBE.Infraction.Repo
{
    public class GennerticRepo<T> : IGennericRepo<T> where T : class
    {
        protected readonly Databasese _database;
        protected readonly  DbSet<T> _dbset;
        public GennerticRepo(Databasese database) {
           _database = database;
            _dbset = database.Set<T>();
        }
        public void Add(T enity)
        {
           _dbset.Add(enity);

        }

        public  void  Delete(T enity)
        {
              _dbset.Remove(enity);
            
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbset.AsNoTracking().ToListAsync();
            
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public void  Update(T enity)
        {
             _dbset.Update(enity);
        }
    }
}
