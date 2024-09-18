using lab_03.BL.Models;
using lab_03.BL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_01.TechUI
{
    public class TechUI
    {
        private UserService _userService;
        private ClubService _clubService;
        private MatchService _matchService;
        private LeagueService _leagueService;
        private IConfiguration _configuration;

        public TechUI(UserService userService, ClubService clubService, MatchService matchService, LeagueService leagueService, IConfiguration configuration)
        {
            _userService = userService;
            _clubService = clubService;
            _matchService = matchService;
            _leagueService = leagueService;
            _configuration = configuration;
        }
        public void openMainView()
        {
            int choice;
            do
            {
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Guest");
                Console.WriteLine("2. Sign in");
                Console.WriteLine("3. Sign up");
                Console.WriteLine("Command: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        //forGuest();
                        break;
                    case 2:
                        signIn();
                        break;
                    case 3:
                        signUp();
                        break;
                    case 0:
                        //exit();
                        break;
                    default:
                        Console.WriteLine("don't exists");
                        break;
                }
            } while (choice != 0);
        }
        public void signIn()
        {
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            User user = _userService.Login(username, password);
            if (user != null)
            {
                Console.WriteLine(user.Name);
            }
            else
            {
                Console.WriteLine("username or password invalid");
            }
        }
        public void signUp()
        {
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            string password, repassword;
            do
            {
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();
                Console.WriteLine("Reenter password: ");
                repassword = Console.ReadLine();
            } while (password != repassword);
            Console.WriteLine("Enter role: ");
            string role = Console.ReadLine();
            _userService.Register(username, password, role);
        }
    }
}
