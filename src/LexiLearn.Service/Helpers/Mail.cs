using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace LexiLearn.Service.Helpers;

public class Mail
{
    private SmtpClient _smtp = new SmtpClient();

    public Mail()
    {
        Connect();
    }

    public async Task Connect()
    {
        //connect + login
        try
        {
            await _smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await _smtp.AuthenticateAsync("eiscocyber@gmail.com", "ptjivbzmmwmqiwvv");
        }
        catch
        {
            Console.WriteLine("Connect to email failed.\nCheck your internet or disconnect vpn!\n");
        }
    }

    public int Verfy(string emailAdress)
    {
        MimeMessage mail = new MimeMessage();

        //add sender + add recipient
        mail.From.Add(MailboxAddress.Parse("eiscocyber@gmail.com"));
        mail.To.Add(MailboxAddress.Parse(emailAdress));

        int codeForVerfy = GenerateCode();

        //message for verfy
        mail.Subject = "Verfy Your Account";
        mail.Body = new TextPart()
        {
            Text = "This email from LexiLearn\n" +
                $"Your verfication code is {codeForVerfy}"
        };

        //send message
        _smtp.Send(mail);

        return codeForVerfy;
    }

    private static int GenerateCode()
    {
        //generate 
        Random random = new Random();
        return random.Next(1000, 9999);
    }
}