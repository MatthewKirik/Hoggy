using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;

namespace WcfServiceLibrary.Services
{
    public class EditionService : IEditionContract
    {
        public void EditCard(AuthenticationToken token, CardDTO card)
        {
            throw new NotImplementedException();
        }
    }
}
