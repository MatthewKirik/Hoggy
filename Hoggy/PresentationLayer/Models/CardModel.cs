using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class CardModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<TagModel> Tags { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<UserModel> Participants { get; set; }
    }
}
