using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Repository
{
    public interface IDatabaseconnectionProvider
    {
        void ConnectionOpen();
         void SaveProject();
    }
}
