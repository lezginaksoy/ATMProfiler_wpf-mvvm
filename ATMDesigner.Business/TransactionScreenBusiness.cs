using ATMDesigner.Business.Interfaces;
using ATMDesigner.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Business
{
   public class TransactionScreenBusiness : ITransactionScreenBusiness
    {
       private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int SaveScreen()
        {
            throw new NotImplementedException();
        }

        public string GetScreenData(string ScreenId)
        {
            string ScreenData="";
            try
            {


                using (TransactionScreenRepository db = new TransactionScreenRepository())
                {
                    ArrayList ScreenDataList = db.GetScreenData(ScreenId);
                    foreach (object[] processRow in ScreenDataList)
                    {
                        ScreenData = processRow[0].ToString();
                        //[80m[B0mPEC:\Backgrounds\324.bmp\HK
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return ScreenData;           
        }



    }
}
