using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DustInTheWind.TripleDragonFunding.Toolkit.Csv;

internal class AmountConverter : DefaultTypeConverter
{
	public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
	{
		if (string.IsNullOrWhiteSpace(text))
			return 0m;

		// Remove thousands separator (regular space and non-breaking space U+00A0)
		string normalized = text
			.Replace(" ", "")
			.Replace(" ", "");

		// Remove explicit '+' sign that decimal.Parse does not accept
		if (normalized.StartsWith('+'))
			normalized = normalized[1..];

		return decimal.Parse(normalized, CultureInfo.InvariantCulture);
	}
}