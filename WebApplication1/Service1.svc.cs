using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.Data.SqlClient;

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
          
            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connectas"].ConnectionString);
                conn.Open();
                {
                   
                    using (SqlCommand cmd = new SqlCommand("Select ID from Municipality where [Municipality] = '"+ Municipality + "'", conn))
                    {
                                               
                        cmd.ExecuteNonQuery();
                        MunID = cmd.ExecuteScalar().ToString();

                    }

                    using (SqlCommand cmd = new SqlCommand("Select ID from TaxesConfig where [TaxTypeString] = '" + TaxType + "'", conn))
                    {

                        cmd.ExecuteNonQuery();
                        TaxID = cmd.ExecuteScalar().ToString();

                    }

                    //INSERTAS EINA CIA

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [Taxes] values ("+ MunID +"," + TaxID + ",'" + Date + "','2018-01-14')", conn))
                    {

                        cmd.ExecuteNonQuery();
                       
                    }

                }
            }
            catch (SqlException ex)
            {
                return ex.ToString();
                //Log exception
                //Display Error message
            }
            return "OK";
        }


    }
}
