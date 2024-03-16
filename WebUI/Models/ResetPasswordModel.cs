namespace WebUI.Models
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
