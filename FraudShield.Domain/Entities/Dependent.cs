namespace FraudShield.Domain.Entities;

public class Dependent
{
    public Guid Id { get; set; }
    public Guid GuardianId { get; set; }
    public string Name { get; private set; }

    public decimal MonthlyLimit { get; private set; }
    public decimal PerTransactionLimit { get; private set; }

    public bool TransferEnabled { get; private set; }
    public Boolean IsBlocked { get; private set; }
    private Dependent() { }
    public Dependent(
        Guid guardianId,
        string name,
        decimal monthlyLimit,
        decimal perTransactionLimit,
        bool transfersEnabled)
    {
        if (guardianId == Guid.Empty)
            throw new ArgumentException("GuardianId cannot be empty.", nameof(guardianId));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        if (monthlyLimit <= 0)
            throw new ArgumentOutOfRangeException(nameof(monthlyLimit), "Monthly limit cannot be negative.");
        if (perTransactionLimit <= 0)
            throw new ArgumentOutOfRangeException(nameof(perTransactionLimit), "Per transaction limit cannot be negative.");
        if (perTransactionLimit > monthlyLimit)
            throw new ArgumentException("Per transaction limit cannot exceed monthly limit.", nameof(perTransactionLimit));

        Id = Guid.NewGuid();
        GuardianId = guardianId;
        Name = name.Trim();
        MonthlyLimit = monthlyLimit;
        PerTransactionLimit = perTransactionLimit;
        TransferEnabled = transfersEnabled;
        IsBlocked = false;
    }

    public void UpdateLimits(decimal monthlyLimit, decimal perTransactionLimit)
    {
        if (monthlyLimit <= 0)
            throw new ArgumentOutOfRangeException(nameof(monthlyLimit), "Monthly limit cannot be negative.");
        if (perTransactionLimit <= 0)
            throw new ArgumentOutOfRangeException(nameof(perTransactionLimit), "Per transaction limit cannot be negative.");
        if (perTransactionLimit > monthlyLimit)
            throw new ArgumentException("Per transaction limit cannot exceed monthly limit.", nameof(perTransactionLimit));

        MonthlyLimit = monthlyLimit;
        PerTransactionLimit = perTransactionLimit;
    }
    public void Block() => IsBlocked = true;
    public void UNblock() => IsBlocked = false;
    public void SetTransfer(bool enabled) => TransferEnabled = enabled;
}
