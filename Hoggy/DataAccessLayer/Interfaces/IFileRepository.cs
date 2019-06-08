using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFileRepository
    {
        string AddFile(byte[] file);
        byte[] GetFile(string key);
    }
}
