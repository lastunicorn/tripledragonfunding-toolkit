using CsvHelper.Configuration;

namespace DustInTheWind.TripleDragonFunding.Toolkit.Csv;

internal sealed class TransactionRecordMap : ClassMap<TransactionRecord>
{
	public TransactionRecordMap()
	{
		Map(x => x.Date)
			.Name("Date");

		Map(x => x.Loan)
			.Name("Loan")
			.TypeConverter<LoanTypeConverter>();

		Map(x => x.Type)
			.Name("Type")
			.TypeConverter<TransactionTypeTypeConverter>();

		Map(x => x.Amount)
			.Name("Amount (€)")
			.TypeConverter<AmountConverter>();
	}
}