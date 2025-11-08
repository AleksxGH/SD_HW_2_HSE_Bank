using Presentation.Commands.Interfaces;
using Application.Facades.Interfaces;
using Domain.Entities.Interfaces;
using Spectre.Console;

namespace Presentation.Commands
{
    public class RenameBankAccountCommand : ICommand
    {
        private readonly IBankAccountsFacade _facade;
        public string Name => "Переименовать банковский счет";

        public RenameBankAccountCommand(IBankAccountsFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {
            IBankAccount? account = null;

            // --- Ввод счёта ---
            while (account == null)
            {
                AnsiConsole.Markup("[white]Введите название или ID счёта:[/] ");
                var data = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(data))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Некорректный формат ввода.[/]");
                    continue;
                }

                account = _facade.TryGetBankAccount(data);
                if (account == null)
                    AnsiConsole.MarkupLine("[red]Ошибка: Банковский счёт не найден.[/]");
            }

            // --- Ввод нового имени ---
            string? newName = null;
            while (string.IsNullOrWhiteSpace(newName))
            {
                AnsiConsole.Markup("[white]Введите новое название счёта:[/] ");
                newName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Банковский счёт должен иметь название.[/]");
                    continue;
                }

                if (_facade.TryGetBankAccount(newName) != null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Счёт с таким названием уже существует.[/]");
                    newName = null;
                }
            }

            // --- Обновление ---
            _facade.UpdateBankAccountName(account.Id, newName);
            account = _facade.GetBankAccountById(account.Id);

            // --- Вывод результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Title("[bold white]Банковский счёт успешно переименован![/]")
                .AddColumn("[bold teal]Название[/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Баланс[/]");

            table.AddRow(
                $"[grey]{account!.Name}[/]",
                $"[grey]{account.Id}[/]",
                $"[grey]{account.Balance:F2}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
    }

}
