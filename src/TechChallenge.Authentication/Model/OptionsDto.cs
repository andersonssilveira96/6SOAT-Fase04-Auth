namespace TechChallenge.Authentication.Model
{
    public class OptionsDto
    {
        public string Region { get; set; } = string.Empty;
        public string UserPoolId { get; set; } = string.Empty;
        public string UserPoolClientId { get; set; } = string.Empty;
        public string UserDefault { get; set; } = string.Empty;
        public string EmailDefault { get; set; } = string.Empty;
        public string PasswordDefault { get; set; } = string.Empty;
    }
}
