using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D.Net.EmailInterfaces;
using D.Net.EmailClient;
using System.Configuration;
using System.Data;

namespace IMAP
{
    public class EmailReader
    {
        static void Main(string[] args)
        {
            string sFuncName = string.Empty;
            clsLog oLog = new clsLog();
            Logic oLogic = new Logic();
            string sErrDesc = string.Empty;

            try
            {
                sFuncName = "Main Program";
                oLog.WriteToDebugLogFile("Starting Program", sFuncName);
                string sTicketNumber = string.Empty;
                string sBodyContent = string.Empty;
                string sServer = ConfigurationManager.AppSettings["Server"];
                int iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                string sFromEmail = ConfigurationManager.AppSettings["FromEmail"];
                string sFromPassword = ConfigurationManager.AppSettings["FromPassword"];

                IEmailClient ImapClient = EmailClientFactory.GetClient(EmailClientEnum.IMAP);
                ImapClient.Connect(sServer, sFromEmail, sFromPassword, iPort, true);
                ImapClient.SetCurrentFolder("INBOX");
                ImapClient.LoadMessages();
                string sLastEmailId = oLogic.GetMailNumber();
                if (sLastEmailId != "NO RECORDS")
                {
                    int iMailId = Convert.ToInt32(sLastEmailId);
                    for (int i = 440; i <= ImapClient.Messages.Count - 1; i++)
                    {
                        string sCC = string.Empty;
                        oLog.WriteToDebugLogFile("Count of i : " + i, sFuncName);
                        IEmail msm = (IEmail)ImapClient.Messages[i];
                        if (!msm.Subject.Contains("Delivery Status Notification"))
                        {
                            if (msm.SequenceNumber > iMailId)
                            {
                                msm.LoadInfos();

                                foreach (var item in msm.Cc)
                                {
                                    if (sCC.Length == 0)
                                    {
                                        sCC = item.ToString();
                                    }
                                    else
                                    {
                                        sCC = sCC + "," + item.ToString();
                                    }
                                }
                                // Check the subject contains ticket ID or nor
                                if (msm.Subject.Contains("[##"))
                                {
                                    sTicketNumber = oLogic.Between(msm.Subject, "[##", "##]");
                                    sBodyContent = msm.TextBody.ToString().Replace("\r\n", "<br/>");
                                    //call create thread
                                    string sResult = oLogic.InsertTicketThread(sTicketNumber, sBodyContent, msm.SequenceNumber, sCC);
                                    oLog.WriteToDebugLogFile("For Ticket Number : " + sTicketNumber + " the Result is " + sResult, sFuncName);
                                }
                                else
                                {
                                    string sEmail = msm.From[0];
                                    string sUserName = oLogic.Before(sEmail, "@");
                                    string sBodyContent1 = msm.TextBody.ToString().Replace("\r\n", "<br/>");
                                    string sResult = oLogic.InsertTicketandUser(sUserName, sEmail, msm.Subject, sBodyContent1, msm.SequenceNumber, sCC);
                                    string[] sArray = sResult.Split('*');
                                    if (sArray[0] == "SUCCESS")
                                    {
                                        // Send auto reply email to the person who raised the ticket
                                        oLog.WriteToDebugLogFile("Before sending email to Email id : " + sEmail + "and username : " + sUserName , sFuncName);
                                        string sSentStatus = oLogic.SendAutomatedEmail(sEmail, sUserName, sArray[1].ToString(), msm.Subject, "[##" + sArray[1].ToString() + "##]", sBodyContent1, ref sErrDesc);
                                        oLog.WriteToDebugLogFile("AFter sending email to Email id : " + sEmail + "and username : " + sUserName, sFuncName);
                                    }
                                    oLog.WriteToDebugLogFile("For Email id : " + sEmail + "and username : " + sUserName + " : the Result is " + sResult, sFuncName);
                                }
                            }
                        }
                    }

                    oLog.WriteToDebugLogFile("Ending Program", sFuncName);
                }
                else
                {
                    oLog.WriteToDebugLogFile(sLastEmailId, sFuncName);
                }

            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
            }

        }
    }
}
