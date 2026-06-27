# Triple Dragon Funding Toolkit

[![GitHub Repo](https://img.shields.io/badge/github-repo-blue?logo=github)](https://github.com/lastunicorn/tripledragonfunding-toolkit) [![GitHub Build](https://img.shields.io/github/actions/workflow/status/lastunicorn/tripledragonfunding-toolkit/build-master.yml?logo=github)](https://github.com/lastunicorn/tripledragonfunding-toolkit/actions/workflows/build-master.yml) [![NuGet Version](https://img.shields.io/nuget/v/DustInTheWind.TripleDragonFunding.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.TripleDragonFunding.Toolkit) [![NuGet Downloads](https://img.shields.io/nuget/dt/DustInTheWind.TripleDragonFunding.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.TripleDragonFunding.Toolkit)

`Triple Dragon Funding Toolkit` is a .NET library that helps working with files exported from Triple Dragon Funding.

Triple Dragon Funding is a loan investment platform.

- https://www.tdfunding.eu/en

## Installation

Package Manager:

```powershell
Install-Package DustInTheWind.TripleDragonFunding.Toolkit
```

.NET CLI:

```bash
dotnet add package DustInTheWind.TripleDragonFunding.Toolkit
```

## Runtime Requirements

- Library target framework: `.NET 8.0` (`net8.0`)

## Features

- **Parse Triple Dragon Funding Statement Documents** - Load and parse CSV files exported directly from the Triple Dragon Funding platform

## Quick Start

### a) Export the Statement CSV File

In Triple Dragon Funding web application:

- TBD

You will get a CSV containing transaction rows that can be parsed with this toolkit.

### b) Parse the Exported Document

```csharp
using DustInTheWind.TripleDragonFunding.Toolkit;

StatementDocument statementDocument = await StatementDocument.LoadFromFileAsync("statement.csv");

foreach (TransactionRecord transaction in statementDocument)
{
	...
}
```

## CSV Statement Document

Each row is mapped to a `TransactionRecord` with the following columns:

| CSV Column      | Type     | TransactionRecord Property | Description                                         |
|-----------------|----------|--------------------------|-----------------------------------------------------|
| `Date`          | `DateOnly` | `Date`                   | The date when the transaction occurred.             |
| `Loan` | `Loan` | `Loan`     | A unique identifier for the loan.        |
| `Type`    | `TransactionType` | `Type`            | The type of transaction (e.g., Loan investment, Loan interest). |
| `Amount (€)` | `decimal` | `Amount`          | The transaction amount.                             |

## Demo Project

The repository includes a sample CLI project in `sources/TripleDragonFunding.Toolkit.Demo` that demonstrates:

- reading `statement.csv`
- printing parsed data.

You can use this project as a reference implementation for your own importer/exporter tools.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
