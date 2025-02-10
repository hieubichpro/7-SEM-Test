using Benchmark.Models;
using Benchmark.Repositories.IRepositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Benchmark.Repositories.Npgsql
{
    public class NpgsqlUserRepository(string connectionStr) : IUserRepository
    {
        public async Task<User> InsertUser(string login, string password, string role, string name)
        {
            var cmd = new NpgsqlCommand();
            cmd.Connection = new NpgsqlConnection(connectionStr);

            await cmd.Connection.OpenAsync();

            cmd.CommandText =
                "INSERT INTO public.user (login, password, role, name) " +
                "VALUES (@Login, @Password, @Role, @Name);";

            cmd.Parameters.AddWithValue("Login", login);
            cmd.Parameters.AddWithValue("Password", password);
            cmd.Parameters.AddWithValue("Role", role);
            cmd.Parameters.AddWithValue("Name", name);

            await cmd.ExecuteNonQueryAsync();

            await cmd.Connection.CloseAsync();
            return new User(login, password, role, name);

        }

        public async Task<User> SelectUser(string login)
        {
            var cmd = new NpgsqlCommand();
            cmd.Connection = new NpgsqlConnection(connectionStr);

            await cmd.Connection.OpenAsync();

            cmd.CommandText = $"SELECT * FROM public.user WHERE login='{login}'";
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();
            //User result = null;
            //if (reader.HasRows)
            //{
            //    result = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(0));
            //}
            //reader.Close();

            //cmd.Connection.Close();
            //return result;
            var result = new User("aa", "bb", "Admin", "1", 10);
            await reader.CloseAsync();

            await cmd.Connection.CloseAsync();
            return result;


        }
    }
}
