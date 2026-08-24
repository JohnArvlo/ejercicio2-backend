using caso2_solucion.domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
