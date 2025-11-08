using Spectre.Console;
using Presentation.Commands.Interfaces;

namespace Presentation.Menu
{
    /// <summary>
    /// Класс меню, управляющий выполнением команд.
    /// </summary>
    public class Menu
    {
        private readonly List<ICommand> _commands = new();

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void Run()
        {
            bool isFirstRun = true; // Флаг для показа приветствия только один раз

            while (true)
            {
                AnsiConsole.Clear();

                // --- Заголовок ---
                var figlet = new FigletText("HSE Bank")
                    .Color(Color.Teal);
                AnsiConsole.Write(new Align(figlet, HorizontalAlignment.Center));
                AnsiConsole.WriteLine();

                // --- Приветствие (только при первом запуске) ---
                if (isFirstRun)
                {
                    AnsiConsole.MarkupLine("[white]Добро пожаловать в систему управления финансами![/]"); ;
                    AnsiConsole.WriteLine();
                    isFirstRun = false;
                }

                // --- Меню выбора ---
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[Teal]Выберите действие:[/]")
                        .PageSize(10)
                        .HighlightStyle(new Style(Color.Teal))
                        .AddChoices(_commands.Select(c => c.Name).Concat(new[] { "Выход" }))
                );

                if (choice == "Выход")
                {
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine("[red]Сессия завершена.[/]");
                    break;
                }

                var selectedCommand = _commands.FirstOrDefault(c => c.Name == choice);
                if (selectedCommand == null)
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: выбранная команда не найдена.[/]");
                    continue;
                }

                Console.Clear();
                AnsiConsole.Write(new Rule($"[Teal]{selectedCommand.Name}[/]"));
                AnsiConsole.WriteLine();

                try
                {
                    selectedCommand.Execute();
                    AnsiConsole.MarkupLine("[green]Команда выполнена успешно![/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLineInterpolated($"[red]Ошибка: {ex.Message}[/]");
                }

                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[grey]Нажмите любую клавишу, чтобы продолжить...[/]");
                Console.ReadKey(true);
            }
        }
    }
}
