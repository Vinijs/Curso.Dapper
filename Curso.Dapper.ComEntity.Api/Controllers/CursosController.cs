using Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;
using Curso.Dapper.ComEntity.Api.Domain.Queries.Curso;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Dapper.ComEntity.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class CursosController : ControllerBase
{
    private readonly IMediator _mediator;

    public CursosController(IMediator mediator) 
        => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new ObterTodosCursosQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CadastrarCursoCommand command)
    {
        await _mediator.Publish(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AlterarCursoCommand command)
    {
        await _mediator.Publish(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _ = await _mediator.Send(new RemoverCursoCommand(id));
        return Ok();
    }
}
