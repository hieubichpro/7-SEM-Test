
using System.Collections.Generic;
using System.Data;
using lab_01.DA.dbContext;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace lab_04.DA
{
    public class ClubRepository : IClubRepository
    {
        private dbContextFactory _dbContextFactory;
        private ILogger<ClubRepository> logger;
        public ClubRepository(dbContextFactory dbContextFactory, ILogger<ClubRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            this.logger = logger;
        }
        public Club? readbyName(string name)
        {
            logger.LogInformation("started read club by name");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read club by name");
            return db_context.clubs.FirstOrDefault(u => u.Name == name);
        }

        public Club? readbyId(int id)
        {
            logger.LogInformation("started read club by id");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read club by id");
            return db_context.clubs.FirstOrDefault(u => u.Id == id);
        }
        public void create(Club club)
        {
            logger.LogInformation("started create club");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                if (db_context.clubs.Count() > 0)
                {
                    club.Id = db_context.clubs.Select(u => u.Id).Max() + 1;
                }
                else
                {
                    club.Id = 1;
                }
                db_context.clubs.Add(club);
                db_context.SaveChanges();
                logger.LogInformation("ended create club");
            }
            catch (Exception ex)
            {
                logger.LogError("create failed club");
                throw new Exception("failed while create club");
            }
        }
        public List<Club> readAll()
        {
            logger.LogInformation("started read club by role");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read club by role");
            return db_context.clubs.ToList();
        }

        public void update(Club club)
        {
            logger.LogInformation("started update club");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                db_context.clubs.Update(club);
                db_context.SaveChanges();
                logger.LogInformation("ended update club");
            }
            catch (Exception ex)
            {
                logger.LogError("update club failed");
                throw new Exception("failed while update club");
            }
        }

        public void delete(Club club)
        {
            logger.LogInformation("started delete club");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                db_context.clubs.Remove(club);
                db_context.SaveChanges();
                logger.LogInformation("ended delete club");
            }
            catch (Exception ex)
            {
                logger.LogError("delete club failed");
                throw new Exception("failed while delete club");
            }
        }
    }
}
