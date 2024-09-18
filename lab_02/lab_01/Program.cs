using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using lab_03.BL.IRepositories;
using lab_04.DA;
using lab_9.BL.IRepositories;
using lab_9.DA;
using lab_03.BL.Services;
using lab_03.BL.Models;
using Microsoft.Extensions.Logging;
using lab_01.DA.dbContext.PostgreSQL;
using lab_01.TechUI;
using Microsoft.EntityFrameworkCore;
using lab_01.DA.dbContext;
using Serilog;
using Serilog.AspNetCore;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                                           .AddJsonFile("dbsettings.json")
                                                           .Build();

            var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<dbContextFactory, pgSqlDbContextFactory>();

                services.AddSingleton(configuration);

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IClubLeagueRepository, ClubLeagueRepository>();
                services.AddScoped<IClubRepository, ClubRepository>();
                services.AddScoped<ILeagueRepository, LeagueRepository>();
                services.AddScoped<IMatchRepository, MatchRepository>();

                services.AddScoped<UserService>();
                services.AddScoped<ClubService>();
                services.AddScoped<LeagueService>();
                services.AddScoped<MatchService>();

                var my_logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
                services.AddLogging(x => x.AddSerilog(logger: my_logger, dispose: true));

                services.AddSingleton<TechUI>();


            });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var techUI = services.GetRequiredService<TechUI>();
                    techUI.openMainView();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}