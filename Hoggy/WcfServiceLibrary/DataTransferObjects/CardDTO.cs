using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.DataTransferObjects
{
    public class CardDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<TagDTO> Tags { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<UserDTO> Participants { get; set; }
    }
}
