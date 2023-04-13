namespace DiplomMagister.Services.OutServices
{
    public class EmailOptions
    {
        public const string Position = "EmailService";

        public string AppMail { get; set; } = String.Empty;
        public string AppMailPassword { get; set; } = String.Empty;
    }
}
