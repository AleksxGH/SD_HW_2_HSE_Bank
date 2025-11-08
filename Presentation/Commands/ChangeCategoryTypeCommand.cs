using Application.Facades.Interfaces;
using Domain.Enums;
using Spectre.Console;
using Presentation.Commands.Interfaces;
using Domain.Entities.Interfaces;

namespace Presentation.Commands
{
    public class ChangeCategoryTypeCommand : ICommand
    {
        private readonly ICategoriesFacade _facade;
        public string Name => "Изменить тип категории";

        public ChangeCategoryTypeCommand(ICategoriesFacade facade)
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

            CategoryType newType;
            while (true)
            {
                AnsiConsole.Markup("[white]Введите тип категории (income/expense):[/] ");
                var input = Console.ReadLine()?.Trim();

                if (string.Equals(input, "income", StringComparison.OrdinalIgnoreCase))
                {
                    newType = CategoryType.Income;
                    break;
                }
                else if (string.Equals(input, "expense", StringComparison.OrdinalIgnoreCase))
                {
                    newType = CategoryType.Expense;
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Введите корректный тип категории (income/expense).[/]");
                }
            }

            // --- Обновление ---
            _facade.UpdateCategoryType(category.Id, newType);
            category = _facade.GetCategoryById(category.Id);

            // --- Вывод результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Title("[bold white]Тип категории успешно изменен![/]")
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
