using MailKit.Net.Smtp;
using MimeKit;
using WebUI.Repositories.Abstract;

namespace WebUI.Repositories.Concrete
{
    public class MailRepository : IMailRepository
    {
        public void MailGonder(string ad, string email, string mailIcerik, string mailBaslik)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("gonderenAd", "gonderenMail");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress(ad, email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mailIcerik;

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = mailBaslik;

            SmtpClient client = new SmtpClient();
            client.Connect("gonderenPort", 587, false);
            client.Authenticate("gonderenMail", "gonderenSifre");
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}
