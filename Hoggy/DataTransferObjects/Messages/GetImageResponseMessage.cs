using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Messages
{
    [MessageContract]
    public class GetImageResponseMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public long Length;
        [MessageBodyMember(Order = 2)]
        public System.IO.Stream FileByteStream;
    }
}
