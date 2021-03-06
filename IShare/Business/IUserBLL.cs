using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IUserBLL
    {
         int ValidateLogin(string username, string pwd);
        int ThirdPartyLogin(string username);
        int UserRegister(string username, string password);
    }
}
