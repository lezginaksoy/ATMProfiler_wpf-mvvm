using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Repository
{
    public class TransactionScreenRepository : IDisposable
    {
        DatabaseProviderService provider = new DatabaseProviderService();
        DatabaseConnectionService db;

        public TransactionScreenRepository()
        {
            db = new DatabaseConnectionService();
            db.ConnectionOpen();
        }


        public ArrayList GetScreenData(string ScreenId)
        {
            string SqlcmdString = "select screen_data from OC_ATM.ATM_SCREEN_DATA where status='1' and screen_id='{0}'";
            SqlcmdString = string.Format(SqlcmdString,ScreenId);
            ArrayList StateList = provider.ExecuteCommand(SqlcmdString);
            return StateList;
        }





        #region [ Dispose ]

        public void Dispose()
        {

        }

        #endregion


    }

}
