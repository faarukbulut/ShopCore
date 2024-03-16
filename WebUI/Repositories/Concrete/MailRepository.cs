using MailKit.Net.Smtp;
using MimeKit;
using WebUI.Repositories.Abstract;

namespace WebUI.Repositories.Concrete
{
    public class MailRepository : IMailRepository
    {
        public void MailDogrulamaMailGonder(string ad, string email, string tokenLink)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("GonderenAd", "GonderenMail");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress(ad, email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"Hesabınızı doğrulamak için <a href='{tokenLink}'>buraya tıklayınız.</a>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Hesap Doğrulama";

            SmtpClient client = new SmtpClient();
            client.Connect("GonderenMailPort", 587, false);
            client.Authenticate("GonderenMail", "GonderenSifre");
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}
