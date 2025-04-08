// Mappings/MappingProfile.cs
using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;

namespace IPLAwardManagementSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Award mappings
            CreateMap<AwardCreateDto, Award>();
            CreateMap<AwardUpdateDto, Award>();
            CreateMap<Award, AwardDto>();

            // Player mappings
            CreateMap<PlayerCreateDto, Player>();
            CreateMap<PlayerUpdateDto, Player>();
            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.Awards, opt => opt.MapFrom(src => src.PlayerAwards.Select(pa => pa.Award)));

            // Team mappings
            CreateMap<TeamCreateDto, Team>();
            CreateMap<TeamUpdateDto, Team>();
            CreateMap<Team, TeamDto>();

            // Venue mappings
            CreateMap<VenueCreateDto, Venue>();
            CreateMap<VenueUpdateDto, Venue>();
            CreateMap<Venue, VenueDto>();

            // Match mappings
            CreateMap<MatchCreateDto, Match>();
            CreateMap<MatchUpdateDto, Match>();
            CreateMap<Match, MatchDto>();

            // Voter mappings
            CreateMap<VoterCreateDto, Voter>();
            CreateMap<VoterUpdateDto, Voter>();
            CreateMap<Voter, VoterDto>();

            // Vote mappings
            CreateMap<VoteCreateDto, Vote>();
            CreateMap<Vote, VoteDto>();

            // PlayerAward mappings
            CreateMap<PlayerAwardCreateDto, PlayerAward>();
            CreateMap<PlayerAwardUpdateDto, PlayerAward>();
            CreateMap<PlayerAward, PlayerAwardDto>();
        }
    }
}