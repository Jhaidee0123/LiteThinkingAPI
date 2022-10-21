using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using LiteThinking.Application.Articles.Commands;
using LiteThinking.Application.Articles.Queries;
using LiteThinking.Domain.Entities.Users;
using LiteThinking.Domain.Ports.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LiteThinking.Api.Controllers;

[Route("api/[controller]")]
[EnableCors("Cors")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISendGridClientService _sendGridClientService;

    public ArticlesController(IMediator mediator, ISendGridClientService sendGridClientService)
    {
        _mediator = mediator;
        _sendGridClientService = sendGridClientService;
    }

    /// <summary>
    /// POST /api/articles/create-article
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("create-article")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> CreateArticleAsync([FromBody] AddArticleToInventoryCommand command)
    {
        await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created);
    }

    /// <summary>
    /// GET /api/articles/export-articles
    /// </summary>
    /// <param name="inventoryId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("export-articles/{inventoryId}/{email}")]
    [Authorize]
    public async Task<IActionResult> ExportArticlesAsync([FromRoute] Guid inventoryId, [FromRoute] string email)
    {
        var template = await _mediator.Send(new ExportArticlesQuery(inventoryId));
        using (MemoryStream ms = new MemoryStream())
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            document.Open();
            using (var htmlWorker = new HTMLWorker(document))
            {
                using (var sr = new StringReader(template))
                {
                    htmlWorker.Parse(sr);
                }
            }
            document.Close();
            writer.Close();
            var fileBytes = ms.GetBuffer();
            await _sendGridClientService.SendEmail("francocicirelli97@gmail.com", "Inventario adjunto", email, fileBytes);
            return Ok(fileBytes);
        }
    }
}
