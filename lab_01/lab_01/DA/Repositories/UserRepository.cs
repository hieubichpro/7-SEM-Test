using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;
using lab_01.DA.dbContext;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace lab_04.DA
{
    public class UserRepository : IUserRepository
    {
        private dbContextFactory _dbContextFactory;
        private ILogger<UserRepository> logger;
        public UserRepository(dbContextFactory dbContextFactory, ILogger<UserRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            this.logger = logger;
        }

        public User? readById(int id)
        {
            logger.LogInformation("started read user by id");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read user by id");
            return db_context.users.FirstOrDefault(u => u.Id == id);
        }
        public User? readByLogin(string login)
        {
            logger.LogInformation("started read user by login");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read user by login");
            return db_context.users.FirstOrDefault(u => u.Login == login);
        }
        public List<User> readByRole(string role)
        {
            logger.LogInformation("started read user by role");
            using var db_context = _dbContextFactory.get_db_context();
            logger.LogInformation("ended read user by role");
            return db_context.users.Where(u => u.Role == role).ToList();
        }
        public void create(User user)
        {
            logger.LogInformation("started create user");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                if (db_context.users.Count() > 0)
                {
                    user.Id = db_context.users.Select(u => u.Id).Max() + 1;
                }
                else
                {
                    user.Id = 1;
                }
                db_context.users.Add(user);
                db_context.SaveChanges();
                logger.LogInformation("ended create user");
            }
            catch (Exception ex)
            {
                logger.LogError("create user failed");
                throw new Exception("failed while create user");
            }
        }
        public void update(User user)
        {
            logger.LogInformation("started update user");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                db_context.users.Update(user);
                db_context.SaveChanges();
                logger.LogInformation("ended update user");
            }
            catch (Exception ex)
            {
                logger.LogError("update user failed");
                throw new Exception("failed while update user");
            }
        }

        public void delete(User user)
        {
            logger.LogInformation("started delete user");
            try
            {
                using var db_context = _dbContextFactory.get_db_context();
                db_context.users.Remove(user);
                db_context.SaveChanges();
                logger.LogInformation("ended delete user");
            }
            catch (Exception ex)
            {
                logger.LogError("failed while delete user");
                throw new Exception("failed while delete user");
            }
        }
    }
}
