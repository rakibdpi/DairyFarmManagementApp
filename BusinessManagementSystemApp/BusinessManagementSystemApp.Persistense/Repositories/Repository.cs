using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BusinessManagementSystemApp.Core.Repositories;

namespace BusinessManagementSystemApp.Persistense.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }
        public TEntity Get(int id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception e)
            {
                throw new Exception("Data Get by id Fail " + e.Message);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return Context.Set<TEntity>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Get all Data Fail " + e.Message);
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return Context.Set<TEntity>().Where(predicate);
            }
            catch (Exception e)
            {
                throw new Exception("Find Data Fail " + e.Message);
            }
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return Context.Set<TEntity>().SingleOrDefault(predicate);
            }
            catch (Exception e)
            {
                throw new Exception("Get single or Default Data Fail " + e.Message);
            }
        }

        public TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return Context.Set<TEntity>().LastOrDefault(predicate);
            }
            catch (Exception e)
            {
                throw new Exception("Get single or Default Data Fail " + e.Message);
            }
        }

        public void Add(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
            }
            catch (Exception e)
            {
                throw new Exception("Info Save Fail " + e.Message);
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().AddRange(entities);
            }
            catch (Exception e)
            {
                throw new Exception("Info List Save Fail " + e.Message);
            }
        }

        public void Remove(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
            }
            catch (Exception e)
            {
                throw new Exception("Info Delete Fail " + e.Message);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().RemoveRange(entities);
            }
            catch (Exception e)
            {
                throw new Exception("Info List Delete Fail " + e.Message);
            }
        }
    }
}