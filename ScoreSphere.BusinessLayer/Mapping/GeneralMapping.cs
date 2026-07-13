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
            // Team — özel alan yok, ReverseMap sorunsuz
            CreateMap<Team, ResultTeamDto>().ReverseMap();
            CreateMap<CreateTeamDto, Team>().ReverseMap();
            CreateMap<UpdateTeamDto, Team>().ReverseMap();

            // League — özel alan yok, ReverseMap sorunsuz
            CreateMap<League, ResultLeagueDto>().ReverseMap();
            CreateMap<CreateLeagueDto, League>().ReverseMap();
            CreateMap<UpdateLeagueDto, League>().ReverseMap();

            // Season — LeagueName özel alan, ters yönde Ignore lazım
            CreateMap<Season, ResultSeasonDto>()
                .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League!.LeagueName))
                .ReverseMap()
                .ForMember(dest => dest.League, opt => opt.Ignore());  // ters yönde League nav'ı doldurmaya çalışma

            CreateMap<CreateSeasonDto, Season>().ReverseMap();
            CreateMap<UpdateSeasonDto, Season>().ReverseMap();

            // Match — birden fazla özel alan var, ters yönde hepsini Ignore etmemiz lazım
            CreateMap<Match, ResultMatchDto>()
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam!.TeamName))
                .ForMember(dest => dest.HomeTeamLogoUrl, opt => opt.MapFrom(src => src.HomeTeam!.LogoUrl))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam!.TeamName))
                .ForMember(dest => dest.AwayTeamLogoUrl, opt => opt.MapFrom(src => src.AwayTeam!.LogoUrl))
                .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League!.LeagueName))
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
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.Ignore());  // Create Dto'da Status yok, ters yönde de yok say

            CreateMap<UpdateMatchDto, Match>().ReverseMap();

            // Goal — TeamName özel alan
            CreateMap<Goal, ResultGoalDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team!.TeamName))
                .ReverseMap()
                .ForMember(dest => dest.Team, opt => opt.Ignore())
                .ForMember(dest => dest.Match, opt => opt.Ignore());

            CreateMap<CreateGoalDto, Goal>().ReverseMap();

            // MatchEvent — EventType enum <-> string dönüşümü
            CreateMap<MatchEvent, ResultMatchEventDto>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => Enum.Parse<EventType>(src.EventType)))
                .ForMember(dest => dest.Match, opt => opt.Ignore());

            CreateMap<CreateMatchEventDto, MatchEvent>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => Enum.Parse<EventType>(src.EventType.ToString())))
                .ReverseMap()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()));

            // MatchStat — özel alan yok, ReverseMap sorunsuz
            CreateMap<MatchStat, ResultMatchStatDto>().ReverseMap();
            CreateMap<UpdateMatchStatDto, MatchStat>().ReverseMap();
        }
    }
}