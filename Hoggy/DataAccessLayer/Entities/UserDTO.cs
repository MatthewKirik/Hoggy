﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
