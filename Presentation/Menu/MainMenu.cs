using Spectre.Console;
using Presentation.Commands.Interfaces;
using Presentation.Menu.Interfaces;

namespace Presentation.Menu
{
    /// <summary>
    /// Главное меню приложения.
    /// </summary>
    public class MainMenu : IMenu
    {
        private readonly List<ICommand> _commands = [];
        private readonly List<IMenu> _sections = [];

        public string Name => "Главное меню";
        public string Label => "HSE Bank";

        public IEnumerable<ICommand> Commands => _commands;

        public IEnumerable<IMenu> Sections => _sections;

        /// <summary>
        /// Добавить команду 
        /// </summary>
        public void AddCommand(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _commands.Add(command);
        }

        /// <summary>
        /// Добавить раздел меню (подменю).
        /// </summary>
        public void AddSection(IMenu section)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            _sections.Add(section);
        }

        /// <summary>
        /// Отображение главного меню и навигация по разделам.
        /// </summary>
        public void Show()
        {
            while (true)
            {
                AnsiConsole.Clear();

                // Заголовок
                var figlet = new FigletText(Label).Color(Color.Teal);
                AnsiConsole.Write(new Align(figlet, HorizontalAlignment.Center));
                AnsiConsole.WriteLine();

                AnsiConsole.MarkupLine($"[white]{Name}[/]");
                AnsiConsole.WriteLine();

                // Список пунктов меню
                var menuChoices = _sections.Select(s => s.Name)
                    .Concat(_commands.Select(c => c.Name))
                    .Concat(["Выход"])
                    .ToList();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[teal]Выберите раздел:[/]")
                        .PageSize(10)
                        .HighlightStyle(new Style(Color.Teal))
                        .AddChoices(menuChoices)
                );

                if (choice == "Выход")
                {
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine("[red]Сессия завершена.[/]");
                    break;
                }

                // Если выбрали подменю
                var selectedSection = _sections.FirstOrDefault(s => s.Name == choice);
                if (selectedSection != null)
                {
                    selectedSection.Show();
                    continue;
                }

                // Если выбрали команду
                var selectedCommand = _commands.FirstOrDefault(c => c.Name == choice);
                if (selectedCommand != null)
                {
                    ExecuteCommand(selectedCommand);
                    continue;
                }

                AnsiConsole.MarkupLine("[red]Ошибка: выбранный пункт не найден.[/]");
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Выполнение команды с обработкой ошибок и выводом результата.
        /// </summary>
        private static void ExecuteCommand(ICommand command)
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule($"[teal]{command.Name}[/]"));
                AnsiConsole.WriteLine();

                command.Execute();

                AnsiConsole.MarkupLine("[green]Команда выполнена успешно![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLineInterpolated($"[red]Ошибка: {ex.Message}[/]");
            }

            AnsiConsole.MarkupLine("[grey]Нажмите любую клавишу для возврата... [/]");
            Console.ReadKey(true);
        }
    }
}
