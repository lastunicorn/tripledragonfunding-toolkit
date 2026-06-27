using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper;
using CsvHelper.Configuration;

namespace DustInTheWind.TripleDragonFunding.Toolkit.Csv;

internal class CsvStatementDocument
{
	private readonly CsvReader csvReader;

	public CsvStatementDocument(TextReader textReader)
	{
		if (textReader == null) throw new ArgumentNullException(nameof(textReader));

		CsvConfiguration csvConfiguration = new(CultureInfo.InvariantCulture)
		{
			HasHeaderRecord = true,
			IgnoreBlankLines = true,
			TrimOptions = TrimOptions.Trim,
			PrepareHeaderForMatch = args => args.Header.Trim()
		};

		csvReader = new CsvReader(textReader, csvConfiguration);
		csvReader.Context.RegisterClassMap(new TransactionRecordMap());
	}

	public async IAsyncEnumerable<TransactionRecord> ReadTransactions([EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		while (await csvReader.ReadAsync())
		{
			cancellationToken.ThrowIfCancellationRequested();
			yield return csvReader.GetRecord<TransactionRecord>();
		}
	}
}