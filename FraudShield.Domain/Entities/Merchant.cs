namespace FraudShield.Domain.Entities;

public class Merchant 
{
    public Guid Id { get; private set; } 
    public string Name { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public MerchantStatus Status { get; private set; }

    private Merchant() { }
    public Merchant(string name, DateTime? createdAtUtc = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        Id = Guid.NewGuid();
        Name = name.Trim();
        CreatedAtUtc = createdAtUtc ?? DateTime.UtcNow;
        Status = MerchantStatus.Normal;
    }

    public void MarkAsSupicious()
    {
        if(Status == MerchantStatus.Suspicious)
            throw new InvalidOperationException("Merchant is already marked as suspicious.");
        
        Status = MerchantStatus.Suspicious;
    }
    public void ConfirmFraud()
    {
        Status = MerchantStatus.BlackListed;
    }
    public void Normalize()
    {
        if(Status == MerchantStatus.Normal)
            throw new InvalidOperationException("Merchant is already in normal status.");
        
        Status = MerchantStatus.Normal;
    }
}
