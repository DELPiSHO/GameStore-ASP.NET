using System.Net;
using System.Net.Mail;
using System.Text;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Domain.Concrete
{
    public class EmailSettings
    {
        static ShippingDetails shippingDetails = new ShippingDetails();
        public string MailToAddress =  "fanatik1798@gmail.com";
        public string MailFromAddress = shippingDetails.Email;
        public bool UseSsl = true;
        public string Username = "fanatik1798@gmail.com";
        public string Password = "#####";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\game_store_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Nowe zamówienie")
                    .AppendLine("---")
                    .AppendLine("Gry:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Game.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (Suma: {2:c}",
                        line.Quantity, line.Game.Name, subtotal);
                }

                body.AppendFormat("Suma: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Dostawa:")
                    .AppendLine("Imię: " + shippingInfo.firstName)
                    .AppendLine("Nazwisko: "+shippingInfo.lastName)
                    .AppendLine("Email: "+shippingInfo.Email)
                    .AppendLine("Państwo: "+shippingInfo.Country)
                    .AppendLine("Miasto: "+shippingInfo.City)
                    .AppendLine("Ulica: " + shippingInfo.Street)
                    .AppendLine("Numer domu: "+shippingInfo.NumerDomu)
                    .AppendLine("Kod pocztowy: "+shippingInfo.ZipCode)
                    .AppendLine("Dodatki: ")
                    .AppendLine("------------------------------")
                    .AppendFormat("Świąteczne upakowanie: {0}",
                        shippingInfo.GiftWrap ? "Tak" : "Nie");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress = shippingInfo.Email,	        // Od kogo
                                       emailSettings.MailToAddress = "fanatik1798@mail.ru",		        // Komu
                                       "Nowe zamówienie zostało wysłane!",		                        // Temat
                                       body.ToString()); 				                                // Body

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}