﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace University.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly UniversityContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(UniversityContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public TEntity GetByID(TEntity entity)
    {
        return _dbSet.Find(entity);
    }

    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        if (filter == null)
        {
            return _dbSet.ToList();
        }
        return _dbSet.Where(filter).ToList();
    }

    public IQueryable<TEntity> GetAllQuery(Expression<Func<TEntity, bool>> filter = null)
    {
        if (filter == null)
        {
            return _dbSet.AsNoTracking();
        }
        return _dbSet.Where(filter).AsNoTracking();
    }

    public TEntity Insert(TEntity entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}