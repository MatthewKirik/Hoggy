using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Messages
{
    [MessageContract]
    public class GetImageRequestMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public AuthenticationToken Token;
        [MessageHeader(MustUnderstand = true)]
        public int UserId;
    }
}
