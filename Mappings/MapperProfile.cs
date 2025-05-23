﻿using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;

namespace IPLAwardManagementSystem.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Team
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Team, TeamCreateDto>().ReverseMap();
            CreateMap<Team, TeamUpdateDto>().ReverseMap();
            CreateMap<Team, TeamListDto>()
                .ForMember(dest => dest.PlayerCount, opt => opt.MapFrom(src => src.Players.Count))
                .ReverseMap();
            CreateMap<Team, TeamDetailDto>().ReverseMap();

            // Player
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Player, PlayerCreateDto>().ReverseMap();
            CreateMap<Player, PlayerUpdateDto>().ReverseMap();
            CreateMap<Player, PlayerListDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName));

            // ✅ Player details mapping including awards and team name
            CreateMap<Player, PlayerDetailDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName))
                .ForMember(dest => dest.Awards, opt => opt.MapFrom(src => src.PlayerAwards));

            // Match
            CreateMap<Match, MatchDto>()
                .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Venue.Name))
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.TeamName))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.TeamName));
            CreateMap<Match, MatchCreateDto>().ReverseMap();
            CreateMap<Match, MatchUpdateDto>().ReverseMap();

            // Venue
            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<Venue, VenueCreateDto>().ReverseMap();
            CreateMap<Venue, VenueUpdateDto>().ReverseMap();

            // Award
            CreateMap<Award, AwardDto>().ReverseMap();
            CreateMap<Award, AwardCreateDto>().ReverseMap();
            CreateMap<Award, AwardUpdateDto>().ReverseMap();

            // Voter
            CreateMap<Voter, VoterDto>();
            CreateMap<VoterDto, Voter>();

            CreateMap<Voter, VoterCreateDto>().ReverseMap();
            CreateMap<Voter, VoterUpdateDto>().ReverseMap();


            // Vote
            CreateMap<Vote, VoteDto>()
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.Name))
                .ForMember(dest => dest.AwardName, opt => opt.MapFrom(src => src.Award.Name))
                .ForMember(dest => dest.VoterName, opt => opt.MapFrom(src => src.Voter.Name));
            CreateMap<Vote, VoteCreateDto>().ReverseMap();
            CreateMap<Vote, VoteUpdateDto>().ReverseMap();

            // ✅ PlayerAward mapping with Year from DateTime
            CreateMap<PlayerAward, PlayerAwardDto>()
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.Name))
                .ForMember(dest => dest.AwardName, opt => opt.MapFrom(src => src.Award.Name))
                .ForMember(dest => dest.NominationDate, opt => opt.MapFrom(src => src.NominationDate))
                .ForMember(dest => dest.VotesReceived, opt => opt.MapFrom(src => src.VotesReceived))
                .ForMember(dest => dest.IsWinner, opt => opt.MapFrom(src => src.IsWinner))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year.Year)); // ✅ Correctly extract int year

            CreateMap<PlayerAwardDto, PlayerAward>()
                .ForMember(dest => dest.Player, opt => opt.Ignore())
                .ForMember(dest => dest.Award, opt => opt.Ignore());

            CreateMap<PlayerAward, PlayerAwardCreateDto>().ReverseMap();
            CreateMap<PlayerAward, PlayerAwardUpdateDto>().ReverseMap();

            // VenueTeam
            CreateMap<VenueTeam, VenueTeamDto>().ReverseMap();
            CreateMap<VenueTeam, VenueTeamCreateDto>().ReverseMap();
        }
    }
}
