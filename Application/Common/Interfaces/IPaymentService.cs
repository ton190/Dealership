using Stripe.Checkout;

namespace Application.Interfaces;

public interface IPaymentService
{
    public Task<Session?> CreateSession(string email);
    public Task<Session?> GetSession(string sessionId);
}
