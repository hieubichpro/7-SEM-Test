using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;

namespace lab_03.BL.Services
{
    public class ClubService
    {
        private IClubRepository clubRepo;
        private ILogger<ClubService> logger;
        public ClubService(IClubRepository clubRepo, ILogger<ClubService> logger)
        {
            this.clubRepo = clubRepo;
            this.logger = logger;
        }

        public void insertClub(string name)
        {
            logger.LogInformation("insert club started");
            Club? club = clubRepo.readbyName(name);
            if (club == null)
            {
                club = new Club(name);
                clubRepo.create(club);
                logger.LogInformation("insert club ended");
            }
            else
            {
                logger.LogError("insert club failed");
                throw new ClubExistException();
            }
        }
        public int getIdClubByName(string name)
        {
            logger.LogInformation("getIdClubByName started");
            Club club = clubRepo.readbyName(name);
            if (club != null)
            {
                logger.LogInformation("getidclubbyname ended");
                return club.Id;
            }
            else
            {
                logger.LogError("getIdClubByName failed");
                throw new ClubNotFoundException();
            }
        }

        //public string getNameClubById(int id)
        //{
        //    logger.LogInformation("getNameClubById started");
        //    Club club = clubRepo.readbyId(id);
        //    if (club != null)
        //    {
        //        logger.LogInformation("getNameClubById ended");
        //        return club.Name;
        //    }
        //    else
        //    {
        //        logger.LogError("getNameClubById failed");
        //        throw new ClubNotFoundException();
        //    }
        //}
        public List<Club> getAll()
        {
            logger.LogInformation("started read all clubs");
            return clubRepo.readAll();
        }
        public void modifyClub(int id, string name)
        {
            Club? club = clubRepo.readbyId(id);
            if (club != null)
            {
                club.Name = name;
                clubRepo.update(club);
            }
            else
            {
                logger.LogError("read club failed");
                throw new ClubNotFoundException();
            }
        }
        public void deleteClub(Club club)
        {
            logger.LogInformation("started delete club");
            if (clubRepo.readbyId(club.Id) == null)
            {
                logger.LogError("delete failed");
                throw new ClubNotFoundException();
            }
            else
            {
                logger.LogInformation("ended delete club");
                clubRepo.delete(club);
            }
        }
    }
}
