using lab_01.DA.dbContext;
using lab_03.BL.Models;
using lab_04.DA;
using lab_9.BL.IRepositories;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9.DA
{
    public class ClubLeagueRepository : IClubLeagueRepository
    {
        private dbContextFactory _dbContextFactory;
        private ILogger<ClubLeagueRepository> logger;
        public ClubLeagueRepository(dbContextFactory dbContextFactory, ILogger<ClubLeagueRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            this.logger = logger;
        }

        public List<ClubLeague> getAll()
        {
            logger.LogInformation("started read all clubleagues");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read all clubleagues");
            return db_context.clubleagues.ToList();
        }
        public void create(ClubLeague cl)
        {
            logger.LogInformation("started create clubleague");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                if (db_context.clubleagues.Count() > 0)
                {
                    cl.Id = db_context.clubleagues.Select(u => u.Id).Max() + 1;
                }
                else
                {
                    cl.Id = 1;
                }
                db_context.clubleagues.Add(cl);
                db_context.SaveChanges();
                logger.LogInformation("ended create clubleague");
            }
            catch (Exception ex)
            {
                logger.LogError("create clubleague failed");
                throw new Exception("failed while create clubleague");
            }
        }
        public List<ClubLeague> getClubInLeague(int idleague)
        {
            logger.LogInformation("started read clubs by id_league");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read clubs by id_league");
            return db_context.clubleagues.Where(u => u.IdLeague == idleague).ToList();
        }
    }
}
