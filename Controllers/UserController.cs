﻿using Bookings.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _service.GetAllUsers();
            if (users.Any())
                return Ok(users);
            return NotFound();
        }




        public IActionResult GetUserById(int? v)
        {
            if (v is null)
                return BadRequest();

            var user = _service.GetUserById(v).AsEnumerable();
            if (!user.Any())
                return NotFound();

            return Ok(user);
        }
    }
}