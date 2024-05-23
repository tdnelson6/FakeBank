using AutoMapper;
using FakeBankAPI.Models;
using FakeBankAPI.Models.DTOs;
using FakeBankAPI.Repo.RepoFunctionBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Net;

namespace FakeBankAPI.Controllers.v1._0
{
    [Route("api/BankAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BankAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;

        //Constructor
        public BankAPIController(IAccountRepo accountData, IMapper mapper)
        {
            _accountRepo = accountData; //initializes the account data
            _mapper = mapper; //initializes the mapper
            _response = new APIResponse(); //initializes the response
        }

        [HttpGet(Name = "GetAccounts")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAccounts()
        {
            try
            {
                IEnumerable<Account> accounts = await _accountRepo.GetAllAsync(); //gets all the accounts
                _response.Result = _mapper.Map<List<AccountDTO>>(accounts); //maps the accounts to the account DTO
                //_response.IsSuccess = true; true by default
                _response.StatusCode = HttpStatusCode.OK; //sets the status code
                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(e.ToString());
            }
            return _response;
        }

        [HttpGet("{AccountNumber:int}", Name = "GetAccount")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAccount(int AccountNumber)
        {
            try
            {
                if (AccountNumber == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the id is default
                    return BadRequest(_response); //returns bad request
                }
                //gets the account otherwise
                var account = await _accountRepo.GetAsync(x => x.AccountNumber == AccountNumber.ToString()); //gets the account by the account number
                if (account == null) //checks if the account exists
                {
                    _response.StatusCode = HttpStatusCode.NotFound; //sets the status code to not found if the account is not found
                    return NotFound(_response); //returns not found
                }

                _response.Result = _mapper.Map<AccountDTO>(account); //maps the account to the account DTO
                _response.StatusCode = HttpStatusCode.OK; //sets the status code
                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
            }
            return _response; //returns the response if it fails
        }

        [HttpPost(Name = "CreateAccount")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "client")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAccount([FromBody] AccountCreateDTO createDTO)
        {
            try
            {
                if (await _accountRepo.GetAsync(x => x.AccountNumber.ToString().ToLower() == createDTO.AccountNumber.ToString().ToLower()) != null) //checks if the account number already exists
                {
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request
                    _response.ErrorMessages.Add("Account Number already exists"); //adds the error message
                    return BadRequest(_response); //returns bad request
                }
                if (createDTO == null)
                {
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the DTO is null
                    return BadRequest(_response); //returns bad request
                }
                Account account = _mapper.Map<Account>(createDTO); //maps the DTO to the account
                await _accountRepo.CreateAsync(account); //creates the account
                _response.Result = _mapper.Map<AccountDTO>(account); //maps the account to the account DTO
                _response.StatusCode = HttpStatusCode.Created; //sets the status code
                return CreatedAtRoute("GetAccount", new { id = account.Id }, _response); //returns the response
            }
            catch (Exception e)
            {
                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
            }
            return _response; //returns the response if it fails
        }

        [HttpPut("{AccountNumber:int}", Name = "UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateAccount(int AccountNumber, [FromBody] AccountUpdateDTO updateDTO)
        {
            try
            {
                if (AccountNumber == 0 || AccountNumber != updateDTO.AccountNumber)
                {
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the id is default
                    return BadRequest(_response); //returns bad request
                }

                var account = _mapper.Map<Account>(updateDTO); //gets the account

                await _accountRepo.UpdateAsync(account);
                _response.StatusCode = HttpStatusCode.NoContent; //sets the status code
                _response.IsSuccess = true; //sets the response to true
                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
            }
            return _response; //returns the response if it fails
        }

        [HttpDelete("{AccountNumber:int}", Name = "DeleteAccount")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteAccount(int AccountNumber)
        {
            try
            {
                if (AccountNumber == 0)
                {
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the id is default
                    return BadRequest(_response); //returns bad request
                }

                var account = await _accountRepo.GetAsync(x => x.AccountNumber == AccountNumber.ToString()); //gets the account by the account number

                if (account == null) //checks if the account exists
                {
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.NotFound; //sets the status code to not found if the account is not found
                    return NotFound(_response); //returns not found
                }

                await _accountRepo.RemoveAsync(account); //deletes the account
                _response.StatusCode = HttpStatusCode.NoContent; //sets the status code
                _response.IsSuccess = true; //sets the response to true
                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
            }
            return _response; //returns the response if it fails
        }
    }
}
