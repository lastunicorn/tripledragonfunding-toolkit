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

	public async Task<Currency> ReadCurrency()
	{
		await csvReader.ReadAsync();
		csvReader.ReadHeader();
		
		return ExtractCurrency();
	}

	private Currency ExtractCurrency()
	{
		string[] headers = csvReader.HeaderRecord;
		if (headers == null || headers.Length < 4)
			return null;

		string header = headers[3];

		if (header == null)
			return null;

		int openParen = header.LastIndexOf('(');
		int closeParen = header.LastIndexOf(')');

		if (openParen < 0 || closeParen <= openParen)
			return null;

		string symbol = header[(openParen + 1)..closeParen].Trim();

		return CurrencyBySymbol.GetCurrency(symbol);
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