﻿using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.Users;
using LibraryManagementSystem.Domain.Models.Responses;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, 
            IUserService userService, 
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userService = userService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<AppUserResponse> Register(AppUserRegister model)
        {
            var targetUser = await _userService.GetUserByUsername(model.UserName);

            if (targetUser == null)
            {
                return null;
            }

            var userExist = await _userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                return new AppUserResponse()
                {
                    ExpiredOn = null,
                    Token = null,
                    Status = false,
                    Message = "User already exists!"
                };
            }
            AppUser user = new AppUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AppUserResponse()
                {
                    ExpiredOn = null,
                    Token = null,
                    Status = false,
                    Message = "User creation failed! Please check user details and try again."
                };
            }
            var justCreatedUser = await _userManager.FindByNameAsync(model.UserName);
            await _userManager.AddToRoleAsync(user, model.Role);
            targetUser.AppUserId = justCreatedUser.Id;
            await _userRepository.Update(targetUser);

            return new AppUserResponse()
            {
                ExpiredOn = null,
                Token = null,
                Status = true,
                Message = "User created successfully!"
            };
        }

        public async Task<AppUserResponse> Login(AppUserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            var roles = new List<string>();
            if (user != null)
            {
                var getRoles = await _userManager.GetRolesAsync(user);
                roles = getRoles.ToList();
            }
            if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
            
                var accessToken = await _tokenService.IssueAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();
                var tokenCreated = await _tokenService.SaveRefreshToken(user.UserName, refreshToken);
                if(tokenCreated == null)
                {
                    return new AppUserResponse()
                    {
                        Message = "Token still available!"
                    };
                }

                var userData = await _userRepository.GetByAppUserId(user.Id);
                return new AppUserResponse()
                {
                    Token = accessToken.Token,
                    RefreshToken = tokenCreated.Token,
                    RefreshTokenExpiredOn = tokenCreated.ExpiryDate,
                    ExpiredOn = accessToken.ExpiredOn,
                    Status = true,
                    Message = "Login success!",
                    User = userData.ToUserResponse(),
                    Roles = roles
                };            
            }

            return new AppUserResponse()
            {
                Token = null,
                ExpiredOn = null,
                Status = false,
                Message = "Credentials not valid!"
            };
        }
        public async Task<AppUserResponse> Logout(string refreshToken)
        {
            bool IsRevoked = await _tokenService.RevokeRefreshToken(refreshToken);

            if (IsRevoked == true)
            {
                return new AppUserResponse()
                {
                    Message = "Token revoked!"
                };
            }
            return new AppUserResponse()
            {
                Message = "Refresh token not found"
            };
        }

        public async Task<AppUserResponse> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            

            return new AppUserResponse (){ 
                ExpiredOn = null,
                Token = null,
                Status = true, 
                Message = "Role created successfully!"
            };
        }
    }
}
