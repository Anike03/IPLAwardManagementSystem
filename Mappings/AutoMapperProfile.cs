// Mappings/AutoMapperProfile.cs
using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;

namespace IPLAwardManagementSystem.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Award Mappings
            CreateMap<Award, AwardDTO>().ReverseMap();
            CreateMap<AwardCreateDTO, Award>();
            CreateMap<AwardUpdateDTO, Award>();

            // Voter Mappings
            CreateMap<Voter, VoterDTO>().ReverseMap();
            CreateMap<VoterCreateDTO, Voter>();
            CreateMap<VoterUpdateDTO, Voter>();

            // Vote Mappings
            CreateMap<Vote, VoteDTO>()
                .ForMember(dest => dest.AwardName, opt => opt.MapFrom(src => src.Award != null ? src.Award.Name : ""))
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player != null ? src.Player.Name : ""))
                .ForMember(dest => dest.VoterName, opt => opt.MapFrom(src => src.Voter != null ? src.Voter.Name : ""));
            CreateMap<VoteCreateDTO, Vote>();

            // PlayerAward Mappings
            CreateMap<PlayerAward, PlayerAwardDTO>()
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player != null ? src.Player.Name : ""))
                .ForMember(dest => dest.AwardName, opt => opt.MapFrom(src => src.Award != null ? src.Award.Name : ""));
            CreateMap<PlayerAwardCreateDTO, PlayerAward>();
            CreateMap<PlayerAwardUpdateDTO, PlayerAward>();

            // Special mappings for complex DTOs
            CreateMap<Award, AwardWithNomineesDTO>()
                .ForMember(dest => dest.Nominees, opt => opt.MapFrom(src => src.PlayerAwards));
        }
    }
}
