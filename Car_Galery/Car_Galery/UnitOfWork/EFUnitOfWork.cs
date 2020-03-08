using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Car_Galery.Managers.Abstract;
using Car_Galery.Models;
using Car_Galery.Repositories;
using Car_Galery.Repositories.Abstract;

namespace Car_Galery.Managers
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        public EFUnitOfWork(DbContext dbContext)
        {
            Database.SetInitializer<ApplicationDbContext>(null);

            if(dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
        }




        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EFRepository<T>(_dbContext);
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}