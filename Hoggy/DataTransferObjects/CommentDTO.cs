using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class CommentDTO
    {
        public string Text { get; set; }
        public UserDTO Author { get; set; }
        public CardDTO Card { get; set; }
    }
}
