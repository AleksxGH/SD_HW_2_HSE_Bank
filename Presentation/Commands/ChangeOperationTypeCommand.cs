using Application.Facades.Interfaces;
using Spectre.Console;
using Presentation.Commands.Interfaces;
using Domain.Enums;
using Domain.Entities.Interfaces;

namespace Presentation.Commands
{
    public class ChangeOperationTypeCommand : ICommand
    {
        private readonly IOperationsFacade _facade;
        public string Name => "Изменить тип операции";

        public ChangeOperationTypeCommand(IOperationsFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {
            IOperation? operation;

            while (true)
            {
                AnsiConsole.Markup("[white]Введите ID операции:[/] ");
                string? data = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(data))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Пустой ввод.[/]");
                    continue;
                }

                bool isGuid = Guid.TryParse(data, out Guid id);
                if (!isGuid)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Неверный формат ввода.[/]");
                    continue;
                }
                if (id == Guid.Empty)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Операция должна иметь ID.[/]");
                    continue;
                }

                operation = _facade.GetOperationById(id);
                if (operation == null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Счет не найден.[/]");
                    continue;
                }
                break;
            }

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


            _facade.UpdateOperationType(operation.Id, type);

            // --- Отображение результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Centered()
                .Title("[bold white]Тип операции успешно изменен![/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Тип[/]")
                .AddColumn("[bold teal]Счет операции[/]")
                .AddColumn("[bold teal]Сумма[/]")
                .AddColumn("[bold teal]Дата[/]")
                .AddColumn("[bold teal]Категория[/]")
                .AddColumn("[bold teal]Описание[/]");

            table.AddRow(
                $"[white]{operation.Id}[/]",
                $"[grey]{operation.Type}[/]",
                $"[white]{_facade.TryGetBankAccount(operation.BankAccountId.ToString())!.Name}[/]",
                $"[grey]{operation.Amount}[/]",
                $"[grey]{operation.Date:g}[/]",
                $"[white]{_facade.TryGetCategory(operation.CategoryId.ToString())!.Name}[/]",
                $"[grey]{operation.Description ?? "-"}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
    }
}
