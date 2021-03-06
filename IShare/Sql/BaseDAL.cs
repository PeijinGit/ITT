using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BaseDAL
    {
        private readonly IOptions<AppSettingModels> _appSettings;
        protected string connectionString;

        public BaseDAL(IOptions<AppSettingModels> appSettings)
        {
            _appSettings = appSettings;
            connectionString = _appSettings.Value.ConStr;
        }
    }
}
