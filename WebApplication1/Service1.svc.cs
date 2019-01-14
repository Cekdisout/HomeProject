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

        public string AddMunicipality()
        {
            string body = "";
            string[] MunicipalityArray;

            if (OperationContext.Current.RequestContext.RequestMessage.IsEmpty)
                return "Err body";
            body = Encoding.UTF8.GetString(OperationContext.Current.RequestContext.RequestMessage.GetBody<byte[]>());

            if (body.Length < 4)
                return "Err body";

            MunicipalityArray = body.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connectas"].ConnectionString);
                conn.Open();

                for (int i = 0; i < MunicipalityArray.Count(); i++)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [Municipality] values ('" + MunicipalityArray[i] + "')", conn))
                    {

                        if (cmd.ExecuteNonQuery() < 0)
                            return "Insert failed!";
                    }
                }

            }
            catch (SqlException ex)
            {
                return ex.ToString();
            }

            return MunicipalityArray.Count().ToString() + " records inserted";
        }

        public string AddSchedule(string Municipality, string TaxType, string Date)
        {
            string MunID,TaxID;
            DateTime DateFrom, DateTo;

            if (!DateTime.TryParse(Date, out DateFrom))
            {
                return "Date value invalid";
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
