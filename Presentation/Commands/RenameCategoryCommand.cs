using Application.Facades.Interfaces;
using Domain.Entities.Interfaces;
using Spectre.Console;
using Presentation.Commands.Interfaces;

namespace Presentation.Commands
{
    public class RenameCategoryCommand : ICommand
    {
        private readonly ICategoriesFacade _facade;
        public string Name => "Переименовать категорию";

        public RenameCategoryCommand(ICategoriesFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {
            ICategory? category = null;

            // --- Ввод счёта ---
            while (category == null)
            {
                AnsiConsole.Markup("[white]Введите название или ID категории:[/] ");
                var data = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(data))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Некорректный формат ввода.[/]");
                    continue;
                }

                category = _facade.TryGetCategory(data);
                if (category == null)
                    AnsiConsole.MarkupLine("[red]Ошибка: Категория не найдена.[/]");
            }

            // --- Ввод нового имени ---
            string? newName = null;
            while (string.IsNullOrWhiteSpace(newName))
            {
                AnsiConsole.Markup("[white]Введите новое название категории:[/] ");
                newName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Категория должна иметь название.[/]");
                    continue;
                }

                if (_facade.TryGetCategory(newName) != null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Категория с таким названием уже существует.[/]");
                    newName = null;
                }
            }

            // --- Обновление ---
            _facade.UpdateCategoryName(category.Id, newName);
            category = _facade.GetCategoryById(category.Id);

            // --- Вывод результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Title("[bold white]Категория успешно переименована![/]")
                .AddColumn("[bold teal]Название[/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Тип[/]");

            table.AddRow(
                $"[grey]{category!.Name}[/]",
                $"[grey]{category.Id}[/]",
                $"[grey]{category.Type}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
    }
}
