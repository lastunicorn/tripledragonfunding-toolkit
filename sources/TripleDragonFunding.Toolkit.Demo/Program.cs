using System.Globalization;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.TripleDragonFunding.Toolkit.Demo;

internal static class Program
{
	public static async Task Main(string[] args)
	{
		const string fileName = "statement.csv";

		try
		{
			StatementDocument document = await StatementDocument.LoadFromFileAsync(fileName);
			Display(document);
		}
		catch (DocumentLoadException ex)
		{
			await Console.Error.WriteLineAsync($"Failed to read '{fileName}': {ex}");
			Environment.ExitCode = 1;
		}
		catch (Exception ex)
		{
			await Console.Error.WriteLineAsync($"Unexpected error: {ex}");
			Environment.ExitCode = 1;
		}
	}

	private static void Display(StatementDocument document)
	{
		DataGrid dataGrid = new()
		{
			Title = $"Transactions ({document.Currency})",
			BorderTemplate = BorderTemplate.PlusMinusBorderTemplate,
			Footer = $"Count: {document.Count}"
		};

		dataGrid.Columns.Add("Date");
		dataGrid.Columns.Add("Loan");
		dataGrid.Columns.Add("Type");
		dataGrid.Columns.Add("Amount", HorizontalAlignment.Right);

		foreach (TransactionRecord transaction in document)
		{
			dataGrid.Rows.Add(
				transaction.Date.ToString("yyyy-MM-dd"),
				transaction.Loan,
				transaction.Type,
				transaction.Amount.ToString(CultureInfo.CurrentCulture));
		}

		dataGrid.Display();
	}
}