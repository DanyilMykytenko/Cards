using AutoMapper;
using Cards.Application.Cards.Commands.UpdateCard;
using Cards.Application.Interfaces;

namespace Cards.WebApi.Models
{
    public class UpdateCardDto : IMapWith<UpdateCardCommand>
    {
        public string Title { get; set; }
        public string? Details { get; set; }
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCardDto, UpdateCardCommand>()
                .ForMember(dst => dst.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dst => dst.Details,
                    opt => opt.MapFrom(src => src.Details))
                .ForMember(dst => dst.Id,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}
