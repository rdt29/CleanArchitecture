namespace CleanArchitecture.PaymentData
{
    public record AddStripeCustomer(
         string Email,
         string Name,
         AddStripeCard CreditCard);
}