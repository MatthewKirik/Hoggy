using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class CommentModel
    {
        public string Text { get; set; }
        public UserModel Author { get; set; }
        public CardModel Card { get; set; }
    }
}
