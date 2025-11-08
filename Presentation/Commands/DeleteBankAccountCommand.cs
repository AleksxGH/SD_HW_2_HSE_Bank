using Presentation.Commands.Interfaces;
using Application.Facades.Interfaces;
using Domain.Entities.Interfaces;
using Spectre.Console;

namespace Presentation.Commands
{
    public class DeleteBankAccountCommand : ICommand
    {
        private readonly IBankAccountsFacade _facade;
        public string Name => "Удалить банковский счет";

        public DeleteBankAccountCommand(IBankAccountsFacade facade)
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

            // --- Обновление ---
            _facade.DeleteBankAccount(account.Id);

            // --- Вывод результата ---
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold yellow]Банковский счёт удален![/]");
            AnsiConsole.WriteLine();
        }
    }

}
