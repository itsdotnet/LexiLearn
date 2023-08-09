using BCryptNet = BCrypt.Net.BCrypt;

namespace LexiLearn.Service.Exstensions;

public static class StringExtension
{
    public static string Hasher(this string a)
    {
        string hashedPass = BCryptNet.HashPassword(a);

        return hashedPass;
    }
}
