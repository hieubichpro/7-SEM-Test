using lab_03.BL.IRepositories;
using System.Collections.Generic;
using lab_03.BL.Models;
using NLog;
using System;
using Microsoft.Extensions.Logging;
using lab_01.BL.Exceptions;

namespace lab_03.BL.Services

{
    public class MatchService
    {
        private IMatchRepository matchRepo;
        private IClubRepository clubRepo;
        private ILogger<MatchService> logger;
        public MatchService(IMatchRepository matchRepo, IClubRepository clubRepo, ILogger<MatchService> logger)
        {
            this.matchRepo = matchRepo;
            this.clubRepo = clubRepo;
            this.logger = logger;
        }

        public string getNameClubById(int id)
        {
            logger.LogInformation("get club started");
            Club? c = clubRepo.readbyId(id);
            if (c == null)
            {
                logger.LogError("club doesnt exists");
                throw new ClubNotFoundException();
            }
            else
            {
                logger.LogInformation("get club ended");
                return c.Name;
            }
        }
        public List<Match> getMatchByIdLeague(int id_league)
        {
            logger.LogInformation("get matches started");
            var matches = matchRepo.readByIdLeague(id_league);
            logger.LogInformation("get matches ended");
            return matches;
        }

        public void EnterScore(int id, int homeGoal, int guestGoal)
        {
            logger.LogInformation("enter score started");
            Match? match = matchRepo.readByID(id);
            if (match != null)
            {
                match.GoalHomeTeam = homeGoal;
                match.GoalGuestTeam = guestGoal;
                matchRepo.update(match);
                logger.LogInformation("enter score ended");
            }
            else
            {
                logger.LogError("read match failed");
                throw new MatchNotFoundException();
            }
        }
        public Match getById(int id)
        {
            logger.LogInformation("start get match by id");
            var match = matchRepo.readByID(id);
            if (match != null)
            {
                logger.LogInformation("ended get match by id");
                return match;
            }
            else
            {
                logger.LogError("read match by id failed");
                throw new MatchNotFoundException();
            }
        }
    }
}
