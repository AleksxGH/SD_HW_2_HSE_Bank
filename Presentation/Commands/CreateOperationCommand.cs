using Application.Facades.Interfaces;
using Spectre.Console;
using Presentation.Commands.Interfaces;
using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Presentation.Commands
{
    public class CreateOperationCommand : ICommand
    {
        private readonly IOperationsFacade _facade;
        public string Name => "Создать новую банковскую операцию";

        public CreateOperationCommand(IOperationsFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {

            OperationType type;

            while (true)
            {
                AnsiConsole.Markup("[white]Введите тип опреации (income/expense):[/] ");
                var input = Console.ReadLine()?.Trim();

                if (string.Equals(input, "income", StringComparison.OrdinalIgnoreCase))
                {
                    type = OperationType.Income;
                    break;
                }
                else if (string.Equals(input, "expense", StringComparison.OrdinalIgnoreCase))
                {
                    type = OperationType.Expense;
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Введите корректный тип операции (income/expense).[/]");
                }
            }

            IBankAccount? account;

            while (true)
            {
                AnsiConsole.Markup("[white]Введите ID или название счета операции:[/] ");
                string? data = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(data))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Пустой ввод.[/]");
                    continue;
                }

                account = _facade.TryGetBankAccount(data);
                if (account == null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Счет не найден.[/]");
                    continue;
                }
                break;
            }

            decimal amount;

            while (true)
            {
                AnsiConsole.Markup("[white]Введите сумму операции:[/] ");
                string? data = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(data))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Пустой ввод.[/]");
                    continue;
                }

                bool isParsed = decimal.TryParse(data, out amount);
                if (!isParsed)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Неверный формат ввода.[/]");
                    continue;
                }
                if (amount <= 0)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Сумма операции должна быть положительной.[/]");
                    continue;
                }
                break;
            }

            ICategory? category;

            while (true)
            {
                AnsiConsole.Markup("[white]Введите ID или название категории:[/] ");
                string? data = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(data))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Пустой ввод.[/]");
                    continue;
                }

                category = _facade.TryGetCategory(data);
                if (category == null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Категория не найдена.[/]");
                    continue;
                }
                break;
            }

            AnsiConsole.Markup("[white]Введите описание к операции (опционально):[/] ");
            string? description = Console.ReadLine();

            var operation = _facade.CreateOperation(type, account.Id, amount, category.Id, description);

            // --- Отображение результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Centered()
                .Title("[bold white]Новая операция создана успешно![/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Тип[/]")
                .AddColumn("[bold teal]Счет операции[/]")
                .AddColumn("[bold teal]Сумма[/]")
                .AddColumn("[bold teal]Дата[/]")
                .AddColumn("[bold teal]Категория[/]")
                .AddColumn("[bold teal]Описание[/]");

            table.AddRow(
                $"[grey]{operation.Id}[/]",
                $"[grey]{operation.Type}[/]",
                $"[grey]{account.Name}[/]",
                $"[grey]{operation.Amount}[/]",
                $"[grey]{operation.Date:g}[/]",
                $"[grey]{category.Name}[/]",
                $"[grey]{operation.Description ?? "-"}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
    }
}
