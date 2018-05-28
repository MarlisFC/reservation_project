using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RES.DataAccess.Interfaces.Base
{
    public interface IRepositoryBase: IDisposable
    {
        bool CanDispose { get; set; }
        void Dispose(bool force);
    }
}
