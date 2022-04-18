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

        [HttpGet("getUserNameByEmail")]
        public ActionResult GetUserByEmail(string email)
        {
            return Ok(repository.GetUserNameByEmail(email));
        }

        [HttpGet("getUser/{userName}")]
        public ActionResult GetUser(string userName)
        {
            return Ok(repository.GetUser(userName));
        }

        [HttpPost("login")]
        public ActionResult ValidateUser([FromBody] UserCredential user)
        {
            bool isOk = repository.ValidateUser(user.Email, user.Password);
            if (isOk)
            {
                return Ok(true);
            }
            else {
                return UnprocessableEntity();
            }
        }

        [HttpPost("register")]
        public ActionResult InsertUser([FromBody] UserDTO user)
        {
            return Ok(repository.InsertUser(mapper.Map<UserDTO, User>(user)));
        }
    }
}
