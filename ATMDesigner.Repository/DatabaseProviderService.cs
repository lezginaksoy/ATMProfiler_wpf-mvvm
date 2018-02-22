using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Repository
{
    public class DatabaseProviderService
    {
        public DatabaseConnectionService Runner = new DatabaseConnectionService();
        public DatabaseProviderService()
        {
            if (!Runner.IsConnectionOpen())
                Runner.ConnectionOpen();
        }

        public bool ExecuteCommand(List<string> lSqlString)
        {
            bool RetVal = true;         
            List<OracleCommand> sqlCommands = new List<OracleCommand>();
            if (!Runner.IsConnectionOpen())
                RetVal=Runner.ConnectionOpen();
            if (RetVal)
            {
                //OracleTransaction trans = Runner.OraConnection.BeginTransaction();
                try
                {
                    for (int i = 0; i < lSqlString.Count; i++)
                    {
                        OracleCommand myCommand = new OracleCommand(lSqlString[i], Runner.OraConnection);
                        sqlCommands.Add(myCommand);
                    }

                    for (int i = 0; i < sqlCommands.Count; i++)
                    {
                        ((OracleCommand)sqlCommands[i]).ExecuteNonQuery();

                    }
                    //trans.Commit();
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    RetVal = false;
                }
                finally
                {
                    for (int i = 0; i < sqlCommands.Count; i++)
                    {
                        ((OracleCommand)sqlCommands[i]).Dispose();
                    }
                }
            }

            return RetVal;
        }


        public ArrayList ExecuteCommand(string sqlString)
        {
            ArrayList ValueList = new ArrayList();
            DatabaseConnectionService Runner = new DatabaseConnectionService();
            bool Retval=false;
            if (!Runner.IsConnectionOpen())
               Retval=Runner.ConnectionOpen();
            if (Retval)
            {
                OracleCommand myCommand = null; ;
                OracleDataReader myReader = null;
                try
                {
                    myCommand = new OracleCommand(sqlString, Runner.OraConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        object[] values = new object[myReader.FieldCount];
                        myReader.GetValues(values);
                        ValueList.Add(values);
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    myReader.Close();
                    myReader.Dispose();
                    myCommand.Dispose();
                }
            }

            return ValueList;
        }
             


        //public string Connection = "";
        //DatabaseConnectionService conDB = new DatabaseConnectionService();
       

        //public enum SorguTip { StorProcedure = 1, Text = 2 };
        //public bool Hata { get; set; }
        //public string HataMesaj { get; set; }

        //public Dictionary<string, object> Param = null;

        //public void FillParams(string ParamName, object Value)
        //{
        //    try
        //    {
        //        if (Param == null)
        //        {
        //            Param = new Dictionary<string, object>();
        //        }
        //        Param.Add(ParamName, Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        Hata = true;
        //        HataMesaj = ex.Message;
        //    }
        //}

        //public DataSet GetDataSet(string SPName, SorguTip sor)
        //{
        //    try
        //    {
        //        OracleConnection con = conDB.OraCon();
        //        OracleCommand com = new OracleCommand();
        //        DataSet ds = new DataSet();
        //        com.Connection = con;
        //        switch (sor)
        //        {
        //            case SorguTip.StorProcedure:
        //                com.CommandType = CommandType.StoredProcedure;
        //                break;
        //            case SorguTip.Text:
        //                com.CommandType = CommandType.Text;
        //                break;
        //        }

        //        com.CommandText = SPName;

        //        if (Param != null)
        //        {
        //            foreach (var i in Param)
        //            {
        //                com.Parameters.AddWithValue(i.Key, i.Value);
        //            }
        //        }
        //        conDB.ConnectionOpen();
        //        SqlDataAdapter dap = new SqlDataAdapter(com);
        //        dap.Fill(ds);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionDal.DalHandle(ex);
        //        return null;

        //    }
        //    finally
        //    {
        //        conDB.ConnectionClose();
        //    }


        //}


        //public SqlDataReader GetDataReader(string SPName, SorguTip sor)
        //{
        //    try
        //    {
        //        SqlConnection con = conDB.SqlCon();
        //        SqlCommand com = new SqlCommand();
        //        SqlDataReader dr;
        //        com.Connection = con;
        //        switch (sor)
        //        {
        //            case SorguTip.StorProcedure:
        //                com.CommandType = CommandType.StoredProcedure;
        //                break;
        //            case SorguTip.Text:
        //                com.CommandType = CommandType.Text;
        //                break;
        //        }

        //        com.CommandText = SPName;

        //        if (Param != null)
        //        {
        //            foreach (var i in Param)
        //            {
        //                com.Parameters.AddWithValue(i.Key, i.Value);
        //            }
        //        }
        //        conDB.ConnectionOpen();
        //        dr = com.ExecuteReader();
        //        return dr;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionDal.DalHandle(ex);
        //        return null;

        //    }
        //    finally
        //    {
        //        conDB.ConnectionClose();
        //    }


        //}


        //public bool Execute(string SPName, SorguTip sor)
        //{
        //    try
        //    {
        //        SqlConnection con = conDB.SqlCon();
        //        SqlCommand com = new SqlCommand();
        //        com.Connection = con;
        //        switch (sor)
        //        {
        //            case SorguTip.StorProcedure:
        //                com.CommandType = CommandType.StoredProcedure;
        //                break;
        //            case SorguTip.Text:
        //                com.CommandType = CommandType.Text;
        //                break;
        //        }

        //        com.CommandText = SPName;
        //        if (Param != null)
        //        {
        //            foreach (var i in Param)
        //            {
        //                com.Parameters.AddWithValue(i.Key, i.Value);
        //            }
        //        }
        //        conDB.ConnectionOpen();
        //        int donen = com.ExecuteNonQuery();
        //        return donen == 0 ? false : true;

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionDal.DalHandle(ex);
        //        return false;
        //    }
        //    finally
        //    {
        //        conDB.ConnectionClose();
        //    }

        //}

        //public string Scalar(string SPName, SorguTip sor)
        //{
        //    try
        //    {
        //        SqlConnection con = conDB.SqlCon();
        //        SqlCommand com = new SqlCommand();
        //        com.Connection = con;
        //        switch (sor)
        //        {
        //            case SorguTip.StorProcedure:
        //                com.CommandType = CommandType.StoredProcedure;
        //                break;
        //            case SorguTip.Text:
        //                com.CommandType = CommandType.Text;
        //                break;
        //        }

        //        com.CommandText = SPName;
        //        if (Param != null)
        //        {
        //            foreach (var i in Param)
        //            {
        //                com.Parameters.AddWithValue(i.Key, i.Value);
        //            }
        //        }
        //        conDB.ConnectionOpen();
        //        object obj = com.ExecuteScalar();
        //        return obj == null ? "0" : obj.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionDal.DalHandle(ex);
        //        return "";
        //    }
        //    finally
        //    {
        //        conDB.ConnectionClose();
        //    }

        //}


    }

}
