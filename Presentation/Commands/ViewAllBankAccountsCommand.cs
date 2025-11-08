using Presentation.Commands.Interfaces;
using Application.Facades.Interfaces;
using Spectre.Console;

namespace Presentation.Commands
{
    public class ViewAllBankAccountsCommand : ICommand
    {
        private readonly IBankAccountsFacade _facade;
        public string Name => "Просмотреть все банковские счета";

        public ViewAllBankAccountsCommand(IBankAccountsFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {
            var accounts = _facade.GetAllBankAccounts();

            if (accounts == null || !accounts.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет доступных счетов.[/]");
                return;
            }

            // Создаем таблицу
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[bold white]Список банковских счетов[/]")
                .AddColumn("[bold teal]Название[/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Баланс[/]");

            // Добавляем строки
            foreach (var acc in accounts)
            {
                var balanceColor = acc.Balance >= 0 ? "green" : "red";
                table.AddRow(
                    $"[white]{acc.Name}[/]",
                    $"[grey]{acc.Id}[/]",
                    $"[{balanceColor}]{acc.Balance:F2}[/]"
                );
            }

            // Печатаем таблицу
            AnsiConsole.Write(table);
        }
    }
}
