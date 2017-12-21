using AutoMapper;

namespace LendGamesToMyFriends.Mappers
{
    public class AutoMappeConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });
        }
    }
}