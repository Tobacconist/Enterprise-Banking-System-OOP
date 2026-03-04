#  Enterprise Banking System 

> A C# Console Application designed to demonstrate core Object-Oriented Programming (OOP) principles and clean code architecture.

This project simulates a backend banking engine where different account types (Savings and Checking) are managed through a unified interface. It was built specifically to showcase my understanding of software design patterns and the SOLID principles in a real-world scenario.

##  OOP Principles Applied

* **Abstraction:** Utilized `abstract` classes and interfaces (`ILogger`) to decouple logic and define essential banking behaviors without exposing unnecessary details.
* **Encapsulation:** Protected sensitive data like `Balance` using private fields and controlled accessors, ensuring data integrity across the system.
* **Inheritance:** Implemented a hierarchical structure where `SavingsAccount` and `CheckingAccount` inherit shared logic from a base `BankAccount` class, reducing code duplication.
* **Polymorphism:** Overrode base methods to implement account-specific behaviors, such as calculating interest for savings or managing overdraft limits for checking accounts.

##  Key Features

* **Dynamic Account Management:** Supports multiple account types with unique business rules.
* **Dependency Injection:** Implemented an `ILogger` interface to allow for flexible logging (e.g., to Console or potentially to a file/database) without changing core logic.
* **Robust Validation:** Includes comprehensive error handling for invalid transactions like overdrafts or negative deposits.
* **Transaction Logging:** Real-time logging of all financial activities to ensure transparency.

##  Tech Stack

* **Language:** C#
* **Framework:** .NET Console
* **Concepts:** OOP, SOLID Principles, Dependency Injection

##  How to Run

1. Clone this repository.
2. Open the solution file in **Visual Studio**.
3. Press `F5` to run the console application.
4. Observe the transaction logs in the console to see the OOP logic in action.

--
