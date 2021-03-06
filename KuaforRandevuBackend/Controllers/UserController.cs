﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KuaforRandevuBackend.Context;
using KuaforRandevuBackend.Models;
using KuaforRandevuBackend.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KuaforRandevuBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private ApplicationSettings _appSettings;
        private ProjectContext _context;
        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<ApplicationSettings> appSettings, ProjectContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }
        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> PostAppUser(AppUserModel model)
        {

            var appUser = new AppUser()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = model.Username,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                StoreAddress = model.StoreAddress,
                StoreName = model.StoreName,
                StorePhoneNumber = model.StorePhoneNumber,
                StoreType = model.StoreType,




            };
            try
            {
                var result = await _userManager.CreateAsync(appUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("Login")]
        //POST /api/User/Login
        public async Task<IActionResult> Login(LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                        Expires = DateTime.UtcNow.AddDays(365),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                    
                }
                else
                {
                    return BadRequest(new { message = "Kullanıcı adı veya şifre yanlış. Lütfen kontrol ediniz" });
                }
                

            }
            else
            {
                
                return BadRequest(new { message = "Böyle bir kullanıcı bulunmamaktadır. Lütfen üye olunuz." });

            }
        }
        [HttpPost]
        [Authorize]
        [Route("LoginMore")]
        public async Task<IActionResult> LoginMore(LoginModel model)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userLogged = await _userManager.FindByIdAsync(userId);
            try
            {
                
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserID",user.Id.ToString())
                        }),
                            Expires = DateTime.UtcNow.AddDays(365),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);
                        var loggedUser = new LoggedUsers
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Name = user.Name,
                            Surname = user.Surname,
                            Username = user.UserName,
                            User = userLogged,
                            UserId = userLogged.Id
                        };
                        if (userLogged.Id != user.Id)
                        {
                            var isUserLogged = _context.LoggedUsers.Where(x => x.Id == user.Id).FirstOrDefault();
                            if (isUserLogged != null)
                            {
                                return BadRequest(new { message = "Bu hesapla zaten giriş yapmışsınız." });
                            }
                            else
                            {
                                
                                    await _context.LoggedUsers.AddAsync(loggedUser);
                                    await _context.SaveChangesAsync();
                                    return Ok(new { token });
                                
                               
                                

                            }
                            
                        }
                        else
                        {
                            return  BadRequest(new { message = "Bu hesapla zaten giriş yapmışsınız." });
                        }

                        

                    }
                    else
                    {
                        return BadRequest(new { message = "Kullanıcı adı veya şifre yanlış. Lütfen kontrol ediniz" });
                    }


                }
                else
                {

                    return BadRequest(new { message = "Böyle bir kullanıcı bulunmamaktadır. Lütfen üye olunuz." });

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        [HttpPost]
        [Route("LoginSelect")]
        //POST /api/User/Login
        public async Task<IActionResult> LoginSelect(LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                        Expires = DateTime.UtcNow.AddDays(365),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });

                
               


            }
            else
            {

                return BadRequest(new { message = "Böyle bir kullanıcı bulunmamaktadır. Lütfen üye olunuz." });

            }
        }
        [HttpGet]
        [Authorize]
        [Route("GetUserProfile")]

        public async Task<Object> GetUserProfile()
        {

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Email,
                user.Name,
                user.Surname,
                user.UserName,
                
            };
        }

        [HttpGet]
        [Authorize]
        [Route("GetLoggedUsers")]

        public async Task<Object> GetLoggedUsers()
        {

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var loggedUsers = _context.LoggedUsers.Where(x => x.UserId == userId).ToList();
            return loggedUsers;
        }

    }
}
