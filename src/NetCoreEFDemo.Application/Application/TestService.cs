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
        public TestService(IRepository<Test> testRepository)
        {
            this._testRepository = testRepository;
        }

        //[Transactional]
        public async Task<TestDto> Get(string id)
        {
            var test = await _testRepository.AsQueryable().Where(p => p.ID == id).SingleAsync();
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

