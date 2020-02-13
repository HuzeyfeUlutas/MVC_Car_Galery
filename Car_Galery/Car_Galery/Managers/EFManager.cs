using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Car_Galery.Context;
using Car_Galery.Managers.Abstract;
using Car_Galery.Repositories;
using Car_Galery.Repositories.Abstract;

namespace Car_Galery.Managers
{
    public class EFManager : IManager
    {
        private VehiclesContext _dbContext;
        private bool disposed = false;

        public EFManager(VehiclesContext dbContext)
        {
            Database.SetInitializer<VehiclesContext>(null);

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