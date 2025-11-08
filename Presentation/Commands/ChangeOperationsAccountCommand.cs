using Application.Facades.Interfaces;
using Spectre.Console;
using Presentation.Commands.Interfaces;
using Domain.Enums;
using Domain.Entities.Interfaces;

namespace Presentation.Commands
{
    public class ChangeOperationsAccountCommand : ICommand
    {
        private readonly IOperationsFacade _facade;
        public string Name => "Изменить счет операции";

        public ChangeOperationsAccountCommand(IOperationsFacade facade)
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

            IBankAccount? account;

            while (true)
            {
                AnsiConsole.Markup("[white]Введите ID или название нового счета операции:[/] ");
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


            _facade.UpdateOperationAccountId(operation.Id, account.Id);

            // --- Отображение результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Centered()
                .Title("[bold white]Счет операции успешно изменен![/]")
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
                $"[grey]{_facade.TryGetCategory(operation.CategoryId.ToString())!.Name}[/]",
                $"[grey]{operation.Description ?? "-"}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
    }
}
