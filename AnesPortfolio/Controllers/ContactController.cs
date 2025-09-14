using AnesPortfolio.Domain.Contact.Command;
using AnesPortfolio.Domain.Contact.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnesPortfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var contact = await _mediator.Send(new GetContactQuery());
        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertContactCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}