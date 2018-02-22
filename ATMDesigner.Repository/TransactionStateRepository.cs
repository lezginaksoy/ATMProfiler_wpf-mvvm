using ATMDesigner.Common;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Repository
{
    public class TransactionStateRepository : IDisposable
    {
        DatabaseProviderService provider = new DatabaseProviderService();
        DatabaseConnectionService db;
        public TransactionStateRepository()
        {
            db = new DatabaseConnectionService();
            db.ConnectionOpen();
        }


        public int SaveProject(List<string> InsertSqlStringList, List<string> StateIdListFromProjectUpload)
        {
            List<string> DeleteSqlStringList = new List<string>();
           // OracleTransaction trans = db.OraConnection.BeginTransaction();
            OracleTransaction trans = provider.Runner.OraConnection.BeginTransaction();
            int Retval = 102;
            try
            {

                for (int i = 0; i < StateIdListFromProjectUpload.Count; i++)
                {
                    string SqlcmdString = "Delete OC_ATM.ATM_STATE_DATA_DESGNR where Status=1 and state_id='{0}'";
                    SqlcmdString = string.Format(SqlcmdString, StateIdListFromProjectUpload[i]);
                    DeleteSqlStringList.Add(SqlcmdString);
                }

                if (DeleteSqlStringList.Count > 0)
                {
                    Retval = 99;
                    if (provider.ExecuteCommand(DeleteSqlStringList))                    
                    {
                        Retval = 101;
                        if (provider.ExecuteCommand(InsertSqlStringList))
                        {
                            Retval = 100;
                            trans.Commit();
                        }
                        else
                        {
                            Retval = 103;
                            trans.Rollback();
                        }
                    }
                    else
                    {
                        Retval = 104;
                        trans.Rollback();
                    }
                }
                else
                {
                    Retval = 101;
                    if (provider.ExecuteCommand(InsertSqlStringList))
                    {
                        Retval = 100;
                        trans.Commit();
                    }
                    else
                    {
                        Retval = 105;
                        trans.Rollback();       
                    }
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                Retval = 102;
            }

            return Retval;
        }

        public int Synchronous_StateTables(string ProjectName)
        {
            List<string> DeleteSqlStringList = new List<string>();
            List<string> InsertSqlStringList = new List<string>();
            string InsertSql = @"INSERT INTO OC_ATM.ATM_STATE_DATA1(GUID,STATUS,LASTUPDATED,STATE_ID,STATE_DSCR,STATE_TYPE,PRM1,PRM2,PRM3,PRM4,PRM5,PRM6,PRM7,PRM8,CONFIG_ID,BRAND_ID,CONFIG_VERSION)
                            VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6:000}','{7:000}','{8:000}','{9:000}','{10:000}','{11:000}','{12:000}','{13:000}','{14}','{15}','{16}')";

            string DeletecmdString = "Delete OC_ATM.ATM_STATE_DATA1 where Status=1 and state_id='{0}'";

            OracleTransaction trans = provider.Runner.OraConnection.BeginTransaction();
            int Retval = 102;
            try
            {
                ArrayList StateList = new ArrayList();
                StateList = GetStateList(ProjectName);
                if (StateList.Count == 0)
                    return 103;

                foreach (object[] processRow in StateList)
                {
                    //string SqlcmdString = "Delete OC_ATM.ATM_STATE_DATA1 where Status=1 and state_id='{0}'";
                    if (processRow[5].ToString() == "P")
                        continue;

                    string TmpDeletecmdString = string.Format(DeletecmdString, processRow[3]);
                    DeleteSqlStringList.Add(TmpDeletecmdString);

                    string TmpInsertSql = string.Format(InsertSql, processRow[0], processRow[1], processRow[2], processRow[3], processRow[4], processRow[5],
                        processRow[8], processRow[9], processRow[10], processRow[11], processRow[12], processRow[13], processRow[14], processRow[15],
                        processRow[16], processRow[17], processRow[18]);
                    InsertSqlStringList.Add(TmpInsertSql);
                }

                if (DeleteSqlStringList.Count > 0 && InsertSqlStringList.Count > 0 && DeleteSqlStringList.Count == InsertSqlStringList.Count)
                {
                    Retval = 99;
                    if (provider.ExecuteCommand(DeleteSqlStringList))
                    {
                        Retval = 101;
                        if (provider.ExecuteCommand(InsertSqlStringList))
                        {
                            Retval = 100;
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                        }
                    }
                    else
                    {
                        trans.Rollback();
                    }

                }
                else
                {
                    Retval = 104;
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                Retval = 102;
            }

            return Retval;

        }

        public ArrayList GetNextSequence()
        {
            string SqlcmdString = "select  NVL(max(state_id),'-1') StateId from OC_ATM.ATM_STATE_DATA_DESGNR where status=1";
            ArrayList MaxStateId = provider.ExecuteCommand(SqlcmdString);
            return MaxStateId;
        }
        
        public ArrayList AtmConfigList()
        {
            string SqlcmdString = "select code,SUBSTR(name,5,INSTR(name, ';;')-5) name from OC_ATM.ATM_GROUP_CONFIG where status=1";
            ArrayList ConfigList =provider.ExecuteCommand(SqlcmdString);            
            return ConfigList;
        }

        public ArrayList AtmBrandList()
        {
            string SqlcmdString = "Select id,brand from OC_ATM.ATM_BRAND_DEF where Status=1";
            ArrayList BrandList = provider.ExecuteCommand(SqlcmdString);
            return BrandList;
        }

        public ArrayList AtmProjectList()
        {
            string SqlcmdString = "Select distinct project_name from OC_ATM.ATM_STATE_DATA_DESGNR where Status=1";
            ArrayList ProjectList = provider.ExecuteCommand(SqlcmdString);
            return ProjectList;
        }

        public ArrayList AtmTransactionList(string ProjectName)
        {
            string SqlcmdString = "Select   distinct trans_name from OC_ATM.ATM_STATE_DATA_DESGNR where Status=1 and project_name='{0}'";
            SqlcmdString = string.Format(SqlcmdString, ProjectName);
            ArrayList TransactionList = provider.ExecuteCommand(SqlcmdString);
            return TransactionList;
        }

        public ArrayList GetStateList(string ProjectName)
        {
            string SqlcmdString = "Select * from OC_ATM.ATM_STATE_DATA_DESGNR where Status=1 and project_name='{0}' order by  state_id";
            SqlcmdString = string.Format(SqlcmdString, ProjectName);
            ArrayList StateList = provider.ExecuteCommand(SqlcmdString);
            return StateList;
        }

        public ArrayList GetStateIdList()
        {
            string SqlcmdString = "Select state_id from OC_ATM.ATM_STATE_DATA_DESGNR where Status=1  order by  state_id";
            SqlcmdString = string.Format(SqlcmdString);
            ArrayList StateIdList = provider.ExecuteCommand(SqlcmdString);
            return StateIdList;
        }

        #region [ Dispose ]

        public void Dispose()
        {
        
        }

        #endregion


      
    }
}
