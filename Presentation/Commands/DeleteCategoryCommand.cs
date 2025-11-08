using Application.Facades.Interfaces;
using Spectre.Console;
using Presentation.Commands.Interfaces;
using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Presentation.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        private readonly ICategoriesFacade _facade;
        public string Name => "Удалить категорию";

        public DeleteCategoryCommand(ICategoriesFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {
            ICategory? category = null;

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

            // --- Обновление ---
            _facade.DeleteCategory(category.Id);

            // --- Вывод результата ---
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold yellow]Категория успешно удалена![/]");
            AnsiConsole.WriteLine();
        }
    }
}
