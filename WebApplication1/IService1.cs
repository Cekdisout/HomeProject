using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.UI;

namespace WebApplication1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "AddMunicipality")]
        string AddMunicipality();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "AddSchedule/{Municipality}/{TaxType}/{Date}")]
        string AddSchedule(string Municipality, string TaxType,string Date);

        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PosVersion/{StoreNo}/{PosNo}/{IP}/{VersionNo}")]
        //string LogPosVersion(string StoreNo, string PosNo, string IP, string VersionNo);

        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AdmVersion/{StoreNo}/{PosNo}/{IP}/{VersionNo}")]
        //string LogAdmVersion(string StoreNo, string PosNo, string IP, string VersionNo);

        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ErecipeWarning/{StoreNo}/{PosNo}/{IP}/{Count0}/{Count1}")]
        //string LogErecipeStatus(string StoreNo, string PosNo, string IP, string Count0, string Count1);

        [OperationContract]        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "msg/{StoreNo}/{PosNo}")]
        string GetMessage(string StoreNo, string PosNo);

    }
}
