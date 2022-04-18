using API.DTO;
using AutoMapper;
using BusinessLogic.Interfaces;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public AuthController(IMapper mapper, IUserRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost("login")]
        public ActionResult ValidateUser([FromBody] UserCredential userCredentials)
        {
            var user = repository.ValidateUser(userCredentials.Email, userCredentials.Password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        [HttpGet("getUserNameByEmail")]
        public ActionResult GetUserByEmail(string email)
        {
            return Ok(repository.GetUserNameByEmail(email));
        }

        [HttpGet("getUser")]
        public ActionResult GetUser([FromQuery] string userName)
        {
            if (userName == null || userName == "null") return UnprocessableEntity();
            return Ok(repository.GetUser(userName));
        }


        [HttpPost("register")]
        public ActionResult InsertUser([FromBody] UserDTO user)
        {
            return Ok(repository.InsertUser(mapper.Map<UserDTO, User>(user)));
        }
    }
}
