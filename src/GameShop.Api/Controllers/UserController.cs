// using GameShop.Api.Contracts;
// using GameShop.Api.Contracts.Responses;
// using GameShop.Data.Contracts;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// using GameShop.Contracts.Entities;
// using GameShop.Api.Contracts.Requests;
// using GameShop.Api.Authorization.Repositories;

// namespace GameShop.Api.Controllers
// {
//     [Route("users")]
//     public class UserController : Controller
//     {
//         #region Declarations

//         private IUserRepository _userRepository;

//         #endregion Declarations

//         #region Constructors

//         public UserController(IUserRepository userRepository)
//         {
//             _userRepository = userRepository;
//         }

//         #endregion Constructors

//         #region Methods

//         [HttpGet]
//         public async Task<IActionResult> Get([FromQuery]Guid? id = null, [FromQuery]string username = null, [FromQuery]string email = null)
//         {
//             // No query parameters. Get all users.
//             if (HttpContext.Request.Query.Count == 0)
//             {
//                 return Ok(await _userRepository.GetAllUsersAsync());
//             }

//             User user = null;

//             if(id.HasValue)
//             {
//                 user = await _userRepository.FindUserByIdAsync(id.Value);
//             }
//             else if(!string.IsNullOrEmpty(username))
//             {
//                 user = await _userRepository.FindUserByUsernameAsync(username);
//             }
//             else if (!string.IsNullOrEmpty(email))
//             {
//                 user = await _userRepository.FindUserByEmailAsync(email);
//             }

//             // Check if a user is found.
//             if (user != null)
//             {
//                 // Do not send back account.
//                 user.Account = null;

//                 return Ok(user);
//             }

//             // User not found.
//             return NotFound(new ApiResponse()
//             {
//                 Result = Result.Failure,
//                 Description = "User not found."
//             });
//         }

//         [HttpPut("~/account")]
//         public async Task<IActionResult> UpdateAccount([FromBody]UpdateAccountRequest request)
//         {
//             Account account = await _userRepository.FindAccountByUserIdAsync(request.UserId);
//             account.Username = request.Username;
//             account.Email = request.Email;
//             account.PhoneNumber = request.PhoneNumber;

//             int result = await _userRepository.UpdateUserAccountAsync(request.UserId, account);
//             if(result > 0)
//             {
//                 // User successfuly updated.
//                 return Ok(new ApiResponse()
//                 {
//                     Result = Result.Success,
//                     Description = "Account successfully updated."
//                 });
//             }
//             else if(result == 0)
//             {
//                 return BadRequest(((UserRepository)_userRepository).Errors);
//             }

//             // User not found.
//             return NotFound(new ApiResponse()
//             {
//                 Result = Result.Failure,
//                 Description = $"User not found."
//             });
//         }

//         [HttpPut("~/profile")]
//         public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileRequest request)
//         {
//             Profile profile = await _userRepository.FindProfileByUserIdAsync(request.UserId);
//             profile.Name = request.Name;
//             profile.Gender = request.Gender;
//             profile.CivilStatus = request.CivilStatus;
//             profile.Birthday = request.Birthday;
//             profile.Addresses = request.Addresses;
//             profile.ContactInformation = request.ContactInformation;

//             int result = await _userRepository.UpdateUserProfileAsync(request.UserId, profile);
//             if (result > 0)
//             {
//                 // User successfuly updated.
//                 return Ok(new ApiResponse()
//                 {
//                     Result = Result.Success,
//                     Description = "Account successfully updated."
//                 });
//             }
//             else if (result == 0)
//             {
//                 return BadRequest(((UserRepository)_userRepository).Errors);
//             }

//             // User not found.
//             return NotFound(new ApiResponse()
//             {
//                 Result = Result.Failure,
//                 Description = $"User not found."
//             });
//         }

//         [HttpDelete("~/{id}")]
//         public async Task<IActionResult> Delete(Guid id)
//         {
//             User user = await _userRepository.FindUserByIdAsync(id);

//             if (user == null)
//             {
//                 return NotFound(new ApiResponse()
//                 {
//                     Result = Result.Failure,
//                     Description = $"User not found."
//                 });
//             }

//             int result = await _userRepository.DeleteUserByIdAsync(id);

//             if (result > 0)
//             {
//                 return Ok(new ApiResponse()
//                 {
//                     Result = Result.Success,
//                     Description = "User successfuly deleted."
//                 });
//             }
//             else if(result == 0)
//             {
//                 // Get errors.
//                 return BadRequest(((UserRepository)_userRepository).Errors);
//             }

//             // User not found.
//             return NotFound(new ApiResponse()
//             {
//                 Result = Result.Failure,
//                 Description = $"User not found."
//             });
//         }

//         [HttpPost("~/signup")]
//         public async Task<IActionResult> Signup([FromBody]SignupRequest signupRequest)
//         {
//             User user = new User();
//             user.UserId = Guid.NewGuid();
//             user.Account.Username = signupRequest.Username;
//             user.Account.Email = signupRequest.Email;
//             user.Account.PhoneNumber = signupRequest.PhoneNumber;
//             user.Profile = new Profile();
//             user.Profile.Name.Salutation = signupRequest.Salutation;
//             user.Profile.Name.FirstName = signupRequest.FirstName;
//             user.Profile.Name.MiddleName = signupRequest.MiddleName;
//             user.Profile.Name.LastName = signupRequest.LastName;
//             user.Profile.Name.Suffix = signupRequest.Suffix;
//             user.Profile.Gender = signupRequest.Gender;
//             user.Profile.Birthday = signupRequest.Birthday;

//             user.Profile.ContactInformation.Add(new ContactInformation()
//             {
//                 Email = signupRequest.Email,
//                 PhoneNumber = signupRequest.PhoneNumber
//             });

//             int result = await _userRepository.AddUserAsync(user, signupRequest.Password);
//             if(result > 0)
//             {
//                 User createdUser = await _userRepository.FindUserByUsernameAsync(user.Account.Username);

//                 return Ok(createdUser.Profile);
//             }
//             else
//             {
//                 return BadRequest(((UserRepository)_userRepository).Errors);
//             }

//             //Models.User user = new Models.User();
//             //user.UserName = signupRequest.Username;
//             //user.Email = signupRequest.Email;
//             //user.PhoneNumber = signupRequest.PhoneNumber;
//             //user.Profile = new Models.Profile();
//             //user.Profile.Salutation = signupRequest.Salutation;
//             //user.Profile.FirstName = signupRequest.FirstName;
//             //user.Profile.MiddleName = signupRequest.MiddleName;
//             //user.Profile.LastName = signupRequest.LastName;
//             //user.Profile.Suffix = signupRequest.Suffix;
//             //user.Profile.Gender = signupRequest.Gender;
//             //user.Profile.Birthday = signupRequest.Birthday;

//             //user.Profile.ContactInformation.Add(new Models.ProfileContactInformation()
//             //{
//             //    Id = Guid.NewGuid(),
//             //    Email = signupRequest.Email,
//             //    PhoneNumber = signupRequest.PhoneNumber,
//             //    CreatedDate = DateTime.Now,
//             //    ModifiedDate = DateTime.Now,

//             //    ProfileId = user.Profile.Id,
//             //    Profile = user.Profile
//             //});

//             //IdentityResult result = await _userManager.CreateAsync(user, signupRequest.Password);
//             //if(result.Succeeded)
//             //{
//             //    return Ok(_userRepository.FindUserByUsernameAsync(user.UserName));
//             //}
//             //else
//             //{
//             //    return BadRequest(result.Errors);
//             //}
//         }

//         #endregion Methods
//     }
// }
