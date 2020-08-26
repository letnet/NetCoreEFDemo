using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NetCoreEFDemo.Infrastructure
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 是否开启事务
        /// </summary>
        public bool IsTransaction { get; set; }

        private bool _disposed;
        public DbContextBase DbContext { get; }

        public UnitOfWork(DbContextBase _dbContext)
        {
            this.DbContext = _dbContext;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction() => IsTransaction = true;

        /// <summary>
        /// 完成事务
        /// </summary>
        public async Task<int> Commit() => await DbContext?.SaveChangesAsync();

        /// <summary>
        /// 回滚事务
        /// </summary>
        public async ValueTask Rollback() => await DbContext.DisposeAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                DbContext?.Dispose();

            _disposed = true;
        }


    }
}

