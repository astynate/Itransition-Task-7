using Instend.Server.Database.Abstraction;
using Itransition.Server.Hubs;
using Itrantion.Server.Database.Abstraction;
using Itrantion.Server.Models;
using Itrantion.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Itrantion.Server.Controllers
{
    [ApiController]
    [Route("/api/presentations")]
    public class PresentationsController : ControllerBase
    {
        private readonly IPresentationsRepository _presentationsRepository;

        private readonly ISlidesRepository _slidesRepository;

        private readonly IAccessHandler _accessHandler;

        private readonly IHubContext<UserHub> _userHub;

        public PresentationsController
        (
            IPresentationsRepository presentationsRepository, 
            ISlidesRepository slidesRepository,
            IHubContext<UserHub> userHub,
            IAccessHandler accessHandler
        )
        {
            _presentationsRepository = presentationsRepository;
            _slidesRepository = slidesRepository;
            _userHub = userHub;
            _accessHandler = accessHandler;
        }

        [HttpPut]
        public async Task<IActionResult> ChangeName([FromForm] Guid id, [FromForm] string username, [FromForm] string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid data");

            if (await _accessHandler.IsUserHasEditPermission(id, username) == false)
                return BadRequest("You has no rights to perform this operation");

            await _presentationsRepository.ChangeName(id, name);
            await _userHub.Clients.Group(id.ToString()).SendAsync("ChangeName", SerializationHelper.SerializeWithCamelCase(new { id, name }));

            return Ok();
        }

        public class ChangePermissionRequestBody
        {
            public Guid id { get; set; }
            public string username { get; set; } = string.Empty;
            public string user { get; set; } = string.Empty;
            public Permissions permission { get; set; }
        }

        [HttpPut]
        [Route("/api/presentation/permissions")]
        public async Task<IActionResult> ChangePermissions([FromForm] ChangePermissionRequestBody body)
        {
            if (string.IsNullOrEmpty(body.username) || string.IsNullOrWhiteSpace(body.username))
                return BadRequest("Invalid data");

            await _presentationsRepository.EditPermissions(body.id, body.user, body.username, body.permission);

            await _userHub.Clients.Group(body.id.ToString()).SendAsync("ChangePermissions", 
                SerializationHelper.SerializeWithCamelCase(new { body.id, body.user, permission = body.permission.ToString() }));

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPresentation(int page) 
            => Ok(await _presentationsRepository.GetPresentation(page));

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string username, [FromForm] int type)
        {
            var result = await _presentationsRepository.Create(username, type);

            if (result.error != null || result.instance == null)
                return BadRequest(result.error);

            return Ok(result.instance);
        }

        [HttpPost]
        [Route("/api/presentations/slide")]
        public async Task<IActionResult> AddSlide([FromForm] string username, [FromForm] Guid presentationId)
        {
            if (await _accessHandler.IsUserHasEditPermission(presentationId, username) == false)
                return BadRequest("You has no rights to perform this operation");

            var result = await _slidesRepository.AddSlide(presentationId, username);
            
            await _userHub.Clients
                .Group(presentationId.ToString())
                .SendAsync("AddSlide", SerializationHelper.SerializeWithCamelCase(result));

            return Ok();
        }

        [HttpDelete]
        [Route("/api/presentations/slide")]
        public async Task<IActionResult> DeleteSlide([FromForm] Guid slideId, [FromForm] Guid presentationId, [FromForm] string username)
        {
            if (await _accessHandler.IsUserHasEditPermission(presentationId, username) == false)
                return BadRequest("You has no rights to perform this operation");

            var result = await _slidesRepository
                .DeleteSlide(presentationId, slideId, username);

            await _userHub.Clients
                .Group(presentationId.ToString())
                .SendAsync("DeleteSlide", result);

            return Ok();
        }
    }
}