using Presentation.Commands.Interfaces;
using Presentation.Menu.Interfaces;
using Spectre.Console;

namespace Presentation.Menu
{
    /// <summary>
    /// Абстрактный базовый класс для всех разделов меню.
    /// Содержит общую логику отображения, навигации и выполнения команд.
    /// </summary>
    public abstract class MenuSection : IMenu
    {
        private readonly List<ICommand> _commands = [];
        private readonly List<IMenu> _sections = [];

        /// <summary>
        /// Название раздела меню (отображается в списке).
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Заголовок раздела (используется в Figlet).
        /// </summary>
        public abstract string Label { get; }

        /// <summary>
        /// Команды, доступные в меню.
        /// </summary>
        public IEnumerable<ICommand> Commands => _commands.AsReadOnly();

        /// <summary>
        /// Подразделы текущего меню.
        /// </summary>
        public IEnumerable<IMenu> Sections => _sections.AsReadOnly();

        /// <summary>
        /// Добавляет новую команду в меню.
        /// </summary>
        public virtual void AddCommand(ICommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            _commands.Add(command);
        }

        /// <summary>
        /// Добавляет вложенный раздел в меню.
        /// </summary>
        public virtual void AddSection(IMenu section)
        {
            ArgumentNullException.ThrowIfNull(section);
            _sections.Add(section);
        }

        /// <summary>
        /// Отображает меню и обрабатывает выбор пользователя.
        /// </summary>
        public virtual void Show()
        {
            while (true)
            {
                AnsiConsole.Clear();

                // --- Заголовок ---
                var figlet = new FigletText(Label).Color(Color.Teal);
                AnsiConsole.Write(new Align(figlet, HorizontalAlignment.Center));
                AnsiConsole.WriteLine();

                AnsiConsole.MarkupLine($"[white]{Name}[/]");
                AnsiConsole.WriteLine();

                // --- Формируем список пунктов меню ---
                var menuChoices = _sections.Select(s => s.Name)
                    .Concat(_commands.Select(c => c.Name))
                    .Concat(["Назад"])
                    .ToList();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[teal]Выберите действие:[/]")
                        .PageSize(10)
                        .HighlightStyle(new Style(Color.Teal))
                        .AddChoices(menuChoices)
                );

                // --- Обработка выбора ---
                if (choice == "Назад")
                    return;

                var selectedSection = _sections.FirstOrDefault(s => s.Name == choice);
                if (selectedSection != null)
                {
                    selectedSection.Show();
                    continue;
                }

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
        protected virtual void ExecuteCommand(ICommand command)
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule($"[teal]{command.Name}[/]"));
                AnsiConsole.WriteLine();

                command.Execute();

                AnsiConsole.MarkupLine("[green]Команда успешно выполнена![/]");
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
