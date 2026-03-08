namespace Services.Auth
{
    public interface IHashService
    {
        string Hash(string text);
        bool Verify(string text, string hash);
    }

    public class HashService : IHashService
    {
        public string Hash(string text) => BCrypt.Net.BCrypt.HashPassword(text);
        public bool Verify(string text, string hash) => BCrypt.Net.BCrypt.Verify(text, hash);
    }
}
