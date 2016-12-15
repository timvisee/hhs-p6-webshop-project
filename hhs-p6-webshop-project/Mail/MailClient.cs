using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SparkPostDotNet;
using SparkPostDotNet.Transmissions;

namespace hhs_p6_webshop_project.Mail
{
    public class MailClient {
        public static SparkPostClient Client =
            new SparkPostClient(null);

        public static void SendAppointmentEmail(string name, string to, DateTime datetime,
           string clothing)
        {

            var transmission = new Transmission();


            //transmission.CampaignId = "honeymoon-shop-appointment-created";

            //transmission.SubstitutionData["FNAME"] = name;
            //transmission.SubstitutionData["DATE_AND_TIME"] = datetime.ToString("dd MMMM yyyy H:mm");
            //transmission.SubstitutionData["CLOTHING_NAME"] = clothing;

            transmission.Content.From.EMail = "honeymoonshop@rick-soft.com";
            transmission.Content.From.Name = "Honeymoon Shop";
            transmission.Content.Subject = "Uw afspraak bij Honeymoon Shop";
            transmission.Content.Html = ($"Beste {name},\n\nJe pasafspraak bij de Honeymoon Shop is successvol gemaakt.\n\nWe verwachten je op {datetime.ToString("dd MMMM yyyy H:mm")}.\n\nTot snel!\n\nMet vriendelijke groet,\nHoneymoon Shop\n\nKorte Hoogstraat 4\n3011GL Rotterdam").Replace("\n", "<br>");

            var recipient = new Recipient();

            recipient.Address.EMail = to;
            transmission.Recipients.Add(recipient);

            var result = Client.CreateTransmission(transmission);

            result.Wait();

           Console.WriteLine("Sent!");

            



            //Console.WriteLine(String.Format("ID: {0}\nAccepted recipients: {1}\nRejected recipients: {2}\nReason: {3}\n Status code: {4}", task.Result.Id, transmission.Num.TotalAcceptedRecipients, task.Result.TotalRejectedRecipients, task.Result.ReasonPhrase, task.Result.StatusCode));


        }
    }
}
