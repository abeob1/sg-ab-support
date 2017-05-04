using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace WebService
{
    /// <summary>
    /// Summary description for Support
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Support : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region Declarations
        public string sErrDesc = string.Empty;

        public Int16 p_iDebugMode = DEBUG_ON;

        public const Int16 RTN_SUCCESS = 1;
        public const Int16 RTN_ERROR = 0;
        public const Int16 DEBUG_ON = 1;
        public const Int16 DEBUG_OFF = 0;

        clsLog oLog = new clsLog();

        JavaScriptSerializer js = new JavaScriptSerializer();

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        #endregion

        #region WebMethods
        #region Login
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void LogIn(string sJasonInput)
        {
            string sFuncName = "LogIn()";
            string sErrDesc = string.Empty;
            DataSet Ds = new DataSet();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sUserName = string.Empty;
                string sPassword = string.Empty;

                sJasonInput = "[" + sJasonInput + "]";
                List<Jason_ValidateUsers> lstDeserialize = js.Deserialize<List<Jason_ValidateUsers>>(sJasonInput);
                if (lstDeserialize.Count > 0)
                {
                    Jason_ValidateUsers oUserInfo = lstDeserialize[0];
                    sUserName = oUserInfo.sUsername;
                    sPassword = oUserInfo.sPassword;
                }
                sSQL = "SELECT * FROM users where email = @UserName and password = @Password and active=1";
                Ds = Functions.ExecuteDataSet(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@UserName", sUserName), Data.CreateParameter("@Password", sPassword));
                if (Ds.Tables.Count > 0 && Ds != null)
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                    Context.Response.Output.Write(js.Serialize(Ds));
                }
                else
                {
                    sErrDesc = "Invalid UserName or Password";
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile(sErrDesc, sFuncName);
                    throw new Exception(sErrDesc);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Classes

        class Jason_ValidateUsers
        {
            public string sUsername { get; set; }
            public string sPassword { get; set; }
        }
        class UserInfo
        {
            public string sUserName { get; set; }
            public string sFirstName { get; set; }
            public string sLastName { get; set; }
            public string sGender { get; set; }
            public string sBan { get; set; }
            public string sExt { get; set; }
            public string sCountryCode { get; set; }
            public string sPhoneNo { get; set; }
            public string sMobile { get; set; }
            public string sAgentSign { get; set; }
            public string sAccountType { get; set; }
            public string sAccountStatus { get; set; }
            public string sAssignGroup { get; set; }

        }

        #endregion
    }
}
