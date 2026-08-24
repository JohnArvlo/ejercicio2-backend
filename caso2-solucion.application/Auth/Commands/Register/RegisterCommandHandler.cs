using caso2_solucion.application.Interfaces;
using caso2_solucion.domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetByUsernameAsync(request.Username);
            if (existing is not null)
                throw new InvalidOperationException("El usuario ya existe");

            var hash = _passwordHasher.Hash(request.Password);
            var user = new User(request.Username, hash);

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}
