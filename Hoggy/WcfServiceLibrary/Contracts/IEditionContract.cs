﻿using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IEditionContract
    {
        [OperationContract]
        bool EditCard(AuthenticationToken token, CardDTO card);
        [OperationContract]
        bool EditColumn(AuthenticationToken token, ColumnDTO column);
        [OperationContract]
        bool EditUserProfile(AuthenticationToken token, UserProfileDTO userProfile);
    }
}
