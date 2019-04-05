namespace MailerWeb.Models.Responses
{
    public class TokenResponse
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public string Token { get; set; }
    }
}