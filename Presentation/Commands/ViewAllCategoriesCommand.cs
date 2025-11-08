using Application.Facades.Interfaces;
using Spectre.Console;
using Presentation.Commands.Interfaces;

namespace Presentation.Commands
{
    public class ViewAllCategoriesCommand : ICommand
    {
        private readonly ICategoriesFacade _facade;
        public string Name => "Просмотреть все категории";

        public ViewAllCategoriesCommand(ICategoriesFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {
            var categories = _facade.GetAllCategories();

            if (categories == null || !categories.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет доступных категорий.[/]");
                return;
            }

            // Создаем таблицу
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[bold white]Список категорий[/]")
                .AddColumn("[bold teal]Название[/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Тип[/]");

            // Добавляем строки
            foreach (var category in categories)
            {
                var balanceColor = category.Type == Domain.Enums.CategoryType.Income ? "green" : "red";
                table.AddRow(
                    $"[white]{category.Name}[/]",
                    $"[grey]{category.Id}[/]",
                    $"[{balanceColor}]{category.Type}[/]"
                );
            }

            // Печатаем таблицу
            AnsiConsole.Write(table);
        }
    }
}
