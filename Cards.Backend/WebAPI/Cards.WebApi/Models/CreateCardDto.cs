using AutoMapper;
using Cards.Application.Cards.Commands.CreateCard;
using Cards.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Cards.WebApi.Models
{
    public class CreateCardDto : IMapWith<CreateCardCommand>
    {
        [Required]
        public string Title { get; set; }
        public string? Details { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCardDto, CreateCardCommand>()
                .ForMember(dst => dst.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dst => dst.Details,
                    opt => opt.MapFrom(src => src.Details));
        }
    }
}
