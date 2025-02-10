using Benchmark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> InsertUser(string login, string password, string role, string name);

        Task<User> SelectUser(string login);

    }
}
