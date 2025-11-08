using Presentation.Commands.Interfaces;
using Application.Facades.Interfaces;
using Application.Facades;
using Spectre.Console;

namespace Presentation.Commands
{
    public class CreateBankAccountCommand : ICommand
    {
        private readonly IBankAccountsFacade _facade;
        public string Name => "Создать новый банковский счет";

        public CreateBankAccountCommand(IBankAccountsFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {
            bool enterFlag = false;
            string name = string.Empty;
            decimal amount = 0;

            do
            {
                AnsiConsole.Markup("[white]Введите название счёта:[/] ");
                name = Console.ReadLine()!;
                enterFlag = !string.IsNullOrWhiteSpace(name);
                if (!enterFlag)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Банковский счет должен иметь название.[/]");
                }
                else
                {
                    break;
                }
            } while (!enterFlag);

            enterFlag = false;

            do
            {
                AnsiConsole.Markup("[white]Введите начальный баланс:[/] ");
                string? amountStr = Console.ReadLine();
                enterFlag = decimal.TryParse(amountStr, out amount);
                if (!enterFlag)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: введите корректное числовое значение для баланса.[/]");
                }
                else
                {
                    break;
                }
            } while (!enterFlag);


            var account = _facade.CreateBankAccount(name, amount);

            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[white]Новый банковский счёт создан успешно![/]")
                .AddColumn("[teal]Название[/]")
                .AddColumn("[teal]ID[/]")
                .AddColumn("[teal]Баланс[/]");

            table.AddRow(
                $"[grey]{account.Name}[/]",
                $"[grey]{account.Id}[/]",
                $"[grey]{account.Balance}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }
    }
}
