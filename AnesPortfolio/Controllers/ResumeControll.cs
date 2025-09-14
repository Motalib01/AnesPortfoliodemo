using AnesPortfolio.Domain.Resume.Command;
using AnesPortfolio.Domain.Resume.Query;
using AnesPortfolio.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnesPortfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResumeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateResumeCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateResumeRequest request)
    {
        await _mediator.Send(new UpdateResumeCommand
        {
            Id = id,
            CV = request.CV
        });

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteResumeCommand { Id = id });
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resumes = await _mediator.Send(new GetAllResumesQuery());
        return Ok(resumes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var resume = await _mediator.Send(new GetResumeByIdQuery { Id = id });
        if (resume == null) return NotFound();
        return Ok(resume);
    }
}