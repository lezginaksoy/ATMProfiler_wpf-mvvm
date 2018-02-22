using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.Repository
{
    public class DatabaseConnectionService 
    {
        public OracleConnection OraConnection;

         public DatabaseConnectionService()
        {
           //String ConnectionString = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
            string ConnectionString = ConfigurationManager.AppSettings["Constr"].ToString();
            OraConnection = new OracleConnection(ConnectionString);           
        }

         public OracleConnection OraCon()
        {
            String ConnectionString = ConfigurationManager.AppSettings["Constr"].ToString();
            OraConnection = new OracleConnection(ConnectionString);
            return OraConnection;
        }

         public bool IsConnectionOpen()
         {
             if (OraConnection.State == ConnectionState.Open)
             {
                 return true;
             }
             else
             {                
                 return false;
             }
         }

        public bool ConnectionOpen()
        {
            try
            {               
                if (OraConnection.State!= ConnectionState.Open)
                {
                    OraConnection.Open();                   
                }
                return true;
            }
            catch (Exception ex)
            {
                //ExceptionDal.DalHandle(ex);     
                return false;
            }

        }

        public void ConnectionClose()
        {
            try
            {
                if (OraConnection.State != ConnectionState.Open)
                {
                    OraConnection.Close();
                }
            }
            catch (Exception ex)
            {
               // ExceptionDal.DalHandle(ex);                
            }
        }



       
    }


}
