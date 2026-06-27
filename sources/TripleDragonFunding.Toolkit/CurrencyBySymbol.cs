namespace DustInTheWind.TripleDragonFunding.Toolkit;

public static class CurrencyBySymbol
{
	public static IReadOnlyDictionary<string, Currency> Collection { get; } = new Dictionary<string, Currency>
	{
		["€"] = Currency.EUR,
		["$"] = Currency.USD,
		["£"] = Currency.GBP
	};

	public static Currency GetCurrency(string symbol)
	{
		return Collection.GetValueOrDefault(symbol);
	}
}