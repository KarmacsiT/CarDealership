using Microsoft.EntityFrameworkCore;
using MQ7GIA_HFT_2021221.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected CarDealershipContext cd_ctx;

        public Repository(CarDealershipContext cd_ctx)
        {
            this.cd_ctx = cd_ctx;
        }

        public void Add(T entity)
        {
            cd_ctx.Set<T>().Add(entity);
            cd_ctx.SaveChanges();
        }

        public void Delete(T entity)
        {
            cd_ctx.Set<T>().Remove(entity);
            cd_ctx.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
           return cd_ctx.Set<T>();
        }

        public abstract T GetOne(int id);

        public void Update(T entity)
        {
            cd_ctx.Set<T>().Attach(entity);
            cd_ctx.SaveChanges();
        }
    }
}
