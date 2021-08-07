using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;
using AdekaDestek.HelpDeskAPI.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AdekaDestek.HelpDeskAPI.Controllers
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

        [HttpPost("updatepasswordforadeka")]
        public async Task<IActionResult> UpdatePasswordForAdeka(UserUpdatePasswordForAdekaDto userUpdatePasswordForAdekaDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdatePasswordforHelpDeskAsync(userUpdatePasswordForAdekaDto.Password, userUpdatePasswordForAdekaDto.Email, userUpdatePasswordForAdekaDto.ModifiedByName);
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

        [HttpPost("deleteuserforadeka")]
        public async Task<IActionResult> DeleteUserForAdeka(UserDeleteDto userDelete)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.DeleteUserforHelpDeskAsync(userDelete.Email);
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

        [HttpPost("updateuserforadeka")]
        public async Task<IActionResult> UpdateUserForAdeka(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserforHelpDeskAsync(userUpdateDto);
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
