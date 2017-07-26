using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Web;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Net.Mail;

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
        public void LogIn_OLD(string sJasonInput)
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void LogIn(string sJasonInput)
        {
            string sFuncName = "LogIn()";
            string sErrDesc = string.Empty;
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oDt = new DataTable();
            DataTable oSQLDt = new DataTable();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sUserName = string.Empty;
                string sPassword = string.Empty;
                string sPasswordMd5 = string.Empty;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    oDt = oDs.Tables[0];
                    DataRow oDr = oDt.Rows[0];
                    sUserName = oDr["UserName"].ToString().Trim();
                    sPassword = oDr["PassWord"].ToString();
                    using (MD5 md5Hash = MD5.Create())
                    {
                        sPasswordMd5 = GetMd5Hash(md5Hash, sPassword);
                    }
                }

                sSQL = "SELECT * FROM users where email = @UserName and password = @Password and active=1";
                //oDs = Functions.ExecuteDataSet(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@UserName", sUserName), Data.CreateParameter("@Password", sPasswordMd5));
                oSQLDt = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@UserName", sUserName), Data.CreateParameter("@Password", sPasswordMd5));
                if (oSQLDt.Rows.Count == 0)
                {
                    sErrDesc = "Invalid UserName or Password";
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile(sErrDesc, sFuncName);
                    throw new Exception(sErrDesc);
                }
                else
                {
                    DataTable oRetDt = new DataTable();
                    DataTable oRetDt1 = new DataTable();
                    oRetDt1 = Functions.SuccessOutput("Valid Input");

                    oRetDt = oSQLDt.Copy();
                    oRetDt.TableName = "USERINFO";
                    oRetDs.Tables.Add(oRetDt1);
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetCustomerTickets(string sJasonInput)
        {
            string sFuncName = "GetCustomerTickets()";
            string sErrDesc = string.Empty;
            string sSQL = string.Empty;
            DataTable oDt = new DataTable();
            DataSet oRetDs = new DataSet();
            DataTable RetDT = new DataTable();
            DataTable RetDT1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sUserId = string.Empty;
                string sStatusId = string.Empty;

                DataSet oDs = new DataSet();
                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    oDt = oDs.Tables[0];
                    DataRow oDr = oDt.Rows[0];
                    sUserId = oDr["UserID"].ToString().Trim();
                    sStatusId = oDr["StatusId"].ToString().Trim();
                }
                sSQL = "SELECT a.*, b.*,c.first_name as 'AssignedTOName', d.user_name  'customberName',s.name as 'TicketStatus' ";
                sSQL = sSQL + " FROM tickets a ";
                sSQL = sSQL + " LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id ";
                sSQL = sSQL + " LEFT JOIN users c ON a.assigned_to=c.id ";
                sSQL = sSQL + " LEFT JOIN users d ON a.user_id=d.id ";
                sSQL = sSQL + " LEFT JOIN ticket_status s ON a.status=s.id ";
                sSQL = sSQL + " WHERE a.user_id= '" + sUserId + "'";
                sSQL = sSQL + " AND A.[status] = (CASE WHEN ISNULL('" + sStatusId + "','') = '' THEN A.[status] ELSE '" + sStatusId + "' END) ";
                sSQL = sSQL + " ORDER BY a.ticket_number DESC";
                RetDT = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@UserName", sUserId));
                oDs = new DataSet();

                if (RetDT.Rows.Count > 0)
                {
                    RetDT1 = Functions.SuccessOutput("Valid Input");
                    RetDT.TableName = "TICKETS";
                    oRetDs.Tables.Add(RetDT1);
                    oRetDs.Tables.Add(RetDT);
                }
                else
                {
                    throw new Exception("No data found");
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(oRetDs));
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetAllTickets()
        {
            string sFuncName = "GetAllTickets()";
            string sErrDesc = string.Empty;
            string sSQL = string.Empty;
            DataTable oDt = new DataTable();
            DataSet oRetDs = new DataSet();
            DataTable RetDT = new DataTable();
            DataTable RetDT1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                DataSet oDs = new DataSet();

                sSQL = "SELECT a.*, b.*,c.first_name as 'AssignedTOName', d.user_name  'customberName',s.name as 'TicketStatus' ";
                sSQL = sSQL + " FROM tickets a ";
                sSQL = sSQL + " LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id ";
                sSQL = sSQL + " LEFT JOIN users c ON a.assigned_to=c.id ";
                sSQL = sSQL + " LEFT JOIN users d ON a.user_id=d.id ";
                sSQL = sSQL + " LEFT JOIN ticket_status s ON a.status=s.id ";
                sSQL = sSQL + " ORDER BY a.ticket_number DESC";
                RetDT = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);
                oDs = new DataSet();

                if (RetDT.Rows.Count > 0)
                {
                    RetDT1 = Functions.SuccessOutput("Valid Input");
                    RetDT.TableName = "TICKETS";
                    oRetDs.Tables.Add(RetDT1);
                    oRetDs.Tables.Add(RetDT);
                }
                else
                {
                    throw new Exception("No Data Found");
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(oRetDs));
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetConsultantTickets(string sJasonInput)
        {
            string sFuncName = "GetConsultantTickets()";
            string sErrDesc = string.Empty;
            string sSQL = string.Empty;
            DataSet oRetDs = new DataSet();
            DataTable oDt = new DataTable();
            DataTable RetDT = new DataTable();
            DataTable RetDT1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sUserId = string.Empty;

                DataSet oDs = new DataSet();
                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    oDt = oDs.Tables[0];
                    DataRow oDr = oDt.Rows[0];
                    sUserId = oDr["UserID"].ToString().Trim();
                }
                sSQL = "SELECT a.*, b.*,c.first_name as 'AssignedTOName', d.user_name  'customberName',s.name as 'TicketStatus' ";
                sSQL = sSQL + " FROM tickets a ";
                sSQL = sSQL + " LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id ";
                sSQL = sSQL + " LEFT JOIN users c ON a.assigned_to=c.id ";
                sSQL = sSQL + " LEFT JOIN users d ON a.user_id=d.id ";
                sSQL = sSQL + " LEFT JOIN ticket_status s ON a.status=s.id ";
                sSQL = sSQL + " WHERE a.assigned_to= '" + sUserId + "'";
                sSQL = sSQL + " ORDER BY a.ticket_number DESC";
                RetDT = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@UserName", sUserId));
                oDs = new DataSet();

                if (RetDT.Rows.Count > 0)
                {
                    RetDT1 = Functions.SuccessOutput("Valid Input");
                    RetDT.TableName = "TICKETS";
                    oRetDs.Tables.Add(RetDT1);
                    oRetDs.Tables.Add(RetDT);
                }
                else
                {
                    throw new Exception("No datas found");
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(oRetDs));
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetAllUsers()
        {
            string sFuncName = "GetAllUsers()";
            string sErrDesc = string.Empty;
            string sSQL = string.Empty;
            DataSet oRetDs = new DataSet();
            DataTable RetDT = new DataTable();
            DataTable RetDT1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                DataSet oDs = new DataSet();

                sSQL = "SELECT * FROM users ";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing Query " + sSQL, sFuncName);
                RetDT = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);
                oDs = new DataSet();

                if (RetDT.Rows.Count > 0)
                {
                    RetDT1 = Functions.SuccessOutput("Valid Input");
                    RetDT.TableName = "USERS";
                    oRetDs.Tables.Add(RetDT1);
                    oRetDs.Tables.Add(RetDT);
                }
                else
                {
                    throw new Exception("No datas found");
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(oRetDs));
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetAllConsultants()
        {
            string sFuncName = "GetAllConsultants()";
            string sErrDesc = string.Empty;
            string sSQL = string.Empty;
            DataTable RetDT = new DataTable();
            DataTable RetDT1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                DataSet oDs = new DataSet();

                sSQL = "SELECT id,[user_name],email,account_type ";
                sSQL = sSQL + " FROM users ";
                sSQL = sSQL + " WHERE account_type = 'Consultant' AND active = 1 ";
                sSQL = sSQL + " ORDER BY id";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing Query " + sSQL, sFuncName);
                RetDT = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);

                if (RetDT.Rows.Count > 0)
                {
                    RetDT1 = Functions.SuccessOutput("Valid input");
                    RetDT.TableName = "CONSULTANTS";
                    oDs.Tables.Add(RetDT1);
                    oDs.Tables.Add(RetDT);
                }
                else
                {
                    throw new Exception("No data found");
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(oDs));
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void AssignTickets(string sJasonInput)
        {
            string sFuncName = "AssignTickets()";
            string sErrDesc = string.Empty;
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oDt = new DataTable();
            DataTable oSQLDt = new DataTable();
            DataTable oRetDt = new DataTable();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sTicketId = string.Empty;
                string sUserId = string.Empty;
                string sPasswordMd5 = string.Empty;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    oDt = oDs.Tables[0];
                    DataRow oDr = oDt.Rows[0];
                    sTicketId = oDr["TID"].ToString().Trim();
                    sUserId = oDr["UID"].ToString();
                }

                sSQL = "UPDATE tickets SET assigned_to = @AssignedTo WHERE id = @TicketId ";
                Functions.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@TicketId", sTicketId), Data.CreateParameter("@AssignedTo", sUserId));

                oRetDt = Functions.SuccessOutput("Ticket " + sTicketId + " updated successfully");
                oRetDs.Tables.Add(oRetDt);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(oRetDs));

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void CreateUser(string sJasonInput)
        {
            string sFuncName = "CreateUser()";
            string sErrDesc = string.Empty;
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oDt = new DataTable();
            DataTable oSQLDt = new DataTable();
            DataTable oRetDt = new DataTable();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sUserName = string.Empty;
                string sFirstName = string.Empty;
                string sLastName = string.Empty;
                int iGender = 1;
                string sEmail = string.Empty;
                string sPassword = string.Empty;
                string sPasswordMd5 = string.Empty;
                string sCountryCode = string.Empty;
                string sPhoneNo = string.Empty;
                string sAcctType = string.Empty;
                int iActive = 1;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sUserName = oDs.Tables[0].Rows[0]["user_name"].ToString();
                    sFirstName = oDs.Tables[0].Rows[0]["first_name"].ToString();
                    sLastName = oDs.Tables[0].Rows[0]["last_name"].ToString();
                    iGender = Convert.ToInt16(oDs.Tables[0].Rows[0]["gender"]);
                    sEmail = oDs.Tables[0].Rows[0]["email"].ToString();
                    sPassword = oDs.Tables[0].Rows[0]["password"].ToString();
                    sCountryCode = oDs.Tables[0].Rows[0]["country_code"].ToString();
                    sPhoneNo = oDs.Tables[0].Rows[0]["phone_number"].ToString();
                    sAcctType = oDs.Tables[0].Rows[0]["account_type"].ToString();
                    using (MD5 md5Hash = MD5.Create())
                    {
                        sPasswordMd5 = GetMd5Hash(md5Hash, sPassword);
                    }

                    sSQL = "SELECT COUNT(email) MNO FROM USERS WHERE email = @email ";
                    oRetDt = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@email", sEmail));
                    if (oRetDt.Rows.Count > 0)
                    {
                        int iCount;
                        iCount = Convert.ToInt16(oRetDt.Rows[0]["MNO"]);
                        if (iCount > 0)
                        {
                            throw new Exception("Email id already exists");
                        }
                        else
                        {
                            oRetDt = new DataTable();
                            sSQL = "INSERT INTO USERS([user_name],first_name,last_name,gender,email,[password],country_code,phone_number,account_type,active,created_at) ";
                            sSQL = sSQL + " VALUES (@UserName,@FirstName,@LastName,@Gender,@Email,@Password,@CountryCode,@Phone,@AcctType,@Active,@CreatedAt) ";

                            Functions.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@UserName", sUserName),
                                Data.CreateParameter("@FirstName", sFirstName), Data.CreateParameter("@LastName", sLastName), Data.CreateParameter("@Gender", iGender),
                                Data.CreateParameter("@Email", sEmail), Data.CreateParameter("@Password", sPasswordMd5), Data.CreateParameter("@CountryCode", sCountryCode),
                                Data.CreateParameter("@Phone", sPhoneNo), Data.CreateParameter("@AcctType", sAcctType), Data.CreateParameter("@Active", iActive),
                                Data.CreateParameter("@CreatedAt", DateTime.Now));

                            oRetDt = Functions.SuccessOutput("User " + sUserName + " created successfully");
                            oRetDs.Tables.Add(oRetDt);
                        }

                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));

                }
                else
                {
                    throw new Exception("No datas found");
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void UpdateUserStatus(string sJasonInput)
        {
            string sFuncName = "UpdateUserStatus()";
            string sErrDesc = string.Empty;
            DataSet oDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oSQLDt = new DataTable();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sUserId = string.Empty;
                int iActive;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sUserId = oDs.Tables[0].Rows[0]["UID"].ToString();
                    iActive = Convert.ToInt16(oDs.Tables[0].Rows[0]["active"].ToString());

                    sSQL = "UPDATE users SET active = @Active,updated_at = @UpdatedAt WHERE id = @UserId ";
                    Functions.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@Active", iActive),
                        Data.CreateParameter("@UserId", sUserId), Data.CreateParameter("@UpdatedAt", DateTime.Now));

                    oRetDt = Functions.SuccessOutput("User Id " + sUserId + " status updated successfully");
                    oDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oDs));
                }
                else
                {
                    throw new Exception("No datas found");
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetTicketDetails(string sJasonInput)
        {
            string sFuncName = "GetTicketDetails";
            string sSQL = string.Empty;
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oRetDt2 = new DataTable();
            DataTable oRetDt3 = new DataTable();
            Boolean IsData = false;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sTicketId = string.Empty;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sTicketId = oDs.Tables[0].Rows[0]["TID"].ToString();
                }

                if (sTicketId != "")
                {
                    sSQL = "select b.name [StatusName],(SELECT name FROM organization X WHERE x.id = c.company) [CompanyName],c.mobile,c.email,FORMAT(a.duedate,'dd/MM/yyyy') [CDDate], ";
                    sSQL = sSQL + " d.[priority] [priorityName],d.priority_color,(SELECT [user_name] FROM users Y WHERE Y.id = a.assigned_to) [AssignedName], a.* ";
                    sSQL = sSQL + " from tickets a left join ticket_status b on b.id = a.[status] ";
                    sSQL = sSQL + " left join users c on c.id = a.[user_id] left join ticket_priority d on d.priority_id = a.priority_id ";
                    sSQL = sSQL + " WHERE a.id = @TicketId ";
                    oRetDt = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@TicketId", sTicketId));
                    if (oRetDt.Rows.Count > 0)
                    {
                        oRetDt.TableName = "TICKETS";
                        oRetDs.Tables.Add(oRetDt);
                        IsData = true;
                    }
                    sSQL = "SELECT B.[user_name],B.email, A.* FROM ticket_thread A LEFT JOIN users B ON B.id = A.[user_id] WHERE A.ticket_id = @TicketId ";
                    oRetDt2 = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@TicketId", sTicketId));
                    if (oRetDt2.Rows.Count > 0)
                    {
                        oRetDt2.TableName = "TICKET_THREAD";
                        oRetDs.Tables.Add(oRetDt2);
                        IsData = true;
                    }

                    if (IsData == false)
                    {
                        throw new Exception("No datas found");
                    }
                    else
                    {
                        oRetDt3 = Functions.SuccessOutput("Valid data");
                        oRetDs.Tables.Add(oRetDt3);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("Ticket id is mandatory");
                }

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void CreateTicket(string sJasonInput)
        {
            string sFuncName = "CreateTicket";
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                string sTicketNumber = string.Empty;
                string sUserId = string.Empty;
                string sPriorityId = string.Empty;
                string sStatus = string.Empty;
                string sScenario = string.Empty;
                string sExpScenario = string.Empty;
                string sActScenario = string.Empty;
                string sSubject = string.Empty;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sUserId = oDs.Tables[0].Rows[0]["user_id"].ToString();
                    sPriorityId = oDs.Tables[0].Rows[0]["priority_id"].ToString();
                    sStatus = oDs.Tables[0].Rows[0]["status"].ToString();
                    sScenario = oDs.Tables[0].Rows[0]["Scenario"].ToString();
                    sExpScenario = oDs.Tables[0].Rows[0]["ExpectedScenario"].ToString();
                    sActScenario = oDs.Tables[0].Rows[0]["ActualScenario"].ToString();
                    sSubject = oDs.Tables[0].Rows[0]["Subject"].ToString();

                    sSQL = "SELECT SUBSTRING(ticket_number,0,5) + '-' + CONVERT(CHAR,SUBSTRING(ticket_number,6,LEN(TICKET_NUMBER) - 5) + 1) ticket_number FROM tickets ";
                    sSQL = sSQL + "WHERE ID = (SELECT MAX(id) FROM tickets)";
                    DataSet oDs1 = new DataSet();
                    oDs1 = Functions.ExecuteDataSet(ConnectionString, CommandType.Text, sSQL);
                    if (oDs1.Tables[0].Rows.Count > 0)
                    {
                        sTicketNumber = oDs1.Tables[0].Rows[0]["ticket_number"].ToString().Trim();
                    }

                    sSQL = "INSERT INTO tickets(ticket_number,[user_id],priority_id,[status],Scenario,ExpectedScenario,ActualScenario,[Subject],created_at) ";
                    sSQL = sSQL + " VALUES(@TicketNo,@Userid,@PriorityId,@Status,@Scenario,@ExpScenario,@ActScenario,@Subject,@CreatedAt)";
                    Functions.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@TicketNo", sTicketNumber), Data.CreateParameter("@Userid", sUserId),
                        Data.CreateParameter("@PriorityId", sPriorityId), Data.CreateParameter("@Status", sStatus), Data.CreateParameter("@Scenario", sScenario),
                        Data.CreateParameter("@ExpScenario", sExpScenario), Data.CreateParameter("@ActScenario", sActScenario), Data.CreateParameter("@Subject", sSubject),
                        Data.CreateParameter("@CreatedAt", DateTime.Now));

                    oRetDt = Functions.SuccessOutput("Ticket number " + sTicketNumber + " created successfully");
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("No Record Found");
                }

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void CreateTicketThread(string sJasonInput)
        {
            string sFuncName = "CreateTicketThread";
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                string sTicketId = string.Empty;
                string sUserId = string.Empty;
                string sIsInternal = string.Empty;
                string sTitle = string.Empty;
                string sBody = string.Empty;
                double dblActTime = 0;
                double dblBillTime = 0;
                string sToMailAddress = string.Empty;
                string sSendTo = string.Empty;
                string sCCAddress = string.Empty;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sTicketId = oDs.Tables[0].Rows[0]["TID"].ToString();
                    sUserId = oDs.Tables[0].Rows[0]["UID"].ToString();
                    sIsInternal = oDs.Tables[0].Rows[0]["is_internal"].ToString();
                    sTitle = oDs.Tables[0].Rows[0]["title"].ToString();
                    sBody = oDs.Tables[0].Rows[0]["bodyText"].ToString();
                    sSendTo = oDs.Tables[0].Rows[0]["ToAddress"].ToString();
                    sCCAddress = oDs.Tables[0].Rows[0]["CCAddress"].ToString();
                    dblActTime = Convert.ToDouble(oDs.Tables[0].Rows[0]["ActTimeSpent"].ToString());
                    dblBillTime = Convert.ToDouble(oDs.Tables[0].Rows[0]["TimeCharged"].ToString());

                    sSQL = "INSERT INTO ticket_thread(ticket_id,[user_id],is_internal,title,bodyText,ActTimeSpent,TimeCharged,created_at) ";
                    sSQL = sSQL + " VALUES(@TicketId,@UserId,@IsInternal,@Title,@Body,@ActTimeSpent,@TimeCharged,@CreatedAt)";
                    Functions.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@TicketId", sTicketId), Data.CreateParameter("@UserId", sUserId),
                        Data.CreateParameter("@IsInternal", sIsInternal), Data.CreateParameter("@Title", sTitle), Data.CreateParameter("@Body", sBody),
                        Data.CreateParameter("@ActTimeSpent", dblActTime), Data.CreateParameter("@TimeCharged", dblBillTime), Data.CreateParameter("@CreatedAt", DateTime.Now));

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Sending Mail", sFuncName);
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Setting SMTP Properties", sFuncName);
                    SmtpClient smtpclient = new SmtpClient("smtp.gmail.com", 587);
                    smtpclient.UseDefaultCredentials = false;
                    smtpclient.Credentials = new System.Net.NetworkCredential("sapb1.abeoelectra@gmail.com", "abeo1234");
                    smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpclient.EnableSsl = true;

                    string[] MailTo = sSendTo.Split(';');

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("sapb1.abeoelectra@gmail.com");
                    message.To.Add(new MailAddress(sSendTo));

                    string sEmailAddress = string.Empty;
                    foreach (string sEmailAddress_loopVariable in MailTo)
                    {
                        sEmailAddress = sEmailAddress_loopVariable;
                        message.CC.Add(new MailAddress(sEmailAddress));
                    }
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.Subject = sTitle;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.Body = sBody;
                    message.IsBodyHtml = true;

                    object userstate = message;

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Sending Mail", sFuncName);
                    smtpclient.Send(message);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Mail Sent successfully ", sFuncName);

                    oRetDt = Functions.SuccessOutput("Ticket Thread for ticket id " + sTicketId + " created successfully");
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("No Record Found");
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetUnassignedTickets()
        {
            string sFuncName = "GetUnassignedTickets";
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oRetDt1 = new DataTable();
            string sSQL = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sSQL = "SELECT * FROM tickets WHERE ISNULL(assigned_to,'') = '' ";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing Query " + sSQL, sFuncName);
                oRetDt = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);
                if (oRetDt.Rows.Count > 0)
                {
                    oRetDt1 = Functions.SuccessOutput("Valid input");
                    oRetDt.TableName = "TICKETS";
                    oRetDs.Tables.Add(oRetDt1);
                    oRetDs.Tables.Add(oRetDt);
                }
                else
                {
                    throw new Exception("No datas found");
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Dataset is not null", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(oRetDs));
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetAccountType()
        {
            string sFuncName = "GetAccountType";
            string sSQL = string.Empty;
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oRetDt1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                sSQL = "select account_type from users group by account_type";
                oRetDt = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);
                if (oRetDt.Rows.Count > 0)
                {
                    oRetDt1 = Functions.SuccessOutput("Valid Input");
                    oRetDt.TableName = "ACCOUNTTYPE";
                    oRetDs.Tables.Add(oRetDt1);
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("No data found");
                }

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetAllTicketStatus()
        {
            string sFuncName = "GetAllTicketStatus";
            string sSQL = string.Empty;
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oRetDt1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                sSQL = "SELECT * FROM ticket_status";
                oRetDt = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);
                if (oRetDt.Rows.Count > 0)
                {
                    oRetDt1 = Functions.SuccessOutput("Valid Input");
                    oRetDt.TableName = "TICKETSTATUS";
                    oRetDs.Tables.Add(oRetDt1);
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("No data found");
                }

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void UpdateTicketStatus(string sJasonInput)
        {
            string sFuncName = "UpdateTicketStatus";
            string sSQL = string.Empty;
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oRetDt1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting function", sFuncName);

                string sTicketId = string.Empty;
                string sStatus = string.Empty;
                string sEmail = string.Empty;
                string sUserName = string.Empty;
                string sBody = string.Empty;
                string sBodySubject = string.Empty;
                string sMailSubject = string.Empty;
                string sNotification = string.Empty;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sTicketId = oDs.Tables[0].Rows[0]["TID"].ToString();
                    sStatus = oDs.Tables[0].Rows[0]["StatusID"].ToString();
                    sNotification = oDs.Tables[0].Rows[0]["Notification"].ToString();

                    sSQL = "UPDATE tickets SET [status] = @StatusId WHERE id = @TicketId ";
                    Functions.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@StatusId", sStatus), Data.CreateParameter("@TicketId", sTicketId));

                    if (sStatus == "2" && sNotification == "True")
                    {

                        //sSQL = "SELECT first_name + ' ' + last_name [UserName],email FROM users WHERE id = (SELECT [user_id] FROM tickets WHERE id = @TicketId)";
                        sSQL = "SELECT B.first_name + ' ' + B.last_name [UserName],B.email,A.ticket_number + ' ' + A.[Subject] [BodySubject], '[##' + A.ticket_number + '##] Your request has been closed' [MailSubject] ";
                        sSQL = sSQL + " FROM tickets A LEFT JOIN users B ON B.id = A.[user_id] ";
                        sSQL = sSQL + " WHERE B.id = (SELECT [user_id] FROM tickets WHERE id = @TicketId) ";
                        oRetDt1 = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@TicketId", sTicketId));
                        if (oRetDt1.Rows.Count > 0)
                        {
                            DataRow oRow = oRetDt1.Rows[0];
                            sUserName = oRow[0].ToString();
                            sEmail = oRow[1].ToString();
                            sBodySubject = oRow[2].ToString();
                            sMailSubject = oRow[3].ToString();

                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Building Email message", sFuncName);

                            sBody = "<div align=left style='font-size:10.0pt;font-family:Arial'>";
                            sBody = sBody + " Dear " + sUserName + ",<br /><br /> Your request \" " + sBodySubject + " \" has been closed. <br /><br />";
                            sBody = sBody + " We believe that the request has been addressed to the best of your satisfaction. To re-open this request kindly reply to this email. <br /><br />";
                            sBody = sBody + " Regards,<br />Abeo Support";

                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Setting SMTP Properties", sFuncName);
                            SmtpClient smtpclient = new SmtpClient("smtp.gmail.com", 587);
                            smtpclient.UseDefaultCredentials = false;
                            smtpclient.Credentials = new System.Net.NetworkCredential("sapb1.abeoelectra@gmail.com", "abeo1234");
                            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpclient.EnableSsl = true;

                            MailMessage message = new MailMessage();
                            message.From = new MailAddress("sapb1.abeoelectra@gmail.com");
                            message.To.Add(new MailAddress(sEmail));
                            message.SubjectEncoding = System.Text.Encoding.UTF8;
                            message.Subject = sMailSubject;
                            message.BodyEncoding = System.Text.Encoding.UTF8;
                            message.Body = sBody;
                            message.IsBodyHtml = true;

                            object userstate = message;

                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Sending Mail", sFuncName);
                            smtpclient.Send(message);

                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Mail Sent successfully to " + sEmail, sFuncName);
                        }
                    }
                    oRetDt = Functions.SuccessOutput("Ticket Status updated successfully");
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("No data found");
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SetTicketDueDate(string sJasonInput)
        {
            string sFuncName = "SetTicketDueDate";
            string sSQL = string.Empty;
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sTicketId = string.Empty;
                string sDueDate = string.Empty;
                DateTime dDueDate;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sTicketId = oDs.Tables[0].Rows[0]["TID"].ToString();
                    dDueDate = DateTime.Parse(oDs.Tables[0].Rows[0]["DueDate"].ToString());

                    sSQL = "UPDATE tickets SET duedate = @DueDate WHERE id = @TicketId";
                    Functions.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@DueDate", dDueDate), Data.CreateParameter("@TicketId", sTicketId));

                    oRetDt = Functions.SuccessOutput("Valid Input");
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("No data found");
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetMailNumber()
        {
            string sFuncName = "GetMailNumber";
            string sSQL = string.Empty;
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oRetDt1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                sSQL = "select last_mail_B1 from mailnumber";
                oRetDt = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);
                if (oRetDt.Rows.Count > 0)
                {
                    oRetDt1 = Functions.SuccessOutput("Valid Input");
                    oRetDt.TableName = "MAILNUMBER";
                    oRetDs.Tables.Add(oRetDt1);
                    oRetDs.Tables.Add(oRetDt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                    Context.Response.Output.Write(Functions.ds2json(oRetDs));
                }
                else
                {
                    throw new Exception("No data found");
                }

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void GetCustomerDashboardData(string sJasonInput)
        {
            string sFuncName = "GetCustomerDashboardData";
            string sSQL = string.Empty;
            DataSet oDs = new DataSet();
            DataSet oRetDs = new DataSet();
            DataTable oRetDt = new DataTable();
            DataTable oRetDt1 = new DataTable();

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function", sFuncName);
                string sUserId = string.Empty;

                oDs = Functions.jsontodata(sJasonInput);
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    sUserId = oDs.Tables[0].Rows[0]["UserId"].ToString();

                    sSQL = "SELECT (SELECT COUNT(*) FROM tickets WHERE [status] = 1 AND [user_id] = @UserId) [OpenTicket], ";
                    sSQL = sSQL + " (SELECT COUNT(*) FROM tickets WHERE [status] = 2 AND [user_id] = @UserId) [ClosedTicket], (SELECT COUNT(*) FROM tickets WHERE [user_id] = @UserId) [TotalTicket] ";

                    oRetDt = Functions.ExecuteDataTable(ConnectionString, CommandType.Text, sSQL, Data.CreateParameter("@UserId", sUserId));
                    if (oRetDt.Rows.Count > 0)
                    {
                        oRetDt1 = Functions.SuccessOutput("Valid Data");
                        oRetDt.TableName = "CDD";
                        oRetDs.Tables.Add(oRetDt1);
                        oRetDs.Tables.Add(oRetDt);

                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                        Context.Response.Output.Write(Functions.ds2json(oRetDs));
                    }
                    else
                    {
                        throw new Exception("No data found");
                    }
                }
                else
                {
                    throw new Exception("No data found");
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed with ERROR", sFuncName);
                Context.Response.Output.Write(Functions.ds2json(Functions.ErrorHandling(ex.Message.ToString())));
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
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
