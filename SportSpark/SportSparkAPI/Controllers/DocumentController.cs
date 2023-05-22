using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[ActionFilters.AuthorizationFilter()]
    public class DocumentController : BaseController
    {
        private readonly IDocumentService _documentService;
        private readonly IUserService _userService;
        public DocumentController(IDocumentService documentService, IUserService userService)
        {
            _documentService = documentService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DocumentDTO documentDTO)
        {
            try
            {
                var newDocumentId = await _documentService.CreateAsync(documentDTO);

                await _userService.UpdateProfilePictureAsync(documentDTO.Owner.Id, newDocumentId);

                return NoContent();
            }
            catch (Exception ex) 
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }
    }
}
