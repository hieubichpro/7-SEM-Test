using lab_03.BL.Models;

namespace lab_03.BL.IRepositories
{
    public interface IUserRepository
    {
        User readById(int id);
        User readByLogin(string login);
        List<User> readAll();
        void create(User user);
        void update(User user);
        void delete(int id);

    }
}
