using Curso.Dapper.ComEntity.Api.Domain.Common;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Application.Interfaces;

public interface ICommandHandler<TCommand> : INotificationHandler<TCommand> where TCommand : ICommand
{
}
