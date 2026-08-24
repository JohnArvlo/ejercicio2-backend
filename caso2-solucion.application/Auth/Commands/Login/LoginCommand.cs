using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Users.Commands.Login
{
    public record LoginCommand(string Username, string Password) : IRequest<string>;
}
