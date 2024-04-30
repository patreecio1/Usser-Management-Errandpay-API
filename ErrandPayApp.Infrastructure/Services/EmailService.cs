
using ErrandPayApp.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
 
using System.Linq;
using FastMember;
using System.Text;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Enums;

namespace ErrandPayApp.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILoggerService<EmailService> _loggerService;
        private readonly MailConfiguration _mailSettings;
        private readonly AppUrl _appUrl;
        private readonly IUserRepository userRepository;
        public EmailService( MailConfiguration mailConfiguration, AppUrl appUrl, IUserRepository userRepository,
       
            ILoggerService<EmailService> loggerService)
        {
            _loggerService = loggerService;
            _mailSettings = mailConfiguration;
            _appUrl = appUrl;
            this.userRepository = userRepository;
        }

       
        public async Task<bool> Send(EmailRequestModel model)
        {
           // string master = await File.ReadAllTextAsync(Constants.MasterTemplate);
            //master = master.Replace("[Content]", model.Body);
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            try
            {
                message.IsBodyHtml = true;
                MailAddress fromAddress = new MailAddress(_mailSettings.MailFrom, model.SenderDisplayName ?? _mailSettings.MailFromName);
                message.From = fromAddress;


                if (_appUrl.EnvMode != EnvMode.Live.ToString())
                {
                    foreach (var to in model.DestinationEmail)
                    {
                        message.To.Add(new MailAddress(to.Key.ToLower().Replace("lafarge", "yopmail").Replace("holcim", "yopmail").Replace("lafargeholcim","yopmail"), to.Value.ToLower().Replace("lafarge", "yopmail").Replace("lafargeholcim", "yopmail").Replace("holcim", "yopmail")));
                    }
                }
                else
                {
                    foreach (var to in model.DestinationEmail)
                    {
                        message.To.Add(new MailAddress(to.Key, to.Value));
                    }

                }
            

                foreach (var to in model.Bcc)
                {
                    message.Bcc.Add(new MailAddress(to.Key, to.Value));
                    message.Bcc.Add(new MailAddress("babakenny@gmail.com", "babakenny@gmail.com"));
                }

                foreach (var to in model.Cc)
                {
                    message.CC.Add(new MailAddress(to.Key, to.Value));
                }
                _loggerService.LogInformation("About to compose attachment");
               
                for (int i = 0; i < model.Attachement.Length; i++)
                {
                    try
                    {
                        if (i == 1)
                        {
                            string atta = model.Attachement[i];
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(model.Attachement[1]);
                            //  var file = new FileModel($"Attachment{i + 1}", attch, new MemoryStream(bytes));
                            var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);

                            Attachment att = new Attachment(new MemoryStream(buffer), contentType);
                            att.ContentDisposition.FileName = $"Attachment{i + 1}" + ".pdf";
                            message.Attachments.Add(att);
                        }
                        else
                        {
                            string attch = model.Attachement[i];

                            byte[] bytes = ConvertStringToByteArray(attch);// System.IO.File.ReadAllBytes(attch);


                            //  var file = new FileModel($"Attachment{i + 1}", attch, new MemoryStream(bytes));
                            var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);

                            Attachment att = new Attachment(new MemoryStream(bytes), contentType);
                            att.ContentDisposition.FileName = $"Attachment{i + 1}" + ".pdf";
                            message.Attachments.Add(att);

                        }
                        //message.Attachments.Add(new Attachment(file.Content, $"Attachment{i + 1}{file.Extension}"));
                    }
                    catch (Exception ex)
                    {
                        _loggerService.LogInformation("Error during attaching");
                        _loggerService.LogError(ex);
                    }
                }
               
                message.Subject = model.Subject;
                message.Body = model.Body;// master;
                message.IsBodyHtml = true;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = _mailSettings.SMTPServer;
                smtpClient.Port = _mailSettings.SMTPPort;
              //  smtpClient.Credentials = new System.Net.NetworkCredential(_mailSettings.SMTPUserName, _mailSettings.SMTPPassword); //not neccessary
              //  smtpClient.EnableSsl = true;
                // smtpClient.UseDefaultCredentials = true;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return false;
            }
            finally
            {
                smtpClient.Dispose();
                message.Dispose();
            }
        }


        private  byte[]   ConvertStringToByteArray(string byteString)
        {
           // string byteString = "48-65-6C-6C-6F";
            string[] strArray = byteString.Split('-');
            byte[] byteArray = new byte[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                byteArray[i] = Convert.ToByte(strArray[i], 16);
            }
            return byteArray;

        }
          private static string ConvertToHtml(DataTable dt)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<body>");
            sb.AppendLine("<table BORDER ='1'>");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.AppendFormat("<th align = 'center'>{0}</th>", dc.ColumnName);
            }

            foreach (DataRow dr in dt.Rows)

            {
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    string cellValue = dr[dc] != null ? dr[dc].ToString() : "";
                    sb.AppendFormat("<td>{0}</ td>", cellValue);
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

      
        private string BuildContent(DataTable dt)
        {
            var dataTable = dt;

        

            StringBuilder sb = new StringBuilder();
            //Table start.
            sb.Append("<table width='100%'>");



            //Adding HeaderRow.
            sb.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {

                sb.Append("<th>" + column.ColumnName + "</th>");
            }
            sb.Append("</tr>");


            //Adding DataRow.
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");


                foreach (DataColumn column in dt.Columns)
                {
                    
                   
                    sb.Append("<td>" + row[column.ColumnName].ToString() + "</td>");
                     
                }
                sb.Append("</tr>");
            }

            

            sb.Append("</tr>");
            //Table end.
            sb.Append("</table>");
            return sb.ToString();



        }
       
    }
}
