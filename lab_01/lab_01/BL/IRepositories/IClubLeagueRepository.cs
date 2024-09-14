using lab_03.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9.BL.IRepositories
{
    public interface IClubLeagueRepository
    {
        List<ClubLeague> getAll();
        void create(ClubLeague cl);
        List<ClubLeague> getClubInLeague(int idleague);
    }
}
