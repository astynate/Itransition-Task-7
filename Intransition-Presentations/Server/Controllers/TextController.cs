using Instend.Server.Database.Abstraction;
using Itransition.Server.Hubs;
using Itrantion.Server.Models;
using Itrantion.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Instend.Server.Controllers
{
    [ApiController]
    [Route("/api/texts")]
    public class TextController : ControllerBase
    {
        private readonly ISlidesRepository _slidesRepository;

        private readonly IAccessHandler _accessHandler;

        private readonly IHubContext<UserHub> _userHub;

        public TextController(ISlidesRepository slidesRepository, IHubContext<UserHub> userHub, IAccessHandler accessHandler)
        {
            _slidesRepository = slidesRepository;
            _userHub = userHub;
            _accessHandler = accessHandler;
        }

        [HttpPost]
        public async Task<IActionResult> AddText([FromForm] TextModel model)
        {
            await _slidesRepository.AddText(model);

            await _userHub.Clients
                .Group(model.PresentationId.ToString())
                .SendAsync("AddText", SerializationHelper.SerializeWithCamelCase(model));

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteText([FromForm] Guid presentationId, [FromForm] Guid id)
        {
            await _slidesRepository.DeleteText(id);

            await _userHub.Clients
                .Group(presentationId.ToString())
                .SendAsync("DeleteText", id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateText([FromForm] TextModel model)
        {
            await _slidesRepository.UpdateText(model);

            await _userHub.Clients
                .Group(model.PresentationId.ToString())
                .SendAsync("UpdateText", SerializationHelper.SerializeWithCamelCase(model));

            return Ok();
        }
    }
}