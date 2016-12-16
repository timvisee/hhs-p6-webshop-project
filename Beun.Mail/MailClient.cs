using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SparkPost;

namespace Beun.Mail
{
    public class MailClient
    {

        public static void SendAppointmentEmail(string name, string to, DateTime date, string garment) {
            var transmission = new Transmission();


            transmission.Content.TemplateId = "honeymoon-shop-appointment-created";

            transmission.SubstitutionData["FNAME"] = name;
            transmission.SubstitutionData["DATE_AND_TIME"] = $"{date:dddd d MMMM} om {date:H:mm}";
            transmission.SubstitutionData["CURRENT_YEAR"] = DateTime.Now.Year;
            


            var recipient = new Recipient
            {
                Address = new Address { Email = to }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client("195317d791a943fe7052dbd95223bda5bea15b0e");
            client.CustomSettings.SendingMode = SendingModes.Sync;

            Task<SendTransmissionResponse> task = client.Transmissions.Send(transmission);

            if (task.Result == null)
            {
                Console.WriteLine("Result from Transmission.Send was null");
            }



            Console.WriteLine(String.Format("ID: {0}\nAccepted recipients: {1}\nRejected recipients: {2}\nReason: {3}\n Status code: {4}", task.Result.Id, task.Result.TotalAcceptedRecipients, task.Result.TotalRejectedRecipients, task.Result.ReasonPhrase, task.Result.StatusCode));

        }

    }
}
