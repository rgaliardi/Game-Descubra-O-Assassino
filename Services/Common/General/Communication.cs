using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Mvc;

namespace System.Web
{
    public partial class Common
    {
        public static Extensions.Result SendEmail(string ambient, string fromAddress, string toAddress, string subject, string mailBody, string ccAddress = null, string _replyto = null, bool replayMe = false, object fileAttachment = null)
        {
            Extensions.Result _result = Extensions.Result.Email;

            if (!ambient.ToStringNullSafe().ToUpper().Equals("PRD") && !ambient.ToStringNullSafe().ToUpper().Equals("BKP"))
            {
                mailBody = "<span style='position: relative; top: 2px; font-size: 16px; color: red;'><b>Atenção!</b> Por favor, descartar esse e-mail - Teste do ambiente de Homologação!</span><br/><br/>" + mailBody;
            }

            using (var mail = new MailMessage())
            {
                mail.Subject = subject;
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(fromAddress);

                if (replayMe)
                    mail.ReplyToList.Add(new MailAddress(_replyto, "Meu Email"));

                if (!string.IsNullOrEmpty(toAddress))
                {
                    foreach (var address in toAddress.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (!string.IsNullOrEmpty(address.Trim()))
                            mail.To.Add(new MailAddress(address));
                    }
                }

                if (!string.IsNullOrEmpty(ccAddress))
                {
                    foreach (var address in ccAddress.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (!string.IsNullOrEmpty(address.Trim()))
                            mail.CC.Add(new MailAddress(address));
                    }
                }
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
                                                   DeliveryNotificationOptions.OnFailure |
                                                   DeliveryNotificationOptions.OnSuccess;

                if (fileAttachment != null)
                {
                    mail.Attachments.Add(new Attachment(fileAttachment.ToString(), MediaTypeNames.Application.Octet));
                }

                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Host = ConfigurationManager.AppSettings["HostMail"];
                        client.UseDefaultCredentials = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(mail);
                        _result = Extensions.Result.Sucsess;
                    }
                }
                catch (Exception ex)
                {
                    Common.LogError(ex);
                }
            }
            return _result;
        }
    }
}
