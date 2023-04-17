namespace CleanArchitecture.PaymentData
{
    public record StripeCustomer(
         string Name,
         string Email,
         string CustomerId);
}