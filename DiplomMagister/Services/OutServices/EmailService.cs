using DiplomMagister.Controllers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DiplomMagister.Services.OutServices
{
    public class EmailService: IEmailService
    {
        private readonly EmailOptions _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailOptions> configuration, ILogger<EmailService> logger)
        {
            this._configuration = configuration.Value;
            this._logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string login = _configuration.AppMail;
            string password = _configuration.AppMailPassword;

            using (var emailMessage = new MimeMessage())
            {
                emailMessage.From.Add(new MailboxAddress("Тесты­³", $"{login}@yandex.ru"));
                emailMessage.To.Add(new MailboxAddress("", "basliney@bk.ru"));
                emailMessage.Subject = subject;
                //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                //{
                //    Text = message
                //};
                emailMessage.Body = new TextPart("Plain")
                {
                    Text = Guid.NewGuid().ToString()
                };

                if (await GoogleSMTPSend(emailMessage, $"{login}@gmail.com", password) == false)
                {
                    await YandexSMTPSend(emailMessage, $"{login}@yandex.ru", password);
                }
            }
        }

        private async Task YandexSMTPSend(MimeMessage emailMessage, string login, string password)
        {
            var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync($"{login}", password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, e, "Во время отправки письма c помощью яндекса произошла ошибка");
            }
            finally
            {
                client.Dispose();
            }
        }

        private async Task<bool> GoogleSMTPSend(MimeMessage emailMessage, string login, string password)
        {
            bool success = false;
            var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("pop.gmail.com", 25, false);
                await client.AuthenticateAsync($"{login}", password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
                success = true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, e, "Во время отправки письма с помощью гугла произошла ошибка");
            }
            finally
            {
                client.Dispose();
            }
            return success;
        }
    }
}
