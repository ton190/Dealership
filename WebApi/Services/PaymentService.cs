using Application.Interfaces;
using Microsoft.Extensions.Options;
using Shared;
using Stripe;
using Stripe.Checkout;

namespace WebApi.Services;

public class PaymentService : IPaymentService
{
    private string _service_id;
    private readonly IHttpContextAccessor _http;
    public PaymentService(
        IOptions<PaymentServiceSettings> settings, IHttpContextAccessor http)
    {
        StripeConfiguration.ApiKey = settings.Value.Key;
        _service_id = settings.Value.Service_id;
        _http = http;
    }

    public async Task<Price> GetPrice()
    {
        var service = new PriceService();
        return await service.GetAsync(_service_id);
    }

    public async Task<Session?> CreateSession(
        string email)
    {
        var baseUrl = "http://" + _http.HttpContext!.Request.Host.Value;
        string successUrl = baseUrl + UIRoutes.Orders.SearchResult;
        string cancelUrl = baseUrl + UIRoutes.Orders.Search;

        var options = new SessionCreateOptions
        {
            ExpiresAt = DateTime.Now.AddHours(1),
            CustomerEmail = email,
            SuccessUrl = successUrl + "?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = cancelUrl,
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions{Price=_service_id, Quantity=1}
            },
            Mode = "payment"
        };

        var service = new SessionService();
        {
            return await service.CreateAsync(options);
        }
    }

    public async Task<Session?> GetSession(string sessionId)
    {
        var service = new SessionService();
        try
        {
            return await service.GetAsync(sessionId);
        }
        catch (Exception)
        {
            return null;
        }
    }
}

public class PaymentServiceSettings
{
    public string Key { get; set; } = string.Empty;
    public string Service_id { get; set; } = string.Empty;
}
