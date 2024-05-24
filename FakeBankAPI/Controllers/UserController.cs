using FakeBankAPI.Models;
using FakeBankAPI.Models.DTOs;
using FakeBankAPI.Repo;
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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepo userRepo, ILogger<UserController> logger)
        {
            _userRepo = userRepo;
            _response = new APIResponse();
            _logger = logger;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO requestDTO)
        {
            _logger.LogWarning("Login attempt for {username}", requestDTO.UserName); //logs the login attempt

            var loginResponse = await _userRepo.Login(requestDTO); //gets the login response

            //if no user is found, return an error message
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _logger.LogError("Login failed for {username}", requestDTO.UserName); //logs the failed login attempt

                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid username or password");
                return BadRequest(_response);
            }

            _logger.LogWarning("Login successful for {username}", requestDTO.UserName); //logs the successful login attempt

            _response.Result = loginResponse;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegisterationRequestDTO requestDTO)
        {
            _logger.LogWarning("Registration attempt(USER_CONTROLLER) for {username}", requestDTO.UserName); //logs the registration attempt

            bool unique = _userRepo.IsUniqueUser(requestDTO.UserName); //checks if the user is unique
            if (!unique)
            {
                _logger.LogError("Registration failed for {username}", requestDTO.UserName); //logs the failed registration attempt

                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("User already exists");
                return BadRequest(_response);
            }
            //_logger.LogCritical("requestDTOfor {username} : ", requestDTO.UserName);
            var user = await _userRepo.Register(requestDTO); //registers the user
            //_logger.LogCritical("User for {username}: ", user.UserName);
            if (user == null || user.Id == null)
            {
                _logger.LogError("Registration failed for {username}", user.UserName); //logs the failed registration attempt

                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Registration failed - user DNE");
                return BadRequest(_response);
            }

            _logger.LogWarning("Registration successful(USER_CONTROLLER) for {username}", requestDTO.UserName); //logs the successful registration attempt

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = user;
            return Ok(_response);
        }
    }
}
