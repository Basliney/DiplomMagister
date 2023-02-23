namespace DiplomMagister.Classes
{
    public class Token
    {
        public int Id { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime Expires { get; set; }
        public string EncodedJwt { get; set; }
        public string Scope { get; set; }
    }
}
