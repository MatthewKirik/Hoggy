﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IAuthenticationContract
    {
        [OperationContract]
        bool CheckUserIsRegistered(UserDTO user);
        [OperationContract]
        bool CheckPasswordIsCorrect(UserDTO user);
        [OperationContract]
        UserDTO Login();
    }
}