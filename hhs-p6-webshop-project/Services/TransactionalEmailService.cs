using System;
using System.Threading.Tasks;
using hhs_p6_webshop_project.App.Config;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Services.Abstracts;
using hhs_p6_webshop_project.Services.Containers;
using Microsoft.Extensions.Options;
using SparkPost;

namespace hhs_p6_webshop_project.Services
{
    public class TransactionalEmailService : ITransactionalEmailService
    {
        private IOptions<SecureAppConfig> SecretConfig { get; }

        public TransactionalEmailService(IOptions<SecureAppConfig> secretConfig)
        {
            SecretConfig = secretConfig;
        }

        public Task<SendTransmissionResponse> SendContactEmail(ContactModels container)
        {
            //Create transmission object, with proper variables and container context
            var transmission = new Transmission
            {
                Content = {TemplateId = "honeymoon-shop-contact"},
                SubstitutionData =
                {
                    ["NAME"] = container.Name,
                    ["TEL_NR"] = container.Phone,
                    ["MESSAGE"] = container.Comment,
                    ["REF"] = container.Reference
                }
            };
            
            var recipient = new Recipient
            {
                Address = new Address { Email = container.Email }
            };

            //Set recipient
            transmission.Recipients.Add(recipient);

            var client = new Client(SecretConfig.Value.SparkpostApiKey);
            client.CustomSettings.SendingMode = SendingModes.Async;

            //Return the send task
            return client.Transmissions.Send(transmission);
        }

        public Task<SendTransmissionResponse> SendAppointmentEmail(AppointmentMessageContainer container)
        {
            //Create transmission object, with proper variables and container context
            var transmission = new Transmission
            {
                Content = {TemplateId = "honeymoon-shop-appointment-created"},
                SubstitutionData =
                {
                    ["FNAME"] = container.Name,
                    ["DATE_AND_TIME"] = $"{container.Date:dddd d MMMM} om {container.Date:H:mm}",
                    ["CURRENT_YEAR"] = DateTime.Now.Year,
                    ["GARMENT"] = container.Garment
                }
            };
            
            var recipient = new Recipient
            {
                Address = new Address { Email = container.Recipient }
            };

            //Set recipient
            transmission.Recipients.Add(recipient);

            var client = new Client(SecretConfig.Value.SparkpostApiKey);
            client.CustomSettings.SendingMode = SendingModes.Async;

            //Return the send task
            return client.Transmissions.Send(transmission);
        }
    }
}
