namespace DustInTheWind.TripleDragonFunding.Toolkit;

/// <summary>
/// Value object that represents a loan id.
/// The underlying value is the string used in the Loan column of the CSV statement file. 
/// </summary>
public sealed record class Loan : IComparable<Loan>, IComparable<string>
{
	private readonly string rawValue;

	public int Id { get; }

	public bool IsEmpty => rawValue.Length == 0;

	public bool HasValidFormat { get; }

	public Loan(string value)
	{
		rawValue = value ?? string.Empty;

		if (rawValue.StartsWith("Loan ", StringComparison.OrdinalIgnoreCase))
		{
			bool success = int.TryParse(rawValue[5..], out int id);

			if (success)
			{
				Id = id;
				HasValidFormat = true;
			}
		}
	}

	public override string ToString()
	{
		return rawValue;
	}

	public static implicit operator Loan(string value)
	{
		return value == null
			? null
			: new Loan(value);
	}

	public static implicit operator string(Loan loan)
	{
		return loan?.rawValue;
	}

	public int CompareTo(Loan other)
	{
		if (ReferenceEquals(this, other)) return 0;
		if (other is null) return 1;
		return string.Compare(rawValue, other.rawValue, StringComparison.Ordinal);
	}

	public int CompareTo(string other)
	{
		if (ReferenceEquals(rawValue, other)) return 0;
		if (other is null) return 1;
		return string.Compare(rawValue, other, StringComparison.Ordinal);
	}
}