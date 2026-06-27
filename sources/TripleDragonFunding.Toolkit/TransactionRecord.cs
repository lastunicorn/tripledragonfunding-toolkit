namespace DustInTheWind.TripleDragonFunding.Toolkit;

/// <summary>
/// Represents a transaction.
/// </summary>
public record class TransactionRecord
{
	public DateOnly Date { get; set; }

	public Loan Loan { get; set; }

	public TransactionType Type { get; set; }

	public decimal Amount { get; set; }
}