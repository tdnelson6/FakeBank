using FakeBankAPI.Models.DTOs;

namespace FakeBankAPI.Repo.RepoFunctionBase
{
    public interface IUserRepo
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
