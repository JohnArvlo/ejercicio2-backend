using caso2_solucion.domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}
