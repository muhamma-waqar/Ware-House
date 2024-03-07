using Infrastructure.Authentication.Core.Model;
using Infrastructure.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<(MySingInResult result, SingInData? Data)> SingIn(string username, string password)
        {
            var user = await _userManager.FindByIdAsync(username);

            if (user == null)
            {
                return (MySingInResult.Failed, null);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return (MySingInResult.LockedOut, null);
                if (result.IsNotAllowed)
                    return (MySingInResult.NotAllowed, null);
                throw new Exception("Unhandled sign-in outcome.");
            }

            var token = _tokenService.CreateAuthenticationToken(user.Id, username);

            return (MySingInResult.Success,
                data: new SingInData()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = token
                });
        }
    }
}
