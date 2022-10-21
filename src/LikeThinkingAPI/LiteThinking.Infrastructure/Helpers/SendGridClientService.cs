using LiteThinking.Domain.Ports.Repositories;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LiteThinking.Infrastructure.Helpers;

public class SendGridClientService : ISendGridClientService
{
    private readonly IConfiguration _configuration;
    private readonly SendGridClient _sendGridClient;
    
    public SendGridClientService(IConfiguration configuration)
    {
        _configuration = configuration;
        _sendGridClient = new SendGridClient(_configuration.GetSection("SendGridApiKey").Value);
    }

    public async Task SendEmail(string sender, string subject, string receiver, byte[] file) 
    {
        var from = new EmailAddress(sender, "Inventory");

        var to = new EmailAddress(receiver, "Jhaider Betancur");

        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", "<strong>Correo enviado con inventario adjunto</strong>");

        msg.AddAttachment("Inventory.pdf", Convert.ToBase64String(file), "application/pdf");

        await _sendGridClient.SendEmailAsync(msg);
    }
}
