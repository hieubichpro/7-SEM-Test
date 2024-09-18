using lab_03.BL.Models;
using System.Collections.Generic;

namespace lab_03.BL.IRepositories
{
    public interface IMatchRepository
    {
        void create(Match match);
        void update(Match match);
        List<Match> readByIdLeague(int id_league);
        Match? readByID(int id);
        List<Match> readAll();
    }
}
