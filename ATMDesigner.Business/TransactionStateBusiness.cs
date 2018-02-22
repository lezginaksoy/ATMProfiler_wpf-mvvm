using ATMDesigner.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMDesigner.Common;
using ATMDesigner.Repository;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Reflection;
using System.Collections;

namespace ATMDesigner.Business
{
    public class TransactionStateBusiness : ITransactionStateBusiness
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int SaveProject(List<ModelCanvasStateObject> StateObjList, string ProjectName, string StrExtensionStateNumber, List<string> StateIdListFromProjectUpload)
        {
            int RetVal = 102;           
            List<string> cmdStringList = new List<string>();           
            try
            {
                log.Info("SaveProject() Start.!");
                using (TransactionStateRepository db = new TransactionStateRepository())
                {
                    int ExtensionStateNumber = 0;

                    RetVal = 99;
                    if (int.TryParse(StrExtensionStateNumber, out ExtensionStateNumber))
                    {
                        RetVal = 101;
                        foreach (var item in StateObjList)
                        {
                            List<string> cmdString = new List<string>();                         
                            Type ClassType = item.PropertyGrid.SelectedObject.GetType();
                            Object ClassInstance = Activator.CreateInstance(ClassType);
                            object[] parametersArray = new object[] { item.PropertyGrid, ProjectName, item.TransactionName, ExtensionStateNumber };
                            object CommandString = ClassType.InvokeMember("CreateInsertCommandScript", BindingFlags.InvokeMethod, null, ClassInstance, parametersArray);
                            ExtensionStateNumber++;                            
                            cmdString = (List<string>)CommandString;
                            cmdStringList.AddRange(cmdString);                           
                        }
                        RetVal=db.SaveProject(cmdStringList, StateIdListFromProjectUpload);                       
                    }                  
                }
            }
            catch (Exception ex)
            {
                log.Error("SaveProject() Hata " + ex.Message);
                RetVal = 102;
            }

            return RetVal;
        }

        public List<ModelAtmConfig> AtmConfigList()
        {           
            List<ModelAtmConfig> Configatmlist = new List<ModelAtmConfig>();
            using (TransactionStateRepository db = new TransactionStateRepository())
            {
                ArrayList ConfigList = db.AtmConfigList();
                foreach (object[] processRow in ConfigList)
                {
                    Configatmlist.Add(new ModelAtmConfig(processRow[0].ToString(), processRow[1].ToString()));
                }

            }

            return Configatmlist;
        }

        public List<ModelAtmBrand> AtmBrandList()
        {
            List<ModelAtmBrand> brandatmlist = new List<ModelAtmBrand>();
            using (TransactionStateRepository db = new TransactionStateRepository())
            {
                ArrayList BrandList = db.AtmBrandList();
                foreach (object[] processRow in BrandList)
                {
                    brandatmlist.Add(new ModelAtmBrand(processRow[0].ToString(), processRow[1].ToString()));
                }

            }

            return brandatmlist;
        }

        public List<string> AvaliableStateNumberList()
        {
            List<string> statenumberlist = new List<string>();
            using (TransactionStateRepository db = new TransactionStateRepository())
            {
                 ArrayList StateIdlist = db.GetStateIdList();

                for (int i =0; i < 1000; i++)
                {
                    statenumberlist.Add(i.ToString());
                }                
                statenumberlist.Remove("255");
                foreach (object[] processRow in StateIdlist)
                {
                    string NotAvaliableId = processRow[0].ToString();
                    statenumberlist.Remove(NotAvaliableId);                  
                }
                
                //foreach (object[] processRow in Statelist)
                //{

                //    string strmaxid = processRow[0].ToString();
                //    int maxid = 0;

                //    if (int.TryParse(strmaxid, out maxid))
                //    {
                //        for (int i = maxid + 1; i < 999; i++)
                //        {
                //            statenumberlist.Add(i.ToString());
                //        }
                        
                //        if (maxid + 1 <= 255)
                //            statenumberlist.RemoveAt(255 - (maxid + 1));

                //        return statenumberlist;
                //    }
                //    else
                //    {
                //        return statenumberlist = new List<string>();
                //    }
                //}

            }

            return statenumberlist;
        }
        
        public List<string> AtmProjectList()
        {
            List<string> atmprojectlist = new List<string>();
            using (TransactionStateRepository db = new TransactionStateRepository())
            {
                ArrayList ProjectList = db.AtmProjectList();
                foreach (object[] processRow in ProjectList)
                {
                    atmprojectlist.Add(processRow[0].ToString());
                }
            }

            return atmprojectlist;
        }

        public List<string> AtmTransactionList(string ProjectName)
        {
            List<string> atmtransactionlist = new List<string>();
            using (TransactionStateRepository db = new TransactionStateRepository())
            {
                ArrayList TransList = db.AtmTransactionList(ProjectName);
                foreach (object[] processRow in TransList)
                {
                    atmtransactionlist.Add(processRow[0].ToString());
                }
            }

            return atmtransactionlist;
        }
        
        public ArrayList GetStateList(string ProjectName)
        {
            ArrayList StateList = new ArrayList();
            using (TransactionStateRepository db = new TransactionStateRepository())
            {
                 StateList = db.GetStateList(ProjectName);            
            }
            return StateList;
        }
        
        public int Synchronous_StateTables(string ProjectName)
        {
            int RetVal = 102;
            try
            {
                log.Info("Synchronous_StateTables() Start.!");
                using (TransactionStateRepository db = new TransactionStateRepository())
                {
                    RetVal = 99;
                    RetVal = db.Synchronous_StateTables(ProjectName);
                }
                
            }
            catch (Exception ex)
            {
                log.Error("Synchronous_StateTables() Hata " + ex.Message);
                RetVal = 102;
            }

            return RetVal;
        

        }


    }

}
