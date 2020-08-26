using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreEFDemo.Infrastructure.Repositories
{
    /// <summary>
    /// 泛型Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        private DbSet<T> DbSet
        {
            get
            {
                return _unitOfWork.DbContext.Set<T>();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> AsQueryable() => this.DbSet.AsQueryable();

        /// <summary>
        /// 删除行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task Delete(params T[] entities)
        {
            this.DbSet.RemoveRange(entities);
            if (!_unitOfWork.IsTransaction)
            {
                await _unitOfWork.DbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 添加行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task Insert(params T[] entities)
        {
            await this.DbSet.AddRangeAsync(entities);
            if (!_unitOfWork.IsTransaction)
            {
                await _unitOfWork.DbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 更新行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(params T[] entities)
        {
            this.DbSet.UpdateRange(entities);
            if (!_unitOfWork.IsTransaction)
            {
                await _unitOfWork.DbContext.SaveChangesAsync();
            }
        }
    }
}

