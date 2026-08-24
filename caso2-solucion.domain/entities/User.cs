using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.domain.entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; } 

        private User() { } 

        public User(string username, string passwordHash, string role = "User")
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
