using Presentation.Commands.Interfaces;
using Application.Facades.Interfaces;
using Spectre.Console;

namespace Presentation.Commands
{
    /// <summary>
    /// Команда для создания нового банковского счёта.
    /// </summary>
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
            // --- Ввод названия счёта ---
            string name;
            while (true)
            {
                AnsiConsole.Markup("[white]Введите название счёта:[/] ");
                name = Console.ReadLine()?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(name))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Название счёта не может быть пустым.[/]");
                    continue;
                }

                if (_facade.TryGetBankAccount(name) != null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Счёт с таким названием уже существует.[/]");
                    continue;
                }

                break;
            }

            // --- Ввод начального баланса ---
            decimal initialBalance;
            while (true)
            {
                AnsiConsole.Markup("[white]Введите начальный баланс:[/] ");
                var input = Console.ReadLine();

                if (!decimal.TryParse(input, out initialBalance))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Введите корректное число.[/]");
                    continue;
                }

                break;
            }

            // --- Создание счёта ---
            var account = _facade.CreateBankAccount(name, initialBalance);

            // --- Отображение результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Centered()
                .Title("[bold white]Новый банковский счёт создан успешно![/]")
                .AddColumn("[bold teal]Название[/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Баланс[/]");

            table.AddRow(
                $"[grey]{account.Name}[/]",
                $"[grey]{account.Id}[/]",
                $"[grey]{account.Balance:F2}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
    }
}
