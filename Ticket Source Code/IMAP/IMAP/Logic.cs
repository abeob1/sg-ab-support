using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace IMAP
{
    public class Logic
    {
        public string GetMailNumber()
        {
            clsLog oLog = new clsLog();
            string sErrDesc = string.Empty;

            string sFuncName = "GetMailNumber";
            string sSQL = string.Empty;
            DataTable oRetDt = new DataTable();
            string sResult = string.Empty;
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            try
            {
                oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                sSQL = "select IsNull(last_mail_B1,'0') MailNumber from mailnumber";
                oRetDt = Functions.ExecuteDatatable(ConnectionString, CommandType.Text, sSQL);
                if (oRetDt.Rows.Count > 0)
                {
                    sResult = oRetDt.Rows[0]["MailNumber"].ToString();
                    oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                }
                else
                {
                    sResult = "NO RECORDS";
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                sResult = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR", sFuncName);
            }
            return sResult;
        }

        public string InsertTicketThread(string sTicketNum, string sBodyContent)
        {
            clsLog oLog = new clsLog();
            string sErrDesc = string.Empty;

            string sFuncName = "InsertTicketThread";
            string sProcName = string.Empty;
            DataTable oRetDt = new DataTable();
            string sResult = string.Empty;
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            try
            {
                oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                sProcName = "AE_SP001_SupportPortal_InsertTicketThread";
                oRetDt = Functions.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName, Data.CreateParameter("@TicketNum", sTicketNum),
                     Data.CreateParameter("@BodyContent", sBodyContent)).Tables[0];
                if (oRetDt.Rows.Count > 0)
                {
                    sResult = oRetDt.Rows[0]["Message"].ToString();
                    oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                }
                else
                {
                    sResult = "NO RECORDS";
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                sResult = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR", sFuncName);
            }
            return sResult;
        }

        public string InsertTicketandUser(string sUserName, string sEmail)
        {
            clsLog oLog = new clsLog();
            string sErrDesc = string.Empty;

            string sFuncName = "InsertTicketandUser";
            string sProcName = string.Empty;
            DataTable oRetDt = new DataTable();
            string sResult = string.Empty;
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            try
            {
                oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                sProcName = "AE_SP002_SupportPortal_InsertTicketandUser";
                oRetDt = Functions.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName, Data.CreateParameter("@UserName", sUserName),
                     Data.CreateParameter("@Email", sEmail)).Tables[0];
                if (oRetDt.Rows.Count > 0)
                {
                    sResult = oRetDt.Rows[0]["Message"].ToString();
                    oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                }
                else
                {
                    sResult = "NO RECORDS";
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                sResult = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR", sFuncName);
            }
            return sResult;
        }

        public string Between(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        public string Before(string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        public string After(string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }
    }
}
