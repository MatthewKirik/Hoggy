﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class UserProfileDTO
    {
        public byte[] Image { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}
