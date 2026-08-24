using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Users.Commands.Register
{
    public record RegisterCommand(string Username, string Password) : IRequest<Guid>;

}
