using lab_03.BL.Models;
using lab_03.DA.Entities;

namespace lab_03.BL.IRepositories
{
    public interface ILeagueRepository
    {
        League readbyName(string name);
        void create(League league);
        void delete(int id);
        League readById(int id);
        List<League> readAll();
        List<League> readByIdUser(int id);
        void update(League league);

    }
}
