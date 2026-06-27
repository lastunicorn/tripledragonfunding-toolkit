namespace DustInTheWind.TripleDragonFunding.Toolkit.Utils;

public static class StringExtensions
{
	public static Currency GetCurrency(this string symbol)
	{
		return CurrencyBySymbol.GetCurrency(symbol);
	}
}