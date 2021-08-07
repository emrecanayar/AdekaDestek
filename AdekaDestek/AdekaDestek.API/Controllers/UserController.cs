using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AdekaDestek.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("updatepasswordforinfini")]
        public async Task<IActionResult> UpdatePasswordForInfini(UserUpdatePasswordForInfiniDto userUpdatePasswordForInfiniDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdatePasswordforInfiniAsync(userUpdatePasswordForInfiniDto.Password, userUpdatePasswordForInfiniDto.Username, userUpdatePasswordForInfiniDto.ModifiedByName);
                if (result.ResultStatus == Core.Utilities.Results.ComplexTypes.ResultStatus.Success)
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400);
                }
            }


            return BadRequest();
        }
        [HttpPost("updatepasswordforadeka")]
        public async Task<IActionResult> UpdatePasswordForAdeka(UserUpdatePasswordForAdekaDto userUpdatePasswordForAdekaDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdatePasswordforAdekaAsync(userUpdatePasswordForAdekaDto.Password, userUpdatePasswordForAdekaDto.Email, userUpdatePasswordForAdekaDto.ModifiedByName);
                if (result.ResultStatus == Core.Utilities.Results.ComplexTypes.ResultStatus.Success)
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400);
                }
            }

            return BadRequest();
        }
    }
}
