using Microsoft.EntityFrameworkCore;
using NetCoreEFDemo.Application.Entitys;
using NetCoreEFDemo.Application.Models;
using NetCoreEFDemo.Infrastructure;
using NetCoreEFDemo.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreEFDemo.Application.Application
{
    public class TestService : ITestService
    {
        IRepository<Test> _testRepository { get; set; }
        IUnitOfWork _unitOfWork { get; set; }
        public TestService(IRepository<Test> testRepository, IUnitOfWork _unitOfWork)
        {
            this._testRepository = testRepository;
            this._unitOfWork = _unitOfWork;
        }

        //[Transactional]
        public async Task<TestDto> Get(string id)
        {
            var test = await _testRepository.AsQueryable().Where(p => p.ID == id).SingleAsync();
            var mttest = _unitOfWork.DbContext.Database.SqlQuery<Test>("select * from Test");
            return test.MapTo<TestDto>();
        }

        public async Task Insert(List<TestDto> list)
        {
            if (list == null)
                return;
            var listTest = list.MapToList<Test>();
            await _testRepository.Insert(listTest.ToArray());
        }
    }
}

