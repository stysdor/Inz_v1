﻿
namespace API.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class UserCredential {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
