using System.Transactions;

namespace FraudShield.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public Guid DependentId { get; private set; }
    public Guid MerchantId { get; private set; }

    public decimal Amount { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public TransactionType Type { get; private set; }
    public TransactionStatus Status { get; private set; }

    public string? BlockReason { get; private set; }
    private Transaction() { }
    public Transaction(Guid dependentId, Guid merchantId, decimal amount, TransactionType type)
    {
        if (dependentId == Guid.Empty) throw new ArgumentException("DependentId is required.");
        if (merchantId == Guid.Empty) throw new ArgumentException("MerchantId is required.");
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

        Id = Guid.NewGuid();
        DependentId = dependentId;
        MerchantId = merchantId;
        Amount = amount;
        CreatedAtUtc = DateTime.UtcNow;
        Type = type;
        Status = TransactionStatus.Approved;
    }
    public void Block(string reason)
    {
        if (Status == TransactionStatus.Blocked)
            throw new InvalidOperationException("Transaction is already blocked.");
        Status = TransactionStatus.Blocked;
        BlockReason = reason.Trim();
    }
     public void Approve()
    {
        if (Status == TransactionStatus.Approved)
            throw new InvalidOperationException("Transaction is already approved.");
        Status = TransactionStatus.Approved;
        BlockReason = null;
    }
}
