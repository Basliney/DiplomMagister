using System.Diagnostics;

namespace DiplomMagister.Classes.Exceptions
{
    public class RegexException : Exception
    {
        public RegexException()
        {
            Console.Error.WriteLine($"{base.StackTrace}");
            Debug.WriteLine($"{base.StackTrace}");
        }

        public RegexException(string? message) : base(message)
        {
            Console.Error.WriteLine($"{base.StackTrace} {message}");
            Debug.WriteLine($"{base.StackTrace} {message}");
        }
    }
}
