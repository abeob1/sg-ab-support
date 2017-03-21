using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.IO;

namespace IMAP
{
    public class Logic
    {
        clsLog oLog = new clsLog();
        string sFromEmail = ConfigurationManager.AppSettings["FromEmail"];
        string sFromPassword = ConfigurationManager.AppSettings["FromPassword"];
        string sSMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
        int iSMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

        public string GetMailNumber()
        {
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

        public string InsertTicketThread(string sTicketNum, string sBodyContent, Int32 sSeqNum, string sCC)
        {
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
                      Data.CreateParameter("@BodyContent", sBodyContent), Data.CreateParameter("@SequenceNum", sSeqNum),
                      Data.CreateParameter("@CC", sCC)).Tables[0];
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

        public string InsertTicketandUser(string sUserName, string sEmail, string sSubject, string sBodyContent, Int32 sSeqNum, string sCC)
        {
            string sErrDesc = string.Empty;

            string sFuncName = "InsertTicketandUser";
            string sProcName = string.Empty;
            DataTable oRetDt = new DataTable();
            string sStatus = string.Empty;
            string sResult = string.Empty;
            string sTicketNumber = string.Empty;
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            try
            {
                oLog.WriteToDebugLogFile("Starting Function", sFuncName);

                sProcName = "AE_SP002_SupportPortal_InsertTicketandUser";
                oRetDt = Functions.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName, Data.CreateParameter("@UserName", sUserName),
                     Data.CreateParameter("@Email", sEmail), Data.CreateParameter("@Subject", sSubject), Data.CreateParameter("@BodyContent", sBodyContent),
                     Data.CreateParameter("@SequenceNum", sSeqNum), Data.CreateParameter("@CC", sCC)).Tables[0];
                if (oRetDt.Rows.Count > 0)
                {
                    sStatus = oRetDt.Rows[0]["Status"].ToString();
                    sResult = oRetDt.Rows[0]["Message"].ToString();
                    sTicketNumber = oRetDt.Rows[0]["TicketNum"].ToString();
                    oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                }
                else
                {
                    sStatus = "NO RECORDS";
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
            return sStatus + "*" + sTicketNumber.Trim();
        }

        public string SendAutomatedEmail(string sEmailTo, string sUserName, string sTicketNum, string sSubject, string sTicketId, string sBodyContent, ref string sErrDesc)
        {
            string functionReturnValue = string.Empty;

            string sFuncName = "SendAutomatedEmail";

            try
            {
                oLog.WriteToDebugLogFile("Sarting function", sFuncName);
                oLog.WriteToDebugLogFile("Setting SMTP properties", sFuncName);
                SmtpClient smtpClient = new SmtpClient(sSMTPHost, iSMTPPort);

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(sFromEmail, sFromPassword);
                smtpClient.EnableSsl = true;

                oLog.WriteToDebugLogFile("Calling Function CreateDefaultMailMessage()", sFuncName);

                oLog.WriteToDebugLogFile("Sarting function", sFuncName);

                oLog.WriteToDebugLogFile("Assigning Email Properties..", sFuncName);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(sFromEmail);
                message.To.Add(new MailAddress(sEmailTo));
                oLog.WriteToDebugLogFile("Adding To Email Address" + ":  " + sEmailTo, sFuncName);
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                message.Subject = sTicketId + " - " + sSubject;
                message.BodyEncoding = System.Text.Encoding.UTF8;

                var sbMail = new StringBuilder();
                string sTemplatePath = AppDomain.CurrentDomain.BaseDirectory;
                int index = sTemplatePath.IndexOf("\\bin");
                if (index > 0)
                    sTemplatePath = sTemplatePath.Substring(0, index) + "\\Email Template\\NewTicketTemplate.htm";

                using (var sReader = new StreamReader(sTemplatePath))
                {
                    sbMail.Append(sReader.ReadToEnd());

                    sbMail.Replace("{UserName}", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(sUserName.ToLower()));
                    sbMail.Replace("{TicketNum}", sTicketNum);
                    sbMail.Replace("{Subject}", sSubject);
                    sbMail.Replace("{TicketId}", sTicketId);
                    sbMail.Replace("{BodyContent}", sBodyContent);
                }
                message.Body = sbMail.ToString();
                message.IsBodyHtml = true;
                oLog.WriteToDebugLogFile("Sending Email Message", sFuncName);
                oLog.WriteToDebugLogFile("Sending Email Messages to : " + sEmailTo, sFuncName);

                smtpClient.Send(message);
                message.Dispose();

                functionReturnValue = "SUCCESS";
                oLog.WriteToDebugLogFile("Function completed with Success", sFuncName);

            }
            catch (Exception ex)
            {
                functionReturnValue = ex.Message;
                sErrDesc = ex.Message;

                oLog.WriteToDebugLogFile("Function completed with Error", sFuncName);
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToErrorLogFile("Failed sending email to : " + " " + sEmailTo, sFuncName);

            }
            finally
            {
            }
            return functionReturnValue;

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
