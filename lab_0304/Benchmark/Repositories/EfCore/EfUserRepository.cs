using Benchmark.Models;
using Benchmark.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Benchmark.Repositories.EfCore
{
    public class EfUserRepository(DbContextOptions<BenchmarkContext> options) : IUserRepository
    {
        public async Task<User> InsertUser(string login, string password, string role, string name)
        {
            await using var context = new BenchmarkContext(options);
            var user = new User(login, password, role, name);
            if (context.users.Count() > 0)
            {
                user.Id = context.users.Select(u => u.Id).Max() + 1;
            }
            else
            {
                user.Id = 1;
            }
            var added = await context.users.AddAsync(user);
            await context.SaveChangesAsync();

            return added.Entity;

        }

        public async Task<User> SelectUser(string login)
        {
            await using var context = new BenchmarkContext(options);

            return await context.users.FirstOrDefaultAsync(u => u.Login == login);

        }
    }
}
