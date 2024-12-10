using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SneakerShop_Core.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {

        private UserService.User.UserClient _userClient;

        public UserController(UserService.User.UserClient userClient)
        {
            this._userClient = userClient;
        }

        [HttpPost]
        public async Task<UserService.CreateUserReply> Create(UserService.CreateUserRequest createUserRequest)
        {
            var result = await _userClient.AddUserAsync(createUserRequest);
            return result;
        }

        [HttpGet("paginate")]
        public async Task<UserService.GetUserPaginateReply> GetUserPaginate([FromQuery]long afterID,[FromQuery]int limit) 
        { 
            return await _userClient.GetUserPaginateAsync(new UserService.GetUserPaginateRequest { AfterID= afterID, Limit = limit });
        }

        [HttpGet("total")]
        public async Task<UserService.GetNumOfUserReply> GetNumOfUser()
        {
            return await _userClient.GetNumOfUserAsync(new UserService.GetNumOfUserRequest { Message = ""});
        }

        [HttpGet("search")]
        public async Task<UserService.GetUserByIdReply> GetUserById([FromQuery]long id)
        {
            return await _userClient.GetUserByIdAsync(new UserService.GetUserByIdRequest { Id = id});
        }

        [HttpGet("auth")]
        public async Task<UserService.GetUserByEmailReply> GetUserByEmail([FromQuery]string email)
        {
            return await _userClient.GetUserByEmailAsync(new UserService.GetUserByEmailRequest { Email = email });
        }

        [HttpPut("update")]
        public async Task<UserService.UpdateUserReply> UpdateUser(UserService.UpdateUserRequest updateUserRequest)
        {
            var result = await _userClient.UpdateUserAsync(updateUserRequest);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<UserService.DeleteUserReply> DeleteUser([FromQuery] long id)
        {
            var result = await _userClient.DeleteUserAsync(new UserService.DeleteUserRequest { Id = id });
            return result;
        }

    }
}

