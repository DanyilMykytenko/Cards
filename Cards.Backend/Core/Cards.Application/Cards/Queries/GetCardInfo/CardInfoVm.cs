using Cards.Application.Interfaces;
using System;
using Cards.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Cards.Application.Cards.Queries.GetCardInfo
{
    public class CardInfoVm : IMapWith<Card>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Card, CardInfoVm>()
                .ForMember(dst => dst.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dst => dst.Details,
                    opt => opt.MapFrom(src => src.Details))
                .ForMember(dst => dst.CreationDate,
                    opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dst => dst.EditDate,
                    opt => opt.MapFrom(src => src.EditDate));
        }
    }
}
