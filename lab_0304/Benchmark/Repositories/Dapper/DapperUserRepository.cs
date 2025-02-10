using Benchmark.Models;
using Benchmark.Repositories.IRepositories;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Repositories.Dapper
{
    public class DapperUserRepository(string connectionString) : IUserRepository
    {
        public async Task<User> InsertUser(string login, string password, string role, string name)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.OpenAsync();

            const string sql = "INSERT INTO public.user (login, password, role, name) " +
                               "VALUES (@Login, @Password, @Role, @Name);";

            var id = await connection.ExecuteAsync(sql, new
            {
                Login = login,
                Password = password,
                Role = role,
                Name = name
            });

            return new User(login, password, role, name);

        }

        public async Task<User> SelectUser(string login)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var sql = "SELECT * FROM public.user WHERE login = @Login;";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new
                {
                    Login = login
                }
            );

        }
    }
}
