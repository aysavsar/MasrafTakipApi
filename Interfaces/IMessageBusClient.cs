namespace MasrafTakipApi.Interfaces.Service
{
    public interface IMessageBusClient
    {
        void PublishMessage(string exchange, string message);
    }
}
