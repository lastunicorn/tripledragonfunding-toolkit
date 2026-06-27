namespace DustInTheWind.TripleDragonFunding.Toolkit;

/// <summary>
/// Value object that represents a transaction type.
/// The underlying value is the string used in the Type column of the CSV statement file. 
/// </summary>
public sealed record class TransactionType : IComparable<TransactionType>, IComparable<string>
{
	public static readonly TransactionType RegistrationBalance = new("RegistrationBalance");
	public static readonly TransactionType WalletDeposit = new("Wallet deposit");
	public static readonly TransactionType LoanInvestment = new("Loan investment");
	public static readonly TransactionType LoanInterest = new("Loan interest");

	/// <summary>
	/// While <see cref="TransactionType"/> may hold any string value, this collection of known values is provided for convenience.
	/// Take note that the collection may not be exhaustive, as Triple Dragon Funding may introduce new payment types in the future.
	/// </summary>
	public static readonly IReadOnlyCollection<TransactionType> KnownValues =
	[
		RegistrationBalance,
		WalletDeposit,
		LoanInvestment,
		LoanInterest
	];

	public string Value { get; }

	public TransactionType(string value)
	{
		Value = value ?? throw new ArgumentNullException(nameof(value));
	}

	public override string ToString()
	{
		return Value;
	}

	public static implicit operator TransactionType(string value)
	{
		return value == null
			? null
			: new TransactionType(value);
	}

	public static implicit operator string(TransactionType transactionType)
	{
		return transactionType?.Value;
	}

	public int CompareTo(TransactionType other)
	{
		if (ReferenceEquals(this, other)) return 0;
		if (other is null) return 1;
		return string.Compare(Value, other.Value, StringComparison.Ordinal);
	}

	public int CompareTo(string other)
	{
		if (ReferenceEquals(Value, other)) return 0;
		if (other is null) return 1;
		return string.Compare(Value, other, StringComparison.Ordinal);
	}
}