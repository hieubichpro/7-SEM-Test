using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_9.BL.IRepositories;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;

namespace lab_03.BL.Services

{
    public class LeagueService
    {
        private ILeagueRepository leagueRepo;
        private IMatchRepository matchRepo;
        private IClubRepository clubRepo;
        private IClubLeagueRepository clubleagueRepo;
        private ILogger<LeagueService> logger;
        public LeagueService(ILeagueRepository leagueRepo, IMatchRepository matchRepo, IClubRepository clubRepo, IClubLeagueRepository clubleagueRepo, ILogger<LeagueService> logger)
        {
            this.leagueRepo = leagueRepo;
            this.matchRepo = matchRepo;
            this.clubRepo = clubRepo;
            this.clubleagueRepo = clubleagueRepo;
            this.logger = logger;
        }

        public void insertLeague(string name, int idUser)
        {
            logger.LogInformation("read league started");
            League? league = leagueRepo.readbyName(name);
            logger.LogInformation("read league ended");

            if (league == null)
            {
                logger.LogInformation("created league started");
                league = new League(name, idUser);
                leagueRepo.create(league);
                logger.LogInformation("created league ended");
            }
            else
            {
                logger.LogError("insert failed");
                throw new LeagueExistException();
            }
        }

        //internal List<League> getByIdUser(int id)
        //{
        //    logger.LogInformation("started get league by id user");
        //    return leagueRepo.readByIdUser(id);
        //}

        public void deleteLeague(League league)
        {
            logger.LogInformation("started delete league");
            if (leagueRepo.readById(league.Id) == null)
            {
                logger.LogError("delete failed");
                throw new LeagueNotFoundException();
            }
            leagueRepo.delete(league);
        }

        //public League getByName(string name)
        //{
        //    logger.LogInformation("read league started");
        //    League l = leagueRepo.readbyName(name);
        //    if (l == null)
        //    {
        //        logger.LogError("read failed");
        //        throw new LeagueNotFoundException();
        //    }
        //    else
        //    {
        //        logger.LogInformation("read league ended");
        //        return l;
        //    }
        //}

        public List<League> getAll()
        {
            logger.LogInformation("started read all leagues");
            return leagueRepo.readAll();
        }

        public void modifyLeague(int id, string name, int idUser)
        {
            logger.LogInformation("modify league started");
            var l = leagueRepo.readById(id);
            if (l == null)
            {
                logger.LogError("read failed");
                throw new LeagueNotFoundException();
            }
            else
            {
                l.Name = name;
                l.IdUser = idUser;
                leagueRepo.update(l);
                logger.LogInformation("modify league ended");
            }
        }

        public League getById(int id)
        {
            logger.LogInformation("started read league by id");
            var l = leagueRepo.readById(id);
            if ( l == null )
            {
                logger.LogError("read failed");
                throw new LeagueNotFoundException();
            }
            else
            {
                logger.LogInformation("read ended");
                return l;
            }
        }

        //public void Schedule(int id_league)
        //{
        //    List<int> idclubs = new List<int>();
        //    //var allClub = clubleagueRepo.getClubInLeague(id_league);
        //    var allClub = clubleagueRepo.getAll();

        //    Console.WriteLine(allClub.Count);
        //    foreach (var club in allClub)
        //    {
        //        idclubs.Add(club.IdClub);
        //    }
        //    for (int i = 0; i < idclubs.Count; i++)
        //    {
        //        for (int j = 0; j < idclubs.Count; j++)
        //        {
        //            if (j == i)
        //                continue;
        //            matchRepo.create(new Match(id_league, idclubs[i], idclubs[j]));
        //        }
        //    }
        //}

        //public List<ClubStat> Summary(int idLeague)
        //{
        //    var matches = matchRepo.readByIdLeague(idLeague);
        //    List<int> idclubs = new List<int>();
        //    foreach (var match in matches)
        //    {
        //        if (!idclubs.Contains(match.IdHomeTeam))
        //        {
        //            idclubs.Add(match.IdHomeTeam);
        //        }
        //    }
        //    Dictionary<int, ClubStat> stats = new Dictionary<int, ClubStat>();
        //    int games, wins, draws, loses, goal1, goal2;
        //    foreach (var id in idclubs)
        //    {
        //        games = 0;
        //        wins = 0;
        //        draws = 0;
        //        loses = 0;
        //        goal1 = 0;
        //        goal2 = 0;
        //        foreach (var match in matches)
        //        {
        //            if (match.GoalHomeTeam != -1 && match.GoalGuestTeam != -1)
        //            {
        //                if (id == match.IdHomeTeam)
        //                {
        //                    if (match.GoalHomeTeam > match.GoalGuestTeam)
        //                    {
        //                        wins++;
        //                    }
        //                    else if (match.GoalHomeTeam == match.GoalGuestTeam)
        //                    {
        //                        draws++;
        //                    }
        //                    else
        //                    {
        //                        loses++;
        //                    }
        //                    goal1 += match.GoalHomeTeam;
        //                    goal2 += match.GoalGuestTeam;
        //                    games++;
        //                }
        //                else if (id == match.IdGuestTeam)
        //                {
        //                    if (match.GoalHomeTeam > match.GoalGuestTeam)
        //                    {
        //                        loses++;
        //                    }
        //                    else if (match.GoalHomeTeam == match.GoalGuestTeam)
        //                    {
        //                        draws++;
        //                    }
        //                    else
        //                    {
        //                        wins++;
        //                    }
        //                    goal1 += match.GoalGuestTeam;
        //                    goal2 += match.GoalHomeTeam;
        //                    games++;
        //                }
        //            }
        //        }
        //        stats.Add(id, new ClubStat(idLeague, clubRepo.readbyId(id).Name, (idclubs.Count - 1) * 2, games, wins, draws, loses, goal1, goal2, goal1 - goal2, wins * 3 + draws));
        //        //clubStatRepo.create(stats[id]);
        //    }
        //    return stats.Values.ToList();
        //}
    }
}
