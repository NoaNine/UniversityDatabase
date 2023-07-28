﻿using Microsoft.EntityFrameworkCore;
using University.Dal.UnitOfWork;
using University.DAL.Repository;

namespace University.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly UniversityContext _context;
        private string _errorMessage = string.Empty;
        private bool _disposed = false;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(UniversityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }
            return (IRepository<TEntity>)_repositories[type];
        }

        public void Save()
        {
            _context.SaveChanges();
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}