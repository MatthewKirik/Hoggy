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
        [MessageBodyMember(Order = 1)]
        public int Length;
        [MessageBodyMember(Order = 2)]
        public System.IO.Stream FileByteStream;
    }
}
