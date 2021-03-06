using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUserDAL
    {
        int ValidateLogin(string username, string pwd);
        int ThirdPartyLogin(string username);
        int UserRegister(string username, string password, int usertype);
    }
}
