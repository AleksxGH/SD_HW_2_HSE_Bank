using Application.Facades.Interfaces;
using Spectre.Console;
using Presentation.Commands.Interfaces;
using Domain.Enums;

namespace Presentation.Commands
{
    public class ViewAllOperationsCommand : ICommand
    {
        private readonly IOperationsFacade _facade;
        public string Name => "Просмотреть все банковские операции";

        public ViewAllOperationsCommand(IOperationsFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {

            var operations = _facade.GetAllOperations();

            if (operations == null || !operations.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет доступных операций.[/]");
                return;
            }

            // Создаем таблицу
            var table = new Table()
                 .Border(TableBorder.Rounded)
                 .Expand()
                 .Centered()
                 .Title("[bold white]Список банковских операций![/]")
                 .AddColumn("[bold teal]ID[/]")
                 .AddColumn("[bold teal]Тип[/]")
                 .AddColumn("[bold teal]Счет операции[/]")
                 .AddColumn("[bold teal]Сумма[/]")
                 .AddColumn("[bold teal]Дата[/]")
                 .AddColumn("[bold teal]Категория[/]")
                 .AddColumn("[bold teal]Описание[/]");

            // Добавляем строки
            foreach (var operation in operations)
            {
                var operationColor = operation.Type == OperationType.Income ? "green" : "red";
                table.AddRow(
                $"[white]{operation.Id}[/]",
                $"[{operationColor}]{operation.Type}[/]",
                $"[white]{_facade.TryGetBankAccount(operation.BankAccountId.ToString())!.Name}[/]",
                $"[{operationColor}]{operation.Amount}[/]",
                $"[grey]{operation.Date:g}[/]",
                $"[white]{_facade.TryGetCategory(operation.CategoryId.ToString())!.Name}[/]",
                $"[grey]{operation.Description ?? "-"}[/]"
            );
            }

            // Печатаем таблицу
            AnsiConsole.Write(table);
        }
    }
}
