using Application.Facades.Interfaces;
using Domain.Enums;
using Spectre.Console;
using Presentation.Commands.Interfaces;


namespace Presentation.Commands
{
    public class CreateCategoryCommand : ICommand
    {
        private readonly ICategoriesFacade _facade;
        public string Name => "Создать новую категорию";

        public CreateCategoryCommand(ICategoriesFacade facade)
        {
            _facade = facade;
        }

        public void Execute()
        {

            string name;
            while (true)
            {
                AnsiConsole.Markup("[white]Введите название категории:[/] ");
                name = Console.ReadLine()?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(name))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Название категории не может быть пустым.[/]");
                    continue;
                }

                if (_facade.TryGetCategory(name) != null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Категория с таким названием уже существует.[/]");
                    continue;
                }

                break;
            }

            CategoryType type;

            while (true)
            {
                AnsiConsole.Markup("[white]Введите тип категории (income/expense):[/] ");
                var input = Console.ReadLine()?.Trim();

                if (string.Equals(input, "income", StringComparison.OrdinalIgnoreCase))
                {
                    type = CategoryType.Income;
                    break;
                }
                else if (string.Equals(input, "expense", StringComparison.OrdinalIgnoreCase))
                {
                    type = CategoryType.Expense;
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Введите корректный тип категории (income/expense).[/]");
                }
            }

            var category = _facade.CreateCategory(name, type);

            // --- Отображение результата ---
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Expand()
                .Centered()
                .Title("[bold white]Новая категория создана успешно![/]")
                .AddColumn("[bold teal]Название[/]")
                .AddColumn("[bold teal]ID[/]")
                .AddColumn("[bold teal]Тип[/]");

            table.AddRow(
                $"[grey]{category.Name}[/]",
                $"[grey]{category.Id}[/]",
                $"[grey]{category.Type}[/]"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
    }
}
