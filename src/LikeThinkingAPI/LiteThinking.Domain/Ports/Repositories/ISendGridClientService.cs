namespace LiteThinking.Domain.Ports.Repositories;

public interface ISendGridClientService
{
    Task SendEmail(string sender, string subject, string receiver, byte[] file);
}
