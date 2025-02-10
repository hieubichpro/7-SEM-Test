using lab_01.DA.dbContext;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_04.DA
{
    public class MatchRepository : IMatchRepository
    {
        private dbContextFactory _dbContextFactory;
        ILogger<MatchRepository> logger;
        public MatchRepository(dbContextFactory dbContextFactory, ILogger<MatchRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            this.logger = logger;
        }

        public void create(Match match)
        {
            logger.LogInformation("started create match");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                if (db_context.matches.Count() > 0)
                {
                    match.Id = db_context.matches.Select(u => u.Id).Max() + 1;
                }
                else
                {
                    match.Id = 1;
                }
                db_context.matches.Add(match);
                db_context.SaveChanges();
                logger.LogInformation("ended create match");
            }
            catch (Exception ex)
            {
                logger.LogError("create match failed");
                throw new Exception("failed while create match");
            }
        }
        public void update(Match match)
        {
            logger.LogInformation("started update match");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                db_context.matches.Update(match);
                db_context.SaveChanges();
                logger.LogInformation("ended update match");
            }
            catch (Exception ex)
            {
                logger.LogError("update match failed");
                throw new Exception("failed while update match");
            }
        }

        public List<Match> readByIdLeague(int id_league)
        {
            logger.LogInformation("start read all match");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("end read all match");
            return db_context.matches.Where(u => u.IdLeague ==  id_league).ToList();
        }

        public Match? readByID(int id)
        {
            logger.LogInformation("started read match by id");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read match by id");
            return db_context.matches.FirstOrDefault(u => u.Id == id);
        }

        public List<Match> readAll()
        {
            logger.LogInformation("started read all match");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read all match");
            return db_context.matches.ToList();
        }
    }
}
