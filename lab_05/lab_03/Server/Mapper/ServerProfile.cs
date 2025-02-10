using AutoMapper;
using lab_03.BL.Models;
using lab_03.Server.DTO;

namespace lab_03.Server.Mapper
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<ClubDto, Club>();
            CreateMap<UserDto, User>();
            CreateMap<LeagueDto, League>();
            CreateMap<MatchDto, Match>();

            CreateMap<Club, ClubDto>();
            CreateMap<User, UserDto>();
            CreateMap<League, LeagueDto>();
            CreateMap<Match, MatchDto>();

            CreateMap<RawUserDto, User>();
            CreateMap<RawMatchDto, Match>();
            CreateMap<RawLeagueDto, League>();
            CreateMap<RawClubDto, Club>();

            CreateMap<RawChangePassword, User>();
        }
    }
}
