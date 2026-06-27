# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
# Build the solution
dotnet build TripleDragonFunding.slnx

# Run the demo (reads statement.csv from its output directory)
dotnet run --project sources/TripleDragonFunding.Demo

# Pack the library for NuGet
dotnet pack sources/TripleDragonFunding/TripleDragonFunding.csproj -c Release -o ./artifacts
```

No test projects exist yet. NuGet publish is triggered automatically by pushing a `v*.*.*` tag to GitHub.

## Architecture

This is a single-purpose .NET 8.0 library (`DustInTheWind.TripleDragonFunding`) that parses CSV statement exports from the Triple Dragon Funding loan platform.

**Public surface** (namespace `DustInTheWind.TripleDragonFunding`):
- `StatementDocument` — extends `Collection<TransactionRecord>`; entry point via static `LoadFromFileAsync` / `LoadAsync` overloads (file path, `string`, `Stream`, `FileInfo`, `StreamReader`, `TextReader`). All loading funnels through `LoadInternalAsync`.
- `TransactionRecord` — `record class` with `Date`, `Loan`, `Type`, `Amount` properties.
- `DocumentLoadException` — wraps any parsing error; re-thrown so callers receive a stable exception type.

**Internal CSV layer** (`DustInTheWind.TripleDragonFunding.Csv`):
- `CsvStatementDocument` — wraps `CsvHelper.CsvReader`; exposes `ReadTransactions()` as `IAsyncEnumerable<TransactionRecord>`.
- `TransactionRecordMap` — `CsvHelper` class map; maps CSV column names (including `"Amount (€)"`) to `TransactionRecord` properties.

The demo project (`TripleDragonFunding.Demo`, net10.0) loads `statement.csv` and renders it as a table via `ConsoleTools`.

## Code Conventions

- No `var` — always use the explicit type.
- LINQ lambda parameter name: `x`.
- Object instantiation: `new()` target-typed style.
- Object initializers with multiple properties: one property per line.
- No curly brackets for single-line `if` / `for` / `using` bodies.
- No underscore prefix on private fields.

## XML Documentation

Only add `<summary>` / XML doc to public types exposed by the NuGet package (`TripleDragonFunding` project). Types used only internally need no XML doc.

## NuGet Packaging

`Directory.Build.props` sets shared metadata (company, description, license, readme, changelog). The version defaults to `0.0.0.0`; CI overrides it at build time from the git tag. To publish, push a tag matching `vMAJOR.MINOR.PATCH` — the `publish-nuget.yml` workflow handles pack + push to nuget.org automatically.
