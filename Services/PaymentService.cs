using System;
using System.Threading.Tasks;
using MasrafTakipApi.Interfaces.Service;

namespace MasrafTakipApi.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> MakePaymentAsync(Guid userId, decimal amount)
        {
            Console.WriteLine($"[PaymentService] User {userId} paid {amount:C} successfully.");
            await Task.Delay(100); // Simüle edilmiş gecikme
            return true;
        }
    }
}
