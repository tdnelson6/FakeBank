using FakeBankAPI.Models;
using FakeBankAPI.Models.DTOs;
using FakeBankAPI.Repo.RepoFunctionBase;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FakeBankAPI.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    [ApiVersionNeutral]//runs with all versions
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        protected APIResponse _response;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _response = new APIResponse();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO requestDTO)
        {
            var loginResponse = await _userRepo.Login(requestDTO); //gets the login response

            //if no user is found, return an error message
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid username or password");
                return BadRequest(_response);
            }

            try
            {
                _response.Result = loginResponse;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(e.ToString());
            }
            return _response;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegisterationRequestDTO requestDTO)
        {
            bool unique = _userRepo.IsUniqueUser(requestDTO.UserName); //checks if the user is unique
            if (!unique)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("User already exists");
                return BadRequest(_response);
            }

            var user = await _userRepo.Register(requestDTO); //registers the user
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Registration failed");
                return BadRequest(_response);
            }

            try
            {
                _response.Result = user;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(e.ToString());
            }
            return _response;
        }
    }
}
