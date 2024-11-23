﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth.Models
{
	public class UserDto
	{
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
