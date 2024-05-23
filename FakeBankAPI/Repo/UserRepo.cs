using AutoMapper;
using FakeBankAPI.BaseData;
using FakeBankAPI.Models;
using FakeBankAPI.Models.DTOs;
using FakeBankAPI.Repo.RepoFunctionBase;
using Microsoft.AspNetCore.Identity;
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
        private string secretKey;
        private readonly IMapper _mapper;

        public UserRepo(DBContext data, IConfiguration config, UserManager<AppUser> userManager, IMapper mapper)
        {
            _data = data; //initializes the database context
            _userManager = userManager; //initializes the user manager
            secretKey = config.GetSection("AppSettings:Secret").Value; //gets the secret key from the appsettings.json file
            _mapper = mapper; //initializes the mapper
        }

        public bool IsUniqueUser(string username) //checks if the user exists in the database
        {
            var user = _data.Users.FirstOrDefault(x => x.UserName == username); 
            if (user == null)
            {
                return true; //returns true if the user does not exist
            }
            return false; //returns false if the user exists
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _data.Users.FirstOrDefault(x => x.UserName == loginRequestDTO.UserName); //gets the user from the database

            bool isvalid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password); //checks if the password is valid

            //if no user is found, return an empty token
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null 
                };
            }

            //if user is found, generate a token
            var tokenHandler = new JwtSecurityTokenHandler(); //creates a new token handler
            var key = Encoding.ASCII.GetBytes(secretKey); //gets the secret key
            var roles = await _userManager.GetRolesAsync(user); //gets the roles of the user

            //creates the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), //adds the user id to the token
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
                Role = roles.FirstOrDefault() //adds the role to the response DTO
            };
            return loginResponseDTO; //returns the response DTO

        }

        public async Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            AppUser user = new()//creates a new user
            {
                UserName = registerationRequestDTO.UserName, //sets the username
                Email = registerationRequestDTO.Email.ToLower(), //sets the email normalized
                PhoneNumber = registerationRequestDTO.PhoneNumber, //sets the phone number
            };

            try
            {
                var result = _userManager.CreateAsync(user, registerationRequestDTO.Password).Result; //creates the user
                if (result.Succeeded) //if the user is created successfully
                {
                    await _userManager.AddToRoleAsync(user, registerationRequestDTO.Role); //adds the role to the user
                    var returnUser = _data.Users.FirstOrDefault(x => x.UserName == registerationRequestDTO.UserName); //gets the user from the database
                    return _mapper.Map<UserDTO>(returnUser); //returns the user
                }
                else
                {
                    throw new Exception("User creation failed! Please check user details and try again."); //throws an exception if the user creation fails
                }
            }
            catch (Exception e)
            {
                //TODO log error
            }
            return new UserDTO(); //returns an empty user DTO
        }
    }
}
