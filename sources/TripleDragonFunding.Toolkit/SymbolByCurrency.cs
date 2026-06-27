namespace DustInTheWind.TripleDragonFunding.Toolkit;

public static class SymbolByCurrency
{
	public static IReadOnlyDictionary<Currency, string> Collection { get; } = new Dictionary<Currency, string>
	{
		[Currency.EUR] = "€",
		[Currency.USD] = "$",
		[Currency.GBP] = "£"
	};

	public static string GetSymbol(Currency currency)
	{
		return Collection.GetValueOrDefault(currency);
	}
}