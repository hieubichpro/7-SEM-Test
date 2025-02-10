using lab_03.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using lab_03.DA.Entities;
using System.Xml.Linq;

namespace lab_03.BL.Services
{
    public class LeagueService
    {
        private ILeagueRepository _leagueRepository;
        private IMatchRepository _matchRepository;
        private IClubRepository _clubRepository;
        private ILogger<LeagueService> logger;
        public LeagueService(ILeagueRepository leagueRepository, IMatchRepository matchRepository, IClubRepository clubRepository, ILogger<LeagueService> logger)
        {
            _leagueRepository = leagueRepository;
            _matchRepository = matchRepository;
            _clubRepository = clubRepository;
            this.logger = logger;
        }
        public void InsertLeague(League league)
        {
            logger.LogInformation("created league started");
            _leagueRepository.create(league);
            logger.LogInformation("created league ended");
        }
        public void DeleteLeague(int id)
        {
            logger.LogInformation("started delete league");
            _leagueRepository.delete(id);
            logger.LogInformation("ended delete league");
        }
        public void ModifyLeague(League league)
        {
            logger.LogInformation("started modify league");
            _leagueRepository.update(league);
            logger.LogInformation("ended modify league");
        }
        public League GetLeague(int id)
        {
            logger.LogInformation("started read league");
            return _leagueRepository.readById(id);
        }
        public List<League> GetByUser(int userId)
        {
            return _leagueRepository.readByIdUser(userId);
        }
        public List<Club> GetClubsByIdLeague(int idleague)
        {
            return _clubRepository.readByIdLeague(idleague);
        }

        public List<Match> GetMatchesByIdLeague(int idLeague)
        {
            return _matchRepository.readByIdLeague(idLeague);
        }
    }
}
