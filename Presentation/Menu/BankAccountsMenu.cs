using Presentation.Commands.Interfaces;
using Presentation.Menu.Interfaces;
using Spectre.Console;

namespace Presentation.Menu
{
    /// <summary>
    /// Меню для управления банковскими счетами.
    /// </summary>
    public class BankAccountsMenu : IMenu
    {
        private readonly List<ICommand> _commands = new();
        private readonly List<IMenu> _sections = new();

        public string Name => "Банковские счета";

        public IEnumerable<ICommand> Commands => _commands;

        public IEnumerable<IMenu> Sections => _sections;

        /// <summary>
        /// Добавляет команду в меню.
        /// </summary>
        public void AddCommand(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _commands.Add(command);
        }

        /// <summary>
        /// Добавляет вложенный раздел меню.
        /// </summary>
        public void AddSection(IMenu section)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            _sections.Add(section);
        }

        /// <summary>
        /// Отображает меню и обрабатывает выбор пользователя.
        /// </summary>
        public void Show()
        {
            while (true)
            {
                AnsiConsole.Clear();

                // Заголовок
                var figlet = new FigletText("HSE Bank").Color(Color.Teal);
                AnsiConsole.Write(new Align(figlet, HorizontalAlignment.Center));
                AnsiConsole.WriteLine();

                AnsiConsole.MarkupLine($"[white]{Name}[/]");
                AnsiConsole.WriteLine();

                // Собираем пункты меню
                var menuChoices = _sections.Select(s => s.Name)
                    .Concat(_commands.Select(c => c.Name))
                    .Concat(new[] { "Назад" })
                    .ToList();

                // Выбор пользователя
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[teal]Выберите действие:[/]")
                        .PageSize(10)
                        .HighlightStyle(new Style(Color.Teal))
                        .AddChoices(menuChoices)
                );

                // Возврат в главное меню
                if (choice == "Назад")
                    return;

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
        /// Выполняет выбранную команду с обработкой ошибок и выводом результата.
        /// </summary>
        private void ExecuteCommand(ICommand command)
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule($"[teal]{command.Name}[/]"));
                AnsiConsole.WriteLine();

                command.Execute();

                AnsiConsole.MarkupLine("[green]✅ Команда успешно выполнена![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLineInterpolated($"[red]Ошибка: {ex.Message}[/]");
            }

            AnsiConsole.MarkupLine("[grey]Нажмите любую клавишу для возврата...[/]");
            Console.ReadKey(true);
        }
    }
}
