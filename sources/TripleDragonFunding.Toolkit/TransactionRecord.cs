namespace DustInTheWind.TripleDragonFunding.Toolkit;

/// <summary>
/// Represents a transaction.
/// </summary>
public record class TransactionRecord
{
	public DateTime Date { get; set; }

	public string Loan { get; set; }

	public TransactionType Type { get; set; }

	public decimal Amount { get; set; }
}