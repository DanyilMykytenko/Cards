using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Cards.Queries.GetCardInfo
{
    public class GetCardInfoQuery : IRequest<CardInfoVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
