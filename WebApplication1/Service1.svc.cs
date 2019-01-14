using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public string GetMessage(string StoreNo, string PosNo)
        {
            return "Hello";
        }

        public string LogEvent(string StoreNo, string PosNo, string IP, string ErrType)
        {
            int store, pos;

            string body = "";

            if (!(int.TryParse(StoreNo, out store) && (int.TryParse(PosNo, out pos))))
                return "Err param";

            if (OperationContext.Current.RequestContext.RequestMessage.IsEmpty)
                return "Err body";
            body = Encoding.UTF8.GetString(OperationContext.Current.RequestContext.RequestMessage.GetBody<byte[]>());

            if (body.Length < 4)
                return "Err body";
            string[] MunicipalityArray;

            MunicipalityArray = body.Split('\n');
            return MunicipalityArray.Count().ToString();
    

        }

        public string AddSchedule(string Municipality, string TaxType, string Date)
        {
            string MunID,TaxID;
            DateTime DateFrom, DateTo;

            if (!DateTime.TryParse(Date, out DateFrom))
            {
                return "Date value invalid";
            }

            switch (TaxType)
            {
                case "yearly":
                    DateTo = DateFrom.AddYears(1);
                    break;
                case "monthly":
                    DateTo = DateFrom.AddMonths(1);
                    break;
                case "weekly":
                    DateTo = DateFrom.AddDays(7);
                    break;
                case "daily":
                    DateTo = DateFrom.AddDays(1);
                    break;
                default:
                    return "Tax Type parameter invalid";
            }

          
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connectas"].ConnectionString);
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter konektas = new SqlDataAdapter("Select ID from Municipality where [Municipality] = '" + Municipality + "'", conn);
                konektas.Fill(ds);                    
                konektas.Dispose();
                if (ds.Tables[0].Rows.Count == 0) return "Municipality not defined";
                MunID = ds.Tables[0].Rows[0]["ID"].ToString();

                ds = new DataSet();
                konektas = new SqlDataAdapter("Select ID from TaxesConfig where [TaxTypeString] = '" + TaxType + "'", conn);
                konektas.Fill(ds);
                konektas.Dispose();
                if (ds.Tables[0].Rows.Count == 0) return "TaxesConfig not defined";
                TaxID = ds.Tables[0].Rows[0]["ID"].ToString();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Taxes] values (" + MunID + "," + TaxID + ",'" + Date + "','2018-01-14')", conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
