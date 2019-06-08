using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Messages
{
    [MessageContract]
    public class AddImageRequestMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public AuthenticationToken Token;
        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;
    }
}
