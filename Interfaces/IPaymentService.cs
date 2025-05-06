namespace MasrafTakipApi.Interfaces.Service
{
    public interface IPaymentService
    {
        Task<bool> MakePaymentAsync(Guid userId, decimal amount);
    }
}
