using System.Collections.ObjectModel;
using DustInTheWind.TripleDragonFunding.Toolkit.Csv;

namespace DustInTheWind.TripleDragonFunding.Toolkit;

/// <summary>
/// Represents a Mintos statement document.
/// The data is loaded from a CSV file from file system or from memory, from a CSV string,
/// <see cref="Stream"/>, <see cref="StreamReader"/> or <see cref="TextReader"/>.
/// </summary>
/// <remarks>
/// The expected structure of the CSV file is the one that is exported from the Mintos website (2026).
/// </remarks>
public class StatementDocument : Collection<TransactionRecord>
{
	public static async Task<StatementDocument> LoadFromFileAsync(string filePath, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

		try
		{
			using StreamReader streamReader = File.OpenText(filePath);
			return await LoadInternalAsync(streamReader, cancellationToken);
		}
		catch (DocumentLoadException)
		{
			throw;
		}
		catch (Exception ex)
		{
			throw new DocumentLoadException(ex);
		}
	}

	public static async Task<StatementDocument> LoadAsync(string csv, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(csv);

		try
		{
			using StringReader stringReader = new(csv);
			return await LoadInternalAsync(stringReader, cancellationToken);
		}
		catch (DocumentLoadException)
		{
			throw;
		}
		catch (Exception ex)
		{
			throw new DocumentLoadException(ex);
		}
	}

	public static async Task<StatementDocument> LoadAsync(Stream stream, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(stream);

		try
		{
			using StreamReader streamReader = new(stream);
			return await LoadInternalAsync(streamReader, cancellationToken);
		}
		catch (DocumentLoadException)
		{
			throw;
		}
		catch (Exception ex)
		{
			throw new DocumentLoadException(ex);
		}
	}

	public static async Task<StatementDocument> LoadAsync(FileInfo fileInfo, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(fileInfo);

		try
		{
			using StreamReader streamReader = fileInfo.OpenText();
			return await LoadInternalAsync(streamReader, cancellationToken);
		}
		catch (DocumentLoadException)
		{
			throw;
		}
		catch (Exception ex)
		{
			throw new DocumentLoadException(ex);
		}
	}

	public static Task<StatementDocument> LoadAsync(StreamReader streamReader, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(streamReader);

		return LoadInternalAsync(streamReader, cancellationToken);
	}

	public static Task<StatementDocument> LoadAsync(TextReader textReader, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(textReader);

		return LoadInternalAsync(textReader, cancellationToken);
	}

	private static async Task<StatementDocument> LoadInternalAsync(TextReader textReader, CancellationToken cancellationToken)
	{
		try
		{
			CsvStatementDocument csvStatementDocument = new(textReader);
			StatementDocument statementDocument = [];

			await foreach (TransactionRecord transactionRecord in csvStatementDocument.ReadTransactions(cancellationToken))
				statementDocument.Add(transactionRecord);

			return statementDocument;
		}
		catch (DocumentLoadException)
		{
			throw;
		}
		catch (Exception ex)
		{
			throw new DocumentLoadException(ex);
		}
	}
}