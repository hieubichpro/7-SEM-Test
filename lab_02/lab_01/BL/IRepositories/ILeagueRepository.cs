
using lab_03.BL.Models;
using System.Collections.Generic;
using System.Data;

namespace lab_03.BL.IRepositories
{
    public interface ILeagueRepository
    {
        League? readbyName(string name);
        void create(League league);
        void delete(League league);
        League? readById(int id);
        List<League> readAll();
        List<League> readByIdUser(int id);
        void update(League l);

    }
}
