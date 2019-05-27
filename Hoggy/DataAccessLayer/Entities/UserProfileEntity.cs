﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserProfileEntity
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BoardEntity> Boards { get; set; }
        public virtual ICollection<BoardEntity> ParticipatedBoards { get; set; }
        public virtual ICollection<CardEntity> SubscriptedCards { get; set; }
    }
}
