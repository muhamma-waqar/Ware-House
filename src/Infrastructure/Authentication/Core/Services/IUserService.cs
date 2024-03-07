using Infrastructure.Authentication.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Core.Services
{
    public interface IUserService
    {
        Task<(MySingInResult result, SingInData? Data)> SingIn(string username, string password);
    }
}
