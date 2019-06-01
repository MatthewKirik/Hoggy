using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Helpers
{
    class DataExchange
    {
        public static UserModel GetUser()
        {
            return new UserModel();
        }

    }
}
