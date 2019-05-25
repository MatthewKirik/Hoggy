using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IDataExchangeContract
    {
        [OperationContract]
        List<BoardDTO> GetBoards(AuthenticationToken token);
    }
}
