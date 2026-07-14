using AutoMapper;
using ScoreSphere.DtoLayer.TeamDtos;
using ScoreSphere.DtoLayer.LeagueDtos;
using ScoreSphere.DtoLayer.SeasonDtos;
using ScoreSphere.DtoLayer.MatchDtos;
using ScoreSphere.DtoLayer.GoalDtos;
using ScoreSphere.DtoLayer.MatchEventDtos;
using ScoreSphere.DtoLayer.MatchStatDtos;
using ScoreSphere.EntityLayer.Entities;

namespace ScoreSphere.BusinessLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
           
            CreateMap<Team, ResultTeamDto>().ReverseMap();
            CreateMap<CreateTeamDto, Team>().ReverseMap();
            CreateMap<UpdateTeamDto, Team>().ReverseMap();

          
            CreateMap<League, ResultLeagueDto>().ReverseMap();
            CreateMap<CreateLeagueDto, League>().ReverseMap();
            CreateMap<UpdateLeagueDto, League>().ReverseMap();

  
            CreateMap<Season, ResultSeasonDto>()
                .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League!.LeagueName))
                .ReverseMap()
                .ForMember(dest => dest.League, opt => opt.Ignore());  

            CreateMap<CreateSeasonDto, Season>().ReverseMap();
            CreateMap<UpdateSeasonDto, Season>().ReverseMap();

     
            CreateMap<Match, ResultMatchDto>()
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam!.TeamName))
                .ForMember(dest => dest.HomeTeamLogoUrl, opt => opt.MapFrom(src => src.HomeTeam!.LogoUrl))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam!.TeamName))
                .ForMember(dest => dest.AwayTeamLogoUrl, opt => opt.MapFrom(src => src.AwayTeam!.LogoUrl))
                .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League!.LeagueName))
                .ForMember(dest => dest.LeagueCountry, opt => opt.MapFrom(src => src.League!.Country))
    .ForMember(dest => dest.LeagueLogoUrl, opt => opt.MapFrom(src => src.League!.LogoUrl))
                .ForMember(dest => dest.SeasonName, opt => opt.MapFrom(src => src.Season!.SeasonName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.HomeTeam, opt => opt.Ignore())
                .ForMember(dest => dest.AwayTeam, opt => opt.Ignore())
                .ForMember(dest => dest.League, opt => opt.Ignore())
                .ForMember(dest => dest.Season, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<MatchStatus>(src.Status)));

            CreateMap<CreateMatchDto, Match>()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => MatchStatus.Upcoming))
           .ReverseMap();


            CreateMap<UpdateMatchDto, Match>()
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<MatchStatus>(src.Status, true)));


            CreateMap<Goal, ResultGoalDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team!.TeamName))
                .ReverseMap()
                .ForMember(dest => dest.Team, opt => opt.Ignore())
                .ForMember(dest => dest.Match, opt => opt.Ignore());

            CreateMap<CreateGoalDto, Goal>().ReverseMap();

          
            CreateMap<MatchEvent, ResultMatchEventDto>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => Enum.Parse<EventType>(src.EventType)))
                .ForMember(dest => dest.Match, opt => opt.Ignore());

            CreateMap<CreateMatchEventDto, MatchEvent>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => Enum.Parse<EventType>(src.EventType.ToString())))
                .ReverseMap()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()));

     
            CreateMap<MatchStat, ResultMatchStatDto>().ReverseMap();
            CreateMap<UpdateMatchStatDto, MatchStat>().ReverseMap();

            CreateMap<Match, MatchDetailDto>()
    .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam!.TeamName))
    .ForMember(dest => dest.HomeTeamLogoUrl, opt => opt.MapFrom(src => src.HomeTeam!.LogoUrl))
    .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam!.TeamName))
    .ForMember(dest => dest.AwayTeamLogoUrl, opt => opt.MapFrom(src => src.AwayTeam!.LogoUrl))
    .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League!.LeagueName))
    .ForMember(dest => dest.SeasonName, opt => opt.MapFrom(src => src.Season!.SeasonName))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
    .ForMember(dest => dest.Stats, opt => opt.MapFrom(src => src.MatchStat))
    .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals))
    .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.MatchEvents));
        }
    }
}