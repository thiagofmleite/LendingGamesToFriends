using AutoMapper;
using LendGamesToMyFriends.Models;

namespace LendGamesToMyFriends.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<User, UserReference>().ReverseMap();
            #endregion
        }
    }
}