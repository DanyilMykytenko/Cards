using AutoMapper;
using Cards.Application.Interfaces;
using Cards.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Cards.Queries.GetCardList
{
    public class CardLookupDto : IMapWith<Card>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Card, CardLookupDto>()
                .ForMember(dst => dst.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Title,
                    opt => opt.MapFrom(src => src.Title));
        }
    }
}
