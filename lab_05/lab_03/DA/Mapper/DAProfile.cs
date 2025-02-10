using AutoMapper;
using lab_03.BL.Models;
using lab_03.DA.Entities;

namespace lab_03.DA.Mapper
{
    public class DAProfile : Profile
    {
        public DAProfile() 
        {
            CreateMap<User, UserDb>();
            CreateMap<UserDb, User>();

            CreateMap<Club, ClubDb>();
            CreateMap<ClubDb, Club>();

            CreateMap<League, LeagueDb>();
            CreateMap<LeagueDb, League>();

            CreateMap<Match, MatchDb>();
            CreateMap<MatchDb, Match>();
        }
    }
}
