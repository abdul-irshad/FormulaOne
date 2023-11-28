using AutoMapper;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;

namespace FormulaOne.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<CreateDriverAchievementRequest, Achievement>()
                .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins))
                .ForMember(dest => dest.PolePosition, opt => opt.MapFrom(src => src.PolePosition))
                .ForMember(dest => dest.FastestLap, opt => opt.MapFrom(src => src.FastestLap))
                .ForMember(dest => dest.WorldChampionship, opt => opt.MapFrom(src => src.WorldChampionship))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(scr => 1))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(scr => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                ;

            CreateMap<UpdateDriverAchievementRequest, Achievement>()
                .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                ;

            CreateMap<CreateDriverRequest, Driver>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(scr => DateTime.UtcNow))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(scr => scr.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(scr => scr.LastName))
                .ForMember(dest => dest.DriverNumber, opt => opt.MapFrom(scr => scr.DriverNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(scr => scr.DateOfBirth))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                ;

            // CreateMap<UpdateDriverRequest, Driver>()
            //     .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            //     ;
        }
    }
}