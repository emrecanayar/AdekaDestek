using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using AdekaDestek.Entities.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdekaDestek.IdentityAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager) : base(userManager, mapper)
        {
            _signInManager = signInManager;
        }
        [HttpPost("adduser")]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<User>(userAddDto);
                user.ModifiedByName = $"{user.FirstName} {user.LastName}";
                user.ModifiedDate = DateTime.Now;
                user.CreatedByName = $"{user.FirstName} {user.LastName}";
                user.CreatedDate = DateTime.Now;
                user.TwoFactorType = 0;
                var result = await UserManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    return StatusCode(200);
                }
                else
                {

                    return StatusCode(400);

                }

            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost("updateuser")]
        public async Task<IActionResult> Update(UserUpdateForApiDto userUpdateForApiDto)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await UserManager.FindByEmailAsync(userUpdateForApiDto.Email);
                var updatedUser = Mapper.Map<UserUpdateForApiDto, User>(userUpdateForApiDto, oldUser);
                updatedUser.ModifiedByName = $"{oldUser.FirstName} {oldUser.LastName}";
                updatedUser.ModifiedDate = DateTime.Now;
                var result = await UserManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    return StatusCode(200);

                }
                else
                {
                    return StatusCode(400);
                }

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("deleteuser")]
        public async Task<IActionResult> Delete(UserDeleteDto deleteUserDto)
        {
            var user = await UserManager.FindByEmailAsync(deleteUserDto.Email);
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(400);
            }

        }
    }
}
