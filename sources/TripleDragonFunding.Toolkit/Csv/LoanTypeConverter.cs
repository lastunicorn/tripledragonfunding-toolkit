using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DustInTheWind.TripleDragonFunding.Toolkit.Csv;

internal sealed class LoanTypeConverter : DefaultTypeConverter
{
	public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
	{
		try
		{
			return new Loan(text);
		}
		catch (ArgumentException ex)
		{
			throw new TypeConverterException(this, memberMapData, text, row.Context, ex.Message, ex);
		}
	}

	public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
	{
		return value is Loan loan
			? loan.ToString()
			: base.ConvertToString(value, row, memberMapData);
	}
}