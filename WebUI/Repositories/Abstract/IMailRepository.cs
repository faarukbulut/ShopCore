namespace WebUI.Repositories.Abstract
{
    public interface IMailRepository
    {
        void MailDogrulamaMailGonder(string ad, string email, string tokenLink);
    }
}
