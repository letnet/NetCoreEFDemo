using NetCoreEFDemo.Application.Models;
using NetCoreEFDemo.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreEFDemo.Application
{
    public interface ITestService : IBaseApplication
    {
        Task<TestDto> Get(string id);

        Task Insert(List<TestDto> list);
    }
}

