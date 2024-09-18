using lab_01.BL.Exceptions;
using lab_03.BL.IRepositories;
using lab_03.BL.Models;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace lab_03.BL.Services

{
    public class UserService
    {
        private IUserRepository userRepo;
        private ILogger<UserService> logger;
        public UserService(IUserRepository userRepo, ILogger<UserService> logger)
        {
            this.userRepo = userRepo;
            this.logger = logger;
        }

        public User Login(string login, string password)
        {
            logger.LogInformation("login started");
            User? user = userRepo.readByLogin(login);
            if (user == null)
            {
                logger.LogError("user doesn't exists");
                throw new UserNotFoundException();
            }
            else
            {
                if (!user.checkPassword(password))
                {
                    logger.LogError("password not correct");
                    throw new UserNotMatchPasswordException();
                }
            }
            logger.LogInformation("login sucess");
            return user;
        }

        public void Register(string login, string password, string role, string name = "")
        {
            logger.LogInformation("register started");
            if (userRepo.readByLogin(login) == null)
            {
                userRepo.create(new User(login, password, role, name));
                logger.LogInformation("register success");
            }
            else
            {
                logger.LogError("have been exists");
                throw new UserExistException();
            }
        }
        public void ChangeInfo(int id, string username, string password, string role, string name)
        {
            logger.LogInformation("change info started");
            User? u = userRepo.readById(id);
            if (u != null)
            {
                u.Login = username;
                u.Password = password;
                u.Role = role;
                u.Name = name;
                userRepo.update(u);
                logger.LogInformation("change info ended");
            }
            else
            {
                logger.LogError("read user by id failed");
                throw new UserNotFoundException();
            }
        }
        public void deleteUser(User user)
        {
            logger.LogInformation("delete started");
            if (userRepo.readById(user.Id) == null)
            {
                logger.LogError("delete failed");
                throw new UserNotFoundException();
            }
            else
            {
                userRepo.delete(user);
                logger.LogInformation("delete ended");
            }
        }
        public List<User> getAll()
        {
            logger.LogInformation("get all users started");
            return userRepo.readByRole("Referee");
        }
        public User getbyId(int id)
        {
            logger.LogInformation("get user by id started");
            var user = userRepo.readById(id);
            if ( user == null )
            {
                logger.LogError("get user by id failed");
                throw new UserNotFoundException();
            }
            else
            {
                logger.LogInformation("get user by id ended");
                return user;
            }
        }
    }
}
