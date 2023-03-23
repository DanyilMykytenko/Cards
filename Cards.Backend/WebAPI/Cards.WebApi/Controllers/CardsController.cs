using AutoMapper;
using System;
using System.Threading.Tasks;
using Cards.Application.Cards.Commands.CreateCard;
using Cards.Application.Cards.Queries.GetCardInfo;
using Cards.Application.Cards.Queries.GetCardList;
using Cards.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Cards.Application.Cards.Commands.UpdateCard;
using Cards.Application.Cards.Commands.DeleteCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Cards.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CardsController : BaseController
    {
        private readonly IMapper _mapper;
        public CardsController(IMapper mapper) =>
            _mapper = mapper;

          /// <summary>
        /// Gets the list of cards
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /card
        /// </remarks>
        /// <returns>Returns CardListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unathorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CardListVm>> GetAll()
        {
            var isAdmin = User.IsInRole("Admin");
            var query = new GetCardListQuery
            {
                UserId = !isAdmin
                    ? UserId
                    : null
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the card by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /card/38B7C901-D655-4BA5-A02C-A33128834093
        /// </remarks>
        /// <returns>Returns CardInfoVm</returns>
        /// <param name="id">Card id(guid)</param>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unathorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CardListVm>> Get(Guid id)
        {
            var query = new GetCardInfoQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        /// <summary>
        /// Creates the card
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /card
        /// {
        ///     fullName: "user full name"
        ///     details: "card info"
        /// }
        /// </remarks>
        /// <returns>Returns id(guid)</returns>
        /// <param name="createCardDto">createCardDto object</param>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unathorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCardDto createCardDto)
        {
            var command = _mapper.Map<CreateCardCommand>(createCardDto);
            command.UserId = UserId;
            var cardId = await Mediator.Send(command);
            return Ok(cardId);
        }
        /// <summary>
        /// Updates the card
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /card
        /// {
        ///     fullName: "updated user full name"
        ///     details: "updated card info"
        /// }
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="updateCardDto">updateCardDto object</param>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unathorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update([FromBody] UpdateCardDto updateCardDto)
        {
            var command = _mapper.Map<UpdateCardCommand>(updateCardDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Deletes the card
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /card/1928D1D8-6DE9-41C8-94FA-492D32FCDFB6
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="id">id of the card(guid)</param>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unathorized</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteCardCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
