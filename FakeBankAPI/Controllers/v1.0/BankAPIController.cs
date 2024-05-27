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
        private readonly ILogger<BankAPIController> _logger;

        //Constructor
        public BankAPIController(IAccountRepo accountData, IMapper mapper, ILogger<BankAPIController> logger)
        {
            _accountRepo = accountData; //initializes the account data
            _mapper = mapper; //initializes the mapper
            _response = new APIResponse(); //initializes the response
            _logger = logger;
        }

        [HttpGet(Name = "GetAccounts")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAccounts()
        {
            _logger.LogWarning("GetAccounts attempt"); //logs the get accounts attempt
            try
            {
                IEnumerable<Account> accounts = await _accountRepo.GetAllAsync(); //gets all the accounts
                _response.Result = _mapper.Map<List<AccountDTO>>(accounts); //maps the accounts to the account DTO
                //_response.IsSuccess = true; true by default
                _response.StatusCode = HttpStatusCode.OK; //sets the status code

                _logger.LogWarning("GetAccounts successful - {response}", _response.Result); //logs the successful get accounts attempt

                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(e.ToString());
            }
            _logger.LogError("GetAccounts failed"); //logs the failed get accounts attempt
            return _response;
        }

        [HttpGet("{AccountNumber:int}", Name = "GetAccount")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAccount(int AccountNumber)
        {
            _logger.LogWarning("GetAccount attempt for {AccountNumber}", AccountNumber); //logs the get account attempt

            try
            {
                if (AccountNumber == null)
                {
                    _logger.LogError("GetAccount failed for {AccountNumber}, no account number entered", AccountNumber); //logs the failed get account attempt

                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the id is default
                    return BadRequest(_response); //returns bad request
                }
                //gets the account otherwise
                var account = await _accountRepo.GetAsync(x => x.AccountNumber == AccountNumber); //gets the account by the account number

                if (account == null) //checks if the account exists
                {
                    _logger.LogError("GetAccount failed for {AccountNumber}, account DNE", AccountNumber); //logs the failed get account attempt

                    _response.StatusCode = HttpStatusCode.NotFound; //sets the status code to not found if the account is not found
                    return NotFound(_response); //returns not found
                }
                _logger.LogWarning("Account found for {AccountNumber}", AccountNumber); //logs the account found

                _response.Result = _mapper.Map<AccountDTO>(account); //maps the account to the account DTO
                _response.StatusCode = HttpStatusCode.OK; //sets the status code

                _logger.LogWarning("GetAccount successful for {AccountNumber}", AccountNumber); //logs the successful get account attempt

                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _logger.LogError("GetAccount failed for {AccountNumber}, {error}", AccountNumber, e); //logs the failed get account attempt

                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
            }
            return _response; //returns the response if it fails
        }

        [HttpPost(Name = "CreateAccount")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAccount([FromBody] AccountCreateDTO createDTO)
        {
            _logger.LogWarning("CreateAccount attempt for {AccountNumber}", createDTO.AccountNumber); //logs the create account attempt
            try
            {
                foreach (Account acc in await _accountRepo.GetAllAsync()) //checks if the account number already exists
                {
                    if (acc.AccountNumber == createDTO.AccountNumber)
                    {
                        _logger.LogError("CreateAccount failed for {AccountNumber}, account number already exists", createDTO.AccountNumber); //logs the failed create account attempt
                        _response.IsSuccess = false; //sets the response to false
                        _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request
                        _response.ErrorMessages.Add("Account Number already exists"); //adds the error message
                        _response.Result = createDTO.AccountNumber;
                        return BadRequest(_response); //returns bad request
                    }
                }
                /*if (await _accountRepo.GetAsync(x => x.AccountNumber == createDTO.AccountNumber) != null) //checks if the account number already exists
                {
                    _logger.LogError("CreateAccount failed for {AccountNumber}, account number already exists", createDTO.AccountNumber); //logs the failed create account attempt
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request
                    _response.ErrorMessages.Add("Account Number already exists"); //adds the error message
                    _response.Result = createDTO.AccountNumber;
                    return BadRequest(_response); //returns bad request
                }*/
                if (createDTO == null)
                {
                    _logger.LogError("CreateAccount failed, no account number entered"); //logs the failed create account attempt
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the DTO is null
                    return BadRequest(_response); //returns bad request
                }
                Account account = _mapper.Map<Account>(createDTO); //maps the DTO to the account
                await _accountRepo.CreateAsync(account); //creates the account
                _response.Result = "Success"; //maps the account to the account DTO
                _response.StatusCode = HttpStatusCode.Created; //sets the status code
                _response.IsSuccess = true; //sets the response to true

                _logger.LogWarning("CreateAccount successful for {AccountNumber}", createDTO.AccountNumber); //logs the successful create account attempt

                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _logger.LogError("CreateAccount failed for {AccountNumber}, {error}", createDTO.AccountNumber, e); //logs the failed create account attempt

                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
            }
            return _response; //returns the response if it fails
        }

        [HttpPut("{AccountNumber:int}", Name = "UpdateAccount")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateAccount(int AccountNumber, [FromBody] AccountUpdateDTO updateDTO)
        {
            _logger.LogWarning("UpdateAccount attempt for {AccountNumber}", AccountNumber); //logs the update account attempt
            try
            {
                if (AccountNumber == null || AccountNumber != updateDTO.AccountNumber)
                {
                    _logger.LogError("UpdateAccount failed for {AccountNumber}, no account number entered", AccountNumber); //logs the failed update account attempt
                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the id is default
                    return BadRequest(_response); //returns bad request
                }

                var account = _mapper.Map<Account>(updateDTO); //gets the account

                _logger.LogWarning("UpdateAccount for {AccountNumber} - {account}", AccountNumber, account); //logs the update account

                await _accountRepo.UpdateAsync(account);
                _response.StatusCode = HttpStatusCode.NoContent; //sets the status code
                _response.IsSuccess = true; //sets the response to true

                _logger.LogWarning("UpdateAccount successful for {AccountNumber}", AccountNumber); //logs the successful update account attempt

                return Ok(_response); //returns the response
            }
            catch (Exception e)
            {
                _logger.LogError("UpdateAccount failed for {AccountNumber}, {error}", AccountNumber, e); //logs the failed update account attempt
                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
            }
            return _response; //returns the response if it fails
        }

        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteAccount(int AccountNumber)
        {
            _logger.LogWarning("DeleteAccount attempt for {AccountNumber}", AccountNumber); //logs the delete account attempt
            try
            {
                if (AccountNumber == null || AccountNumber == 0)
                {
                    _logger.LogError("DeleteAccount failed for {AccountNumber}, no account number entered", AccountNumber); //logs the failed delete account attempt

                    _response.IsSuccess = false; //sets the response to false
                    _response.StatusCode = HttpStatusCode.BadRequest; //sets the status code to bad request if the id is default
                    return BadRequest(_response); //returns bad request
                }

                foreach (Account acc in await _accountRepo.GetAllAsync()) //checks if the account number already exists
                {
                    if (acc.AccountNumber == AccountNumber)
                    {
                        _logger.LogWarning("DeleteAccount for {AccountNumber}", AccountNumber); //logs the delete account

                        await _accountRepo.RemoveAsync(acc); //deletes the account
                        _response.StatusCode = HttpStatusCode.NoContent; //sets the status code
                        _response.IsSuccess = true; //sets the response to true
                        _response.Result = AccountNumber;
                        return Ok(_response); //returns the response
                    }
                }
                //not found
                _logger.LogError("DeleteAccount failed for {AccountNumber}, account DNE", AccountNumber); //logs the failed delete account attempt

                _response.IsSuccess = false; //sets the response to false
                _response.StatusCode = HttpStatusCode.NotFound; //sets the status code to not found
                _response.Result = AccountNumber;
                return NotFound(_response); //returns not found
            }
            catch (Exception e)
            {
                _logger.LogError("DeleteAccount failed for {AccountNumber}, {error}", AccountNumber, e); //logs the failed delete account attempt
                _response.IsSuccess = false; //sets the response to false
                _response.ErrorMessages.Add(e.ToString()); //adds the error message
                _response.Result = AccountNumber;
            }
            return _response; //returns the response if it fails
        }
    }
}
