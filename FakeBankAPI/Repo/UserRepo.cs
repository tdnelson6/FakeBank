using AutoMapper;
using FakeBankAPI.BaseData;
using FakeBankAPI.Models;
using FakeBankAPI.Models.DTOs;
using FakeBankAPI.Repo.RepoFunctionBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FakeBankAPI.Repo
{
    public class UserRepo : IUserRepo //handles user registration and login functionality
    {
        private readonly DBContext _data;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepo> _logger;

        public UserRepo(DBContext data, IConfiguration config, UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, ILogger<UserRepo> logger)
        {
            _data = data; //initializes the database context
            _userManager = userManager; //initializes the user manager
            //gets the secret key from the appsettings.json file
            secretKey = config.GetValue<string>("ApiSettings:Secret");
            _mapper = mapper; //initializes the mapper
            _roleManager = roleManager; //initializes the role manager
            _logger = logger;
        }

        public bool IsUniqueUser(string username) //checks if the user exists in the database
        {
            var user = _data.AppUsers.FirstOrDefault(x => x.UserName == username); 
            if (user == null)
            {
                return true; //returns true if the user does not exist
            }
            return false; //returns false if the user exists
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _data.AppUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower()); //gets the user from the database
            _logger.LogWarning("Login attempt (USER_REPO) for {username}", loginRequestDTO.UserName); //logs the login attempt

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password); //checks if the password is valid


            //if no user is found, return an empty token
            if (user == null || isValid == false)
            {
                _logger.LogError("Login failed for {username}, correctPass = {isValid} ", loginRequestDTO.UserName, isValid); //logs the failed login attempt

                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null 
                };
            }

            //if user is found, generate a token
            _logger.LogWarning("Login successful for {username}, generating token with key - {key}", loginRequestDTO.UserName, secretKey); //logs the successful login attempt
            var tokenHandler = new JwtSecurityTokenHandler(); //creates a new token handler
            var roles = await _userManager.GetRolesAsync(user); //gets the roles of the user
            var key = Encoding.ASCII.GetBytes(secretKey); //gets the secret key
            _logger.LogWarning("Token generated for {username}, Roles = {roles}", loginRequestDTO.UserName, roles); //logs the roles of the user

            //creates the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName), //adds the username to the token
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()) //adds the role to the token
                }),
                Expires = DateTime.UtcNow.AddDays(7), //sets the expiration time of the token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //signs the token
            };

            var token = tokenHandler.CreateToken(tokenDescriptor); //creates the token
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO() //creates a new login response DTO
            {
                Token = tokenHandler.WriteToken(token), //writes the token to the response DTO
                User = _mapper.Map<UserDTO>(user), //maps the user to the response DTO
            };
            return loginResponseDTO; //returns the response DTO

        }

        public async Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            AppUser user = new()
            {
                UserName = registerationRequestDTO.UserName,
                Email = registerationRequestDTO.Email,
                NormalizedEmail = registerationRequestDTO.Email.ToUpper(),
                PhoneNumber = registerationRequestDTO.PhoneNumber,
                NormalizedUserName = registerationRequestDTO.UserName.ToUpper()
            };
            _logger.LogWarning("Registration attempt (USER_REPO) for {username}", user.UserName);

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDTO.Password);
                if (result.Succeeded)
                {
                    _logger.LogWarning("Registration attempt in succeed (USER_REPO_test2) - {result}", result.Succeeded);

                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("user"));
                    }
                    await _userManager.AddToRoleAsync(user, "admin");
                    var userToReturn = _data.AppUsers.FirstOrDefault(u => u.UserName == registerationRequestDTO.UserName);
                    return _mapper.Map<UserDTO>(userToReturn);

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError("User creation error: {Code} - {Description}", error.Code, error.Description);
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError("Registration exception (USER_REPO) for {username}", e.ToString());
            }

            return new UserDTO();
        }
    }
}
