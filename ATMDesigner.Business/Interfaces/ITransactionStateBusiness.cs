using ATMDesigner.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Business.Interfaces
{
    public interface ITransactionStateBusiness
    {

        int SaveProject(List<ModelCanvasStateObject> StateObjList, string ProjectName, string ExtensionStateNumber, List<string> StateIdListFromProjectUpload);
        int Synchronous_StateTables(string ProjectName);
        List<ModelAtmConfig> AtmConfigList();
        List<ModelAtmBrand> AtmBrandList();
        List<string> AvaliableStateNumberList();
        List<string> AtmProjectList();
        List<string> AtmTransactionList(string ProjectName);
        ArrayList GetStateList(string ProjectName);
    }
}
