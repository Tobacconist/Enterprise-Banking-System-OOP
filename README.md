# Enterprise Banking System

A small console application that models bank accounts with plain C# object-oriented design. It exists to show inheritance, polymorphism and defensive input handling without any framework around it. The user-facing output is in Turkish.

## What it does

`Program.cs` creates two accounts, runs the same sequence of operations on both through the `BankAccount` base type, and then triggers a deliberate failure to show how an invalid operation is reported.

- `SavingsAccount` earns interest: `CalculateInterest` deposits `balance * rate` back into the account.
- `CheckingAccount` has an overdraft limit: `Withdraw` is allowed as long as the amount does not exceed `balance + overdraft limit`. It does not earn interest.
- `BankAccount` holds the shared state (account number, owner, balance) and the common `Deposit` / `Withdraw` rules.

## Design notes

- **Validation lives in constructors and base-class helpers.** Account number, owner, initial balance, interest rate and overdraft limit are checked on creation, so an account can never exist in an invalid state. Amount checks for deposits and withdrawals share a single helper in `BankAccount`.
- **Balance is only writable from inside the hierarchy.** `Balance` has a `protected set`, so callers have to go through `Deposit` and `Withdraw`, which enforce the rules and write the log line.
- **`decimal` for money.** No floating point rounding surprises.
- **Currency formatting is pinned to `tr-TR`.** Amounts are formatted the same way on every machine instead of following the current OS culture. The console output encoding is set to UTF-8 so the lira sign renders correctly.
- **Zero interest is not an error.** An empty savings account logs that no interest was earned instead of failing on a zero-amount deposit.
- **Expected vs. unexpected errors.** The demo catches `InvalidOperationException` (insufficient funds, overdraft exceeded) separately. Anything else falls through to an outer handler that prints the full exception including the stack trace.
- **Logging goes through `ILogger`.** `ConsoleLogger` is the only implementation and is injected through the constructor. This is the only abstraction in the project, kept because the accounts should not depend on `Console` directly.

## Project layout

```
ConsoleApp1/
  Program.cs           entry point and demo scenario
  BankAccount.cs       abstract base class, shared rules
  SavingsAccount.cs    interest-bearing account
  CheckingAccount.cs   account with an overdraft limit
  ILogger.cs           logging interface
  ConsoleLogger.cs     console implementation
```

## Running locally

Requires the .NET 10 SDK.

```
dotnet run --project ConsoleApp1
```

The program prints the log lines for each operation and waits for Enter before closing.
