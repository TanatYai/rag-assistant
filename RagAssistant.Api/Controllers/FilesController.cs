using MediatR;
using Microsoft.AspNetCore.Mvc;
using RagAssistant.Api.ApiDataObjects.ExtractFileToText;

namespace RagAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("extract-to-text", Name = nameof(ExtractFileToText))]
        public async Task<ExtractFileToTextQueryResponse> ExtractFileToText([FromBody] ExtractFileToTextQueryRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
