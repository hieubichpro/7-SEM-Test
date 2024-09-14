using lab_03.BL.Models;
using System.Collections.Generic;

namespace lab_03.BL.IRepositories
{
    public interface IUserRepository
    {
        User? readById(int id);
        User? readByLogin(string login);
        List<User> readByRole(string role);
        void create(User user);
        void update(User user);
        void delete(User user);
    }
}
