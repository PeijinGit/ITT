using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace API.Controllers
{
    [EnableCors("any")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        IUserBLL userBLL;

        public UserController(IUserBLL userBLL)
        {
            this.userBLL = userBLL;
        }


        [HttpPost]
        public Models.LoginResponse ValidateLogin(Models.User user) 
        {        
            int userId = userBLL.ValidateLogin(user.Username, user.Password);
            return UserIdCheck(userId);
        }

        /// <summary>
        /// Google and Facebook loigin
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public Models.LoginResponse ThirdPartyLogin(Models.User user) 
        {
            int userId = userBLL.ThirdPartyLogin(user.Username);
            return UserIdCheck(userId);
        }

        [HttpPost]
        public string UserRegister(Models.User user)
        {
            int result = userBLL.UserRegister(user.Username, user.Password);
            return RegisterResult(result);
        }

        /// <summary>
        /// Validate register result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string RegisterResult(int result)
        {
            if (result == -1)
            {
                HttpContext.Response.StatusCode = 208;
                return "Register Fail";
            }
            else
            {
                HttpContext.Response.StatusCode = 200;
                return "Register Success";
            }
        }


        /// <summary>
        /// Validate user id then return user entity
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private Models.LoginResponse UserIdCheck(int userId) 
        {
            if (userId == -1)
            {
                HttpContext.Response.StatusCode = 401;
                return null;
            }
            else
            {
                var token = TokenHelper.CreateToken(userId);
                HttpContext.Response.StatusCode = 200;
                return new Models.LoginResponse { Id = userId, Token= token };
            }
        }
    }
}