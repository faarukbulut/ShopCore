namespace WebUI.Repositories.Abstract
{
    public interface IMailRepository
    {
        void MailGonder(string ad, string email, string mailIcerik, string mailBaslik);
    }
}
