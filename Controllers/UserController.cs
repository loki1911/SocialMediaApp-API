using Microsoft.AspNetCore.Mvc;
using MychatAPI.Data;
using MychatAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace MychatAPI.Controllers
{
  
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly IUserRepository _userRepository;

            public UserController(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            [HttpPost("signup")]
            public async Task<IActionResult> SignUp([FromBody] User user)
            {
                try
                {
                    if (await _userRepository.UserExistsAsync(user.username))
                    {
                        return BadRequest("Username already exists.");
                    }

                    user.password = EncryptPassword(user.password);
                    await _userRepository.CreateUserAsync(user);
                    return Ok(new {
                       
                        message = "User created successfully!" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new {  message = "An error occurred while creating the user.", error = ex.Message });
                }
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] User user)
            {
                try
                {
                    var existingUser = await _userRepository.GetUserByUsernameAsync(user.username);
                    var  userId = await _userRepository.GetUserByIdAsync(existingUser.userId);
                    if (existingUser == null || !VerifyPassword(user.password, existingUser.password))
                    {
                        return Unauthorized("Invalid username or password.");
                    }

                    return Ok(new {existingUser.userId, message = "Login successful!" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
                }
            }
                  [HttpGet]
                     public async Task<IActionResult> GetAllUsers()
                     {
                     var users = await _userRepository.GetAllUsersAsync();
                        return Ok(users);
                      }

        private string EncryptPassword(string password)
            {
                try
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                        return Convert.ToBase64String(bytes);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while encrypting password.", ex);
                }
            }

            private bool VerifyPassword(string enteredPassword, string storedPassword)
            {
                try
                {
                    return EncryptPassword(enteredPassword) == storedPassword;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while verifying password.", ex);
                }
            }
        }
    }

