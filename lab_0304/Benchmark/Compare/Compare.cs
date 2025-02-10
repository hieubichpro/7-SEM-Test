using Benchmark.Models;
using Benchmark.Repositories.Dapper;
using Benchmark.Repositories.EfCore;
using Benchmark.Repositories.Npgsql;
using Benchmark.Setup;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Compare
{
    [MemoryDiagnoser]
    [MarkdownExporter]
    [RPlotExporter]
    [SimpleJob(launchCount: 1, iterationCount: 5)]

    public class Compare
    {
        private DBSetup databaseSetup = new DBSetup();

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            await databaseSetup.Init();
            //Console.WriteLine(databaseSetup.ConnectionString);
        }

        [Benchmark]
        public async Task NpgslInsert()
        {
            var userRepository = new NpgsqlUserRepository(databaseSetup.ConnectionString);
            await userRepository.InsertUser("test", "test", "Admin", "test");
        }

        [Benchmark]
        public async Task<User> NpgslSelect()
        {
            var userRepository = new NpgsqlUserRepository(databaseSetup.ConnectionString);
            var user = await userRepository.SelectUser("test");

            return user; // to prevent JIT compiler from eliminating call
        }

        [Benchmark]
        public async Task DapperInsert()
        {
            var userRepository = new DapperUserRepository(databaseSetup.ConnectionString);
            await userRepository.InsertUser("test", "test", "Admin", "test");
        }

        [Benchmark]
        public async Task<User> DapperSelect()
        {
            var userRepository = new DapperUserRepository(databaseSetup.ConnectionString);
            var user = await userRepository.SelectUser("test");

            return user; // to prevent JIT compiler from eliminating call
        }

        [Benchmark]
        public async Task<User> EfCoreSelect()
        {
            var builder = new DbContextOptionsBuilder<BenchmarkContext>();
            builder.UseNpgsql(databaseSetup.ConnectionString);
            var userRepository = new EfUserRepository(builder.Options);

            var user = await userRepository.SelectUser("test");

            return user; // to prevent JIT compiler from eliminating call
        }

        [Benchmark]
        public async Task EfCoreInsert()
        {
            var builder = new DbContextOptionsBuilder<BenchmarkContext>();
            builder.UseNpgsql(databaseSetup.ConnectionString);
            var userRepository = new EfUserRepository(builder.Options);

            await userRepository.InsertUser("test", "test", "Admin", "test");
        }

        [GlobalCleanup]
        public async Task GlobalCleanup()
        {
            await databaseSetup.Remove();
        }

    }
}
