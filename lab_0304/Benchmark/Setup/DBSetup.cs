using Npgsql;

using Testcontainers.PostgreSql;

namespace Benchmark.Setup
{
    public class DBSetup
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("BENCHMARK_testdb")
        .WithCleanUp(true)
        .Build();

        public string ConnectionString => _dbContainer.GetConnectionString();

        // Инициализация и поднять контейнера
        public async Task Init()
        {
            await _dbContainer.StartAsync();
            var dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

            //var sqlScript = await File.ReadAllTextAsync("./setup.sql");
            string sqlScript = "CREATE TABLE public.user (\r\n    id SERIAL PRIMARY KEY,\r\n    login VARCHAR(50) NOT NULL,\r\n    password VARCHAR(50) NOT NULL,\r\n    role VARCHAR(50) NOT NULL,\r\n    name TEXT\r\n);";
            var cmd = new NpgsqlCommand(sqlScript, dbConnection);

            await dbConnection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await dbConnection.CloseAsync();
        }

        // Удалить контейнер - docker-composer ... down
        public async Task Remove()
        {
            await _dbContainer.DisposeAsync();
        }

    }
}
