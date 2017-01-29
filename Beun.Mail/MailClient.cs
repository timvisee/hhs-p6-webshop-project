using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkPost;

namespace Beun.Mail
{
    public class MailClient
    {

        public static string ApiKey { get; set; }

        public static string Template = @"BEGIN:VCALENDAR|VERSION:2.0|BEGIN:VEVENT|DTSTART:$START$|DTEND:$END$|SUMMARY:Pasafspraak bij Honeymoon Shop|LOCATION:Korte Hoogstraat 4\, 3011GL\, Rotterdam|DESCRIPTION:Pasafspraak|PRIORITY:3|END:VEVENT|END:VCALENDAR|";

        public static void SendContactMail(string name, string to, string reference, string message, string telnr) {
            var transmission = new Transmission();


            transmission.Content.TemplateId = "honeymoon-shop-contact";

            transmission.SubstitutionData["NAME"] = name;
            transmission.SubstitutionData["TEL_NR"] = telnr;
            transmission.SubstitutionData["MESSAGE"] = message;
            transmission.SubstitutionData["REF"] = reference;
            
            var recipient = new Recipient
            {
                Address = new Address { Email = to }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ApiKey);
            client.CustomSettings.SendingMode = SendingModes.Sync;

            try {
                Task<SendTransmissionResponse> task = client.Transmissions.Send(transmission);

                if (task.Result == null) {
                    Console.WriteLine("Result from Transmission.Send was null");
                }



                Console.WriteLine(
                    String.Format(
                        "ID: {0}\nAccepted recipients: {1}\nRejected recipients: {2}\nReason: {3}\n Status code: {4}",
                        task.Result.Id, task.Result.TotalAcceptedRecipients, task.Result.TotalRejectedRecipients,
                        task.Result.ReasonPhrase, task.Result.StatusCode));
            }
            catch (Exception ex) {
                Console.WriteLine("Error while sending email\n" + ex);
            }
        }

        public static void SendBetterEmail(string name, string to, DateTime date) {
            var transmission = new Transmission();


            transmission.Content.TemplateId = "honeymoon-shop-html-appointment-created";

            transmission.SubstitutionData["NAME"] = name;
            transmission.SubstitutionData["DATE"] = $"{date:dddd d MMMM}";
            transmission.SubstitutionData["TIME"] = $"{date:H:mm}";
            transmission.SubstitutionData["CURRENT_YEAR"] = DateTime.Now.Year;

            string cal = Template.Replace("|", "\n");
            cal = cal.Replace("$START$", date.ToString("yyyyMMdd'T'HHmmss"));
            cal = cal.Replace("$END$", date.AddMinutes(30).ToString("yyyyMMdd'T'HHmmss"));

            //Console.WriteLine(cal);

            var attachment = new Attachment();
            attachment.Name = "afspraak.ical";
            attachment.Type = "text/calendar";
            attachment.Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(cal));

            transmission.Content.Attachments.Add(attachment);


            var recipient = new Recipient
            {
                Address = new Address { Email = to }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ApiKey);
            client.CustomSettings.SendingMode = SendingModes.Sync;

            try {
                Task<SendTransmissionResponse> task = client.Transmissions.Send(transmission);

                if (task.Result == null) {
                    Console.WriteLine("Result from Transmission.Send was null");
                }



                Console.WriteLine(
                    String.Format(
                        "ID: {0}\nAccepted recipients: {1}\nRejected recipients: {2}\nReason: {3}\n Status code: {4}",
                        task.Result.Id, task.Result.TotalAcceptedRecipients, task.Result.TotalRejectedRecipients,
                        task.Result.ReasonPhrase, task.Result.StatusCode));
            }
            catch (Exception ex) {
                Console.WriteLine("Error while sending email\n" + ex);
            }
        }

        public static void SendAppointmentEmail(string name, string to, DateTime date, string garment) {
            if (string.IsNullOrWhiteSpace(ApiKey)) {
                Console.WriteLine("Cancelling call to email API, ApiKey is not set!");
                return;
            }



            var transmission = new Transmission();


            transmission.Content.TemplateId = "honeymoon-shop-appointment-created";

            transmission.SubstitutionData["FNAME"] = name;
            transmission.SubstitutionData["DATE_AND_TIME"] = $"{date:dddd d MMMM} om {date:H:mm}";
            transmission.SubstitutionData["CURRENT_YEAR"] = DateTime.Now.Year;
            transmission.SubstitutionData["GARMENT"] = garment;



            var recipient = new Recipient
            {
                Address = new Address { Email = to }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ApiKey);
            client.CustomSettings.SendingMode = SendingModes.Sync;

            try {
                Task<SendTransmissionResponse> task = client.Transmissions.Send(transmission);

                if (task.Result == null) {
                    Console.WriteLine("Result from Transmission.Send was null");
                }



                Console.WriteLine(
                    String.Format(
                        "ID: {0}\nAccepted recipients: {1}\nRejected recipients: {2}\nReason: {3}\n Status code: {4}",
                        task.Result.Id, task.Result.TotalAcceptedRecipients, task.Result.TotalRejectedRecipients,
                        task.Result.ReasonPhrase, task.Result.StatusCode));
            }
            catch (Exception ex) {
                Console.WriteLine("Error while sending email\n" + ex);
            }
        }

    }
}
