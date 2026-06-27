namespace DustInTheWind.TripleDragonFunding.Toolkit.Utils;

public static class CurrencyExtensions
{
	public static string GetSymbol(this Currency currency)
	{
		return SymbolByCurrency.GetSymbol(currency);
	}
}