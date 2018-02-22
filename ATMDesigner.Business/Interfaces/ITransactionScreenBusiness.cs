using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Business.Interfaces
{
    public interface ITransactionScreenBusiness
    {

        int SaveScreen();
        string GetScreenData(string ScreenId);      
       
    }
}
