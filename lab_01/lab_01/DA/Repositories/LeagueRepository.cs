using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using lab_01.DA.dbContext;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace lab_04.DA
{
    public class LeagueRepository : ILeagueRepository
    {
        private dbContextFactory _dbContextFactory;
        private ILogger<LeagueRepository> logger;
        public LeagueRepository(dbContextFactory dbContextFactory, ILogger<LeagueRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            this.logger = logger;
        }

        public League? readbyName(string name)
        {
            logger.LogInformation("started read league by name");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read league by name");
            return db_context.leagues.FirstOrDefault(u => u.Name == name);
        }
        public void create(League league)
        {
            logger.LogInformation("started create league");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                if (db_context.leagues.Count() > 0)
                {
                    league.Id = db_context.leagues.Select(u => u.Id).Max() + 1;
                }
                else
                {
                    league.Id = 1;
                }
                db_context.leagues.Add(league);
                db_context.SaveChanges();
                logger.LogInformation("ended create league");
            }
            catch (Exception ex)
            {
                logger.LogError("create league failed");
                throw new Exception("failed while create league");
            }
        }
        public void delete(League league)
        {
            logger.LogInformation("started delete league");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                db_context.leagues.Remove(league);
                db_context.SaveChanges();
                logger.LogInformation("ended delete league");
            }
            catch (Exception ex)
            {
                logger.LogError("delete league failed");
                throw new Exception("failed while delete league");
            }
        }

        public League? readById(int id)
        {
            logger.LogInformation("started read league by id");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read league by id");
            return db_context.leagues.FirstOrDefault(u => u.Id == id);
        }
        public List<League> readAll()
        {
            logger.LogInformation("started read all league");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read all league");
            return db_context.leagues.ToList();
        }

        public void update(League l)
        {
            logger.LogInformation("started update league");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                db_context.leagues.Update(l);
                db_context.SaveChanges();
                logger.LogInformation("ended update league");
            }
            catch (Exception ex)
            {
                logger.LogError("update failed");
                throw new Exception("failed while update league");
            }
        }

        public List<League> readByIdUser(int id)
        {
            logger.LogInformation("started read league by id_user");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read league by id_user");
            return db_context.leagues.Where(l => l.IdUser == id).ToList();
        }

    }
}
