using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using HVTApp.Infrastructure.Interfaces.Services;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace HVTApp.Services.MessagesOutlookService
{
    public class OutlookService : IEmailService
    {
        private Outlook.Application GetApplicationObject()
        {

            Outlook.Application application = null;

            // Check whether there is an Outlook process running.
            if (Process.GetProcessesByName("OUTLOOK").Any())
            {
                // If so, use the GetActiveObject method to obtain the process and cast it to an Application object.
                application = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            else
            {

                // If not, create a new instance of Outlook and sign in to the default profile.
                application = new Outlook.Application();
                Outlook.NameSpace nameSpace = application.GetNamespace("MAPI");
                nameSpace.Logon("", "", Missing.Value, Missing.Value);
                nameSpace = null;
            }

            // Return the Outlook Application object.
            return application;
        }

        public void SendMail(string to, string subject, string body)
        {
            var application = GetApplicationObject();
            //var application = new Outlook.Application();

            // Create a new MailItem and set the To, Subject, and Body properties.
            Outlook.MailItem mailItem = (Outlook.MailItem)application.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.To = to;
            mailItem.Subject = subject;
            mailItem.Body = body;
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal;

            mailItem.Send();
        }

        public static void SendEmailFromAccount(Outlook.Application application, string subject, string body, string to, string smtpAddress)
        {

            // Create a new MailItem and set the To, Subject, and Body properties.
            Outlook.MailItem newMail = (Outlook.MailItem)application.CreateItem(Outlook.OlItemType.olMailItem);
            newMail.To = to;
            newMail.Subject = subject;
            newMail.Body = body;

            // Retrieve the account that has the specific SMTP address.
            Outlook.Account account = GetAccountForEmailAddress(application, smtpAddress);
            // Use this account to send the email.
            newMail.SendUsingAccount = account;
            newMail.Send();
        }


        public static Outlook.Account GetAccountForEmailAddress(Outlook.Application application, string smtpAddress)
        {

            // Loop over the Accounts collection of the current Outlook session.
            Outlook.Accounts accounts = application.Session.Accounts;
            foreach (Outlook.Account account in accounts)
            {
                // When the email address matches, return the account.
                if (account.SmtpAddress == smtpAddress)
                {
                    return account;
                }
            }
            throw new System.Exception($"No Account with SmtpAddress: {smtpAddress} exists!");
        }
    }
}